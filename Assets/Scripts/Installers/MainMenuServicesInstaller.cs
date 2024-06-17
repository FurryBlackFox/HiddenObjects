using UI;
using UI.LevelSelector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainMenuServicesInstaller : MonoInstaller
    {
        [SerializeField] private LevelPreview levelPreviewPrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<LevelPreview, LevelPreview.Factory>().FromComponentInNewPrefab(levelPreviewPrefab);
        }
    }
}