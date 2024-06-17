using ScriptableObjects.SceneManagement;
using Services;
using Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class RootServicesInstaller : MonoInstaller
    {
        [SerializeField] private LoadableScenesContainer loadableScenesContainer;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ServerDataProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelDataProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CustomSceneManager>().AsSingle().WithArguments(loadableScenesContainer);
        }
    }
}