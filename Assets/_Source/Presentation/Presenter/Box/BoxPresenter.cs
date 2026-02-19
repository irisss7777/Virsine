using System;
using Contracts.Repositories.Box;
using DG.Tweening;
using Presentation.View.Box;
using Presentation.View.Loader;
using UnityEngine;

namespace Presentation.Presenter.Box
{
    public class BoxPresenter : IDisposable
    {
        private readonly IBoxConfig _config;
        private readonly BoxView _view;
        
        private ForkView _attachedFork;
        private bool _isActive = true;

        public BoxPresenter(BoxView view, IBoxConfig config)
        {
            _view = view;
            _config = config;

            _view.OnTriggerChanged += HandleTrigger;
            _view.OnDestroyBox += HandleDestroy;
            
            SpawnBox();
        }

        private void SpawnBox()
        {
            _isActive = false;

            Vector3 startPos = _view.transform.position;
            startPos.y = _config.SpawnHeight;
            _view.transform.position = startPos;

            AnimateSpawn();
        }

        private void AnimateSpawn()
        {
            Vector3 targetPos = _view.transform.position;
            targetPos.y = 0f;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_view.transform.DOMove(targetPos, _config.FallDuration))
                    .Join(_view.transform.DOLocalRotate(
                        new Vector3(0, 5 * 360f, 0),
                        _config.RotationDuration,
                        RotateMode.LocalAxisAdd))
                    .OnComplete(() =>
                    {
                        _isActive = true;
                        SetGroundedState(true);
                    });
        }

        private void HandleDestroy()
        {
            _view.SetColliderEnable(false);
            _isActive = false;
            SetGroundedState(false);
            _view.transform.SetParent(null);
            
            AnimateDestroy();
        }

        private void AnimateDestroy()
        {
            Vector3 targetPos = _view.transform.position;
            targetPos.y = _config.SpawnHeight;

            float rotX = 3f * 360f;
            float rotY = 5f * 360f;
            float rotZ = 2f * 360f;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_view.transform.DOMove(targetPos, _config.RiseDuration).SetEase(Ease.InQuad))
                .Join(_view.transform.DOLocalRotate(
                    new Vector3(rotX, rotY, rotZ),
                    _config.RiseDuration,
                    RotateMode.LocalAxisAdd).SetEase(Ease.InQuad))
                .OnComplete(Dispose);
        }

        private void HandleTrigger(bool isEnter, Collider other)
        {
            if (!_isActive) 
                return;

            if (other.TryGetComponent(out ForkView fork) && isEnter)
                _attachedFork = fork;
            
            if (other.gameObject.layer == _config.GroundLayer)
                SetGroundedState(isEnter);
        }
        
        private void SetGroundedState(bool grounded)
        {
            _view.SetPickupMod(!grounded);
            _view.Rigidbody.constraints = grounded ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;

            if (_attachedFork != null)
                _view.transform.SetParent(grounded ? null : _attachedFork.transform);

            if (!grounded)
            {
                Vector3 localPos = _view.transform.localPosition;
                localPos.y += _config.AttachOffset.y;
                localPos.z += _config.AttachOffset.z;
                _view.transform.localPosition = localPos;
            }
        }

        public void Dispose()
        {
            _view.OnTriggerChanged -= HandleTrigger;
            _view.OnDestroyBox -= HandleDestroy;
            UnityEngine.Object.Destroy(_view.gameObject);
        }
    }
}