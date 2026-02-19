using System;
using Contracts.Presentation.Loader;
using Domain.Loader;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Application.Loader
{
    public class LoaderFuelService : IMessageDisposable
    {
        public event Action OnDispose;
        
        private readonly LoaderModel _loaderModel;
        private readonly ILoaderPresenter _loaderPresenter;

        public LoaderFuelService(LoaderModel loaderModel, ILoaderPresenter loaderPresenter)
        {
            _loaderModel = loaderModel;
            _loaderPresenter = loaderPresenter;

            _loaderModel.CurrentFuel.OnValueChanged += FuelValueChanged;
        }

        private void FuelValueChanged(float fuelValue)
        {
            _loaderPresenter.DisplayFuel(fuelValue, _loaderModel.MaxFuelValue);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
            _loaderModel.CurrentFuel.OnValueChanged -= FuelValueChanged;
        }
    }
}