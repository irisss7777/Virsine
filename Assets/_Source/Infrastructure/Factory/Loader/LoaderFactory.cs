using Application.Loader;
using Contracts.Presentation.Camera;
using Contracts.Repositories.Loader;
using Domain.Loader;
using Plugins.MessagePipe.MessageBus.Runtime;
using Presentation.Presenter.Loader;
using Presentation.View.Loader;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory.Loader
{
    public class LoaderFactory
    {
        [Inject] private readonly ICameraView _cameraView;
        [Inject] private readonly ILoaderConfig _loaderConfig;
        [Inject] private readonly MessageBus _messageBus;

        public void CreateLoader()
        {
            var model = CreateModel();
            var view = CreateView();
            var presenter = CreatePresenter(view);
            
            CreateServices(model, presenter);
        }

        private LoaderModel CreateModel()
        {
            var model = new LoaderModel(_loaderConfig);
            
            return model;
        }

        private LoaderView CreateView()
        {
            var prefab = (MonoBehaviour)_loaderConfig.LoaderViewPrefab;
            var rotation = prefab.transform.rotation;
            
            var view = Object.Instantiate(prefab, Vector3.zero, rotation).GetComponent<LoaderView>();

            _cameraView.SetCameraPosition(view.CameraTransform);
            
            return view;
        }

        private LoaderPresenter CreatePresenter(LoaderView view)
        {
            var presenter = new LoaderPresenter(view, _loaderConfig);

            return presenter;
        }

        private void CreateServices(LoaderModel model, LoaderPresenter presenter)
        {
            var moveService = new LoaderMoveService(model, presenter, _messageBus);
            var engineService = new LoaderEngineService(model, _messageBus, presenter);
            var forkService = new LoaderForkService(model, presenter, _messageBus);
            var fuelService = new LoaderFuelService(model, presenter);
        }
    }
}