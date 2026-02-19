using Infrastructure.Factory.Box;
using Infrastructure.Factory.Loader;
using Zenject;

namespace Infrastructure.Installers
{
    public class BaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            FactoryBind();
        }

        private void FactoryBind()
        {
            Container.BindInterfacesAndSelfTo<LoaderFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BoxFactory>().AsSingle();
        }
    }
}