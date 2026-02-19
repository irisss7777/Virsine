using Contracts.Repositories.Box;
using Contracts.Repositories.Loader;
using Infrastructure.Repositories.Config;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Installers/ScriptableInstaller")]
public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
{
    [SerializeField] private LoaderConfig _loaderConfig;
    [SerializeField] private BoxConfig _boxConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<ILoaderConfig>().FromInstance(_loaderConfig).AsSingle();
        Container.Bind<IBoxConfig>().FromInstance(_boxConfig).AsSingle();
    }
}