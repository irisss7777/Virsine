using System;
using Contracts.Presentation.Loader;
using Contracts.Signals.Input;
using Domain.Loader;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Application.Loader
{
    public class LoaderEngineService : IMessageDisposable
    {
        public event Action OnDispose;
        
        private readonly LoaderModel _loaderModel;
        private readonly ILoaderPresenter _loaderPresenter;
        
        public LoaderEngineService(LoaderModel loaderModel, MessageBus messageBus, ILoaderPresenter loaderPresenter)
        {
            _loaderModel = loaderModel;
            _loaderPresenter = loaderPresenter;

            messageBus.Subscribe((EngineSetActiveSignal signal) => _loaderModel.SetActiveEngine(), this);
            
            _loaderModel.EngineActive.OnValueChanged += ChangeActiveEngine;
        }

        private void ChangeActiveEngine(bool active)
        {
            _loaderPresenter.SetEngineState(active);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
            
            _loaderModel.EngineActive.OnValueChanged -= ChangeActiveEngine;
        }
    }
}