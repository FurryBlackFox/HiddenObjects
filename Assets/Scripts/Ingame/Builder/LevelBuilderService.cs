using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;
using Zenject;

namespace Ingame
{
    public class LevelBuilderService : MonoBehaviour
    {
        private Camera _mainCamera;
        private LevelDataProvider _levelDataProvider;
        private LevelProgressService _levelProgressService;
        private ClickableObject.Factory _clickableObjectFactory;

        [Inject]
        public void Construct(Camera mainCamera, ClickableObject.Factory clickableObjectFactory, 
            LevelDataProvider levelDataProvider, LevelProgressService levelProgressService)
        {
            _mainCamera = mainCamera;
            _clickableObjectFactory = clickableObjectFactory;
            _levelDataProvider = levelDataProvider;
            _levelProgressService = levelProgressService;
        }
        
        public async UniTask BuildLevel(int levelId)
        {
            var levelData = await _levelDataProvider.GetLevelData(levelId);

            await _levelProgressService.Init(levelId);

            var clickableObject = _clickableObjectFactory.Create();

            var sprite = levelData.DownloadableLevelModel.PreviewSprite;
            
            var worldSize = new Vector2(2 * _mainCamera.orthographicSize * _mainCamera.aspect,
                2 * _mainCamera.orthographicSize);
            
            var initParams = new ClickableObjectInitParams(worldSize, sprite, Vector2.zero);
            clickableObject.Init(initParams);
        }
    }
}