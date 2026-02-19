using Contracts.Presentation.Loader;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.View.Loader
{
    [RequireComponent(typeof(Rigidbody))]
    public class LoaderView : MonoBehaviour, ILoaderView
    {
        public Rigidbody Rigidbody => GetComponent<Rigidbody>();
        public Transform WheelTransform => _wheelTransform;
        public Transform CameraTransform => _cameraTransform;
        public Transform ForkTransform => _forkTransform;
        public Transform Transform => transform;
        public GameObject EngineEnabledObject => _engineEnabledObject;
        public GameObject EngineDisabledObject => _engineDisabledObject;
        public Image FuelBar => _fuelBar;

        [SerializeField] private Transform _viewModel;
        [SerializeField] private Transform _wheelTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _forkTransform;
        [SerializeField] private GameObject _engineEnabledObject;
        [SerializeField] private GameObject _engineDisabledObject;
        [SerializeField] private Image _fuelBar;
    }
}