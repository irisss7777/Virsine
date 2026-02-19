using Contracts.Repositories.Box;
using Presentation.Presenter.Box;
using Presentation.View.Box;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factory.Box
{
    public class BoxFactory
    {
        [Inject] private readonly IBoxConfig _boxConfig;

        public void CreateBox()
        {
            var view = CreateView();
            CreatePresenter(view);
        }

        private BoxView CreateView()
        {
            var prefab = _boxConfig.ViewPrefab as BoxView;
            var view = Object.Instantiate(prefab, _boxConfig.StartPosition, prefab.transform.rotation);
            return view;
        }

        private void CreatePresenter(BoxView view)
        {
            var presenter = new BoxPresenter(view, _boxConfig);
        }
    }
}