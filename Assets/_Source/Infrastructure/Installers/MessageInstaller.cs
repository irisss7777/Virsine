using Contracts.Signals.Input;
using Plugins.MessagePipe.MessageBus.Runtime;
using Zenject;
using MessagePipe;

namespace Infrastructure.Installers
{
    public class MessageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MessageBus>().AsSingle();
            
            var options = Container.BindMessagePipe();
            InputBind(options);
        }
        
        private void InputBind(MessagePipeOptions options) 
        {
            Container.BindMessageBroker<MoveInputSignal>(options);
            Container.BindMessageBroker<EngineSetActiveSignal>(options);
            Container.BindMessageBroker<ForkInputSignal>(options);
        }
    }
}