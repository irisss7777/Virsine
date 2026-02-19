using UnityEngine;

namespace Contracts.Presentation.Camera
{
    public interface ICameraView
    {
        public void SetCameraPosition(Transform cameraTransform);
    }
}