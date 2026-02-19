using System;
using Contracts.Presentation.Loader;
using Contracts.Signals.Input;
using Domain.Loader;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Application.Loader
{
    public class LoaderForkService : IMessageDisposable
    {
        public event Action OnDispose;
        
        private readonly ILoaderPresenter _loaderPresenter;
        private readonly LoaderModel _loaderModel;

        public LoaderForkService(LoaderModel loaderModel, ILoaderPresenter loaderPresenter, MessageBus messageBus)
        {
            _loaderPresenter = loaderPresenter;
            _loaderModel = loaderModel;

            messageBus.Subscribe((ForkInputSignal signal) => MoveFork(signal.ForkDirection), this);
        }

        private void MoveFork(float moveDirection)
        {
            if(moveDirection == 0)
                return;
            
            if(!_loaderModel.EngineActive.Value)
                return;
            
            if(!_loaderModel.UseFuel())
                return;
            
            _loaderPresenter.MoveFork(moveDirection * _loaderModel.ForkSpeed);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}