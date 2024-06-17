using Ingame;
using Ingame.ClickableObject;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class IngameServicesInstaller : MonoInstaller
    {
        [SerializeField] private InputService inputService;
        [SerializeField] private Camera mainCamera;
        
        [SerializeField] private ClickableObject clickableObjectPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Camera>().FromInstance(mainCamera);
            Container.BindInterfacesAndSelfTo<InputService>().FromInstance(inputService);

            Container.BindInterfacesAndSelfTo<LevelProgressService>().AsSingle();
            
            Container.BindFactory<ClickableObject, ClickableObject.Factory>().FromComponentInNewPrefab(clickableObjectPrefab);
        }
    }
}