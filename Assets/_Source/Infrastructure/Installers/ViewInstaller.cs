using Contracts.Presentation.Camera;
using MessagePipe;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.View.Camera;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private CameraView _cameraView;
        
        public override void InstallBindings()
        {
            Container.Bind<ICameraView>().FromInstance(_cameraView).AsSingle();
        }
    }
}