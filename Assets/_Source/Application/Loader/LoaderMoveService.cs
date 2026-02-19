using System;
using Contracts.Presentation.Loader;
using Contracts.Signals.Input;
using Domain.Loader;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.Presenter.Loader;
using UnityEngine;

namespace Application.Loader
{
    public class LoaderMoveService : IMessageDisposable
    {
        public event Action OnDispose;
        
        private readonly LoaderModel _loaderModel;
        private readonly ILoaderPresenter _loaderPresenter;
        
        public LoaderMoveService(LoaderModel loaderModel, ILoaderPresenter loaderPresenter, MessageBus messageBus)
        {
            _loaderModel = loaderModel;
            _loaderPresenter = loaderPresenter;
            
            messageBus.Subscribe((MoveInputSignal signal) => Move(signal.MoveDirection), this);
        }

        private void Move(Vector2 moveDirection)
        {
            _loaderPresenter.Rotate(moveDirection.x);
            
            if (moveDirection.y == 0)
                return;

            if(!_loaderModel.UseFuel())
                return;

            _loaderPresenter.Move(moveDirection, _loaderModel.GetMoveSpeed(), _loaderModel.GetRotateSpeed());
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}