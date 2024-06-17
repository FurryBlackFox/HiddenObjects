using System;
using Cysharp.Threading.Tasks;
using Levels;
using Services;
using Services.SceneManagement;
using UnityEngine;

namespace Ingame
{
    public class LevelProgressService
    {
        public event Action OnProgressUpdated;

        public int CurrentProgress => _levelData.RuntimeLevelModel.CurrentProgress;
        public int TargetProgress => _levelData.ExternalLevelModel.MaxCounterProgress;
        
        private LevelDataProvider _levelDataProvider;
        private CustomSceneManager _customSceneManager;

        private LevelData _levelData;

        public LevelProgressService(LevelDataProvider levelDataProvider, CustomSceneManager customSceneManager)
        {
            _levelDataProvider = levelDataProvider;
            _customSceneManager = customSceneManager;
        }

        public async UniTask Init(int levelId)
        {
            _levelData =  await _levelDataProvider.GetLevelData(levelId);
            OnProgressUpdated?.Invoke();
        }
        
        public void CountClick()
        {
            _levelData.RuntimeLevelModel.CurrentProgress = Mathf.Min(CurrentProgress + 1, TargetProgress);
            
            _levelDataProvider.UpdateRuntimeModel(_levelData.ExternalLevelModel.Id, _levelData.RuntimeLevelModel)
                .Forget();
            
            OnProgressUpdated?.Invoke();

            if (CurrentProgress == TargetProgress)
                _customSceneManager.LoadScene(LoadableScene.MainMenu).Forget();
        }
    }
}