using UnityEngine;
using UnityEngine.UI;

namespace Contracts.Presentation.Loader
{
    public interface ILoaderView
    {
        public Rigidbody Rigidbody { get; }
        public Transform WheelTransform { get; }
        public Transform CameraTransform { get; }
        public Transform ForkTransform { get; }
        public Transform Transform { get; }
        public GameObject EngineEnabledObject { get; }
        public GameObject EngineDisabledObject { get; }
        public Image FuelBar { get; }
    }
}