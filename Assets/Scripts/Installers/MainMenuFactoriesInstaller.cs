using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainMenuFactoriesInstaller : MonoInstaller
    {
        [SerializeField] private LevelPreview levelPreviewPrefab;
        
        public override void InstallBindings()
        {
            Container.BindFactory<LevelPreview, LevelPreview.Factory>().FromComponentInNewPrefab(levelPreviewPrefab);
        }
    }
}