using System;
using Contracts.Presentation.Loader;
using Contracts.Repositories.Loader;
using DG.Tweening;
using UnityEngine;

namespace Presentation.Presenter.Loader
{
    public class LoaderPresenter : ILoaderPresenter
    {
        private readonly ILoaderView _view;
        private readonly ILoaderConfig _config;
        
        private float _rotationValue;
        private float RotationValue
        {
            get => _rotationValue;
            set => _rotationValue += Time.deltaTime * value;
        }
        
        public LoaderPresenter(ILoaderView view, ILoaderConfig config)
        {
            _view = view;
            _config = config;
        }

        public void Move(Vector2 moveDirection, float moveSpeed, float rotateSpeed)
        {
            bool isForwardMove = moveDirection.y > 0;
            
            Vector2 moveProjection = new Vector2(moveDirection.y * Mathf.Cos(RotationValue), moveDirection.y * Mathf.Sin(RotationValue));
            
            _view.Rigidbody.linearVelocity = new Vector3(
                moveProjection.x * moveSpeed, 
                _view.Rigidbody.linearVelocity.y,
                moveProjection.y * moveSpeed);
            
            RotationValue = -moveDirection.x * rotateSpeed;
            
            RotateView(moveProjection, isForwardMove);
        }

        public void Rotate(float rotateDirection)
        {
            RotateSteering(rotateDirection);
        }

        public void SetEngineState(bool active)
        {
            _view.EngineEnabledObject.SetActive(active);
            _view.EngineDisabledObject.SetActive(!active);
        }

        public void MoveFork(float moveDirection)
        {
            _view.ForkTransform.position = new Vector3(
                _view.ForkTransform.position.x,
                Math.Clamp(_view.ForkTransform.position.y + moveDirection * Time.deltaTime, _config.ForkPositionRange.x, _config.ForkPositionRange.y), 
                _view.ForkTransform.position.z);
        }

        public void DisplayFuel(float current, float max)
        {
            _view.FuelBar.fillAmount = current / max;
        }

        private void RotateView(Vector2 moveProjection, bool isForwardMove)
        {
            float rotationMultipliers = isForwardMove ? 1 : -1;
            
            _view.Transform.LookAt(new Vector3(
                moveProjection.x * rotationMultipliers + _view.Transform.position.x,
                _view.Transform.position.y,
                moveProjection.y * rotationMultipliers + _view.Transform.position.z));
        }

        private void RotateSteering(float rotationDirection)
        {
            float targetAngle = rotationDirection * _config.MaxSteeringAngle;
            
            Vector3 targetLocalRotation = new Vector3(33f, targetAngle, 0f);

            if (Mathf.Approximately(_view.WheelTransform.localEulerAngles.y, targetAngle))
                return;
    
            _view.WheelTransform.DOKill();

            var smoothingTime = rotationDirection == 0 ? _config.SteeringDefaultTime : _config.SteeringSmoothTime;

            _view.WheelTransform.DOLocalRotate(targetLocalRotation, smoothingTime)
                .SetEase(Ease.OutQuad);
        }
    }
}