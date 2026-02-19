using Contracts.Presentation.Camera;
using Unity.Cinemachine;
using UnityEngine;

namespace Presentation.View.Camera
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;

        public void SetCameraPosition(Transform cameraTransform) => _cinemachineCamera.Follow = cameraTransform;
    }
}