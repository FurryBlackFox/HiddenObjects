using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Services;
using Services.SceneManagement;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.LevelSelector
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private RectTransform spawnRoot;
        [SerializeField] private TextMeshProUGUI errorText;

        private List<LevelPreview> _spawnedLevelPreviews = new List<LevelPreview>();
        
        private LevelPreview.Factory _levelPreviewFactory;
        private LevelDataProvider _levelDataProvider;
        private CustomSceneManager _customSceneManager;
        
        [Inject]
        private void Construct(LevelPreview.Factory levelPreviewFactory, 
            LevelDataProvider levelDataProvider, CustomSceneManager customSceneManager)
        {
            _levelPreviewFactory = levelPreviewFactory;
            _levelDataProvider = levelDataProvider;
            _customSceneManager = customSceneManager;
        }

        private async void Start()
        {
            await ShowBaseLevelPreviews();
            await ShowFullLevelPreviews();
        }

        private async UniTask ShowBaseLevelPreviews()
        {
            try
            {
                var loadedLevelData = await _levelDataProvider.GetFullLevelData();
                var loadedLevelDataList = loadedLevelData.ToList();

                if (loadedLevelDataList.Count == 0)
                {
                    errorText.gameObject.SetActive(true);
                    return;
                }
                
                foreach (var levelData in loadedLevelDataList)
                {
                    var levelPreview = _levelPreviewFactory.Create();
                    _spawnedLevelPreviews.Add(levelPreview);
                    
                    levelPreview.transform.SetParent(spawnRoot);

                    var currentProgress = levelData.RuntimeLevelModel.CurrentProgress;
                    var targetProgress = levelData.ExternalLevelModel.MaxCounterProgress;

                    var isCompleted = currentProgress >= targetProgress;
                    
                    var levelPreviewData = new LevelPreviewBaseInitParams(levelData.ExternalLevelModel.Id,
                        levelData.ExternalLevelModel.ImageName, currentProgress, 
                        targetProgress, isCompleted);
                    
                    levelPreview.SetBaseParams(levelPreviewData, () => OnLevelPreviewClick(levelPreview));
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                errorText.gameObject.SetActive(true);
                return;
            }
        }
        
        private async UniTask ShowFullLevelPreviews()
        {
            foreach (var levelPreview in _spawnedLevelPreviews)
            {
                try
                {
                    var levelData = await _levelDataProvider.GetLevelData(levelPreview.PreviewBaseInitParams.Id);
                    if (levelData.DownloadableLevelModel == null)
                        levelData = await _levelDataProvider.DownloadMissingPartsForModel(levelPreview.PreviewBaseInitParams.Id);

                    var additionalParams =
                        new LevelPreviewAdditionalInitParams(levelData.DownloadableLevelModel.PreviewSprite,
                            levelData.DownloadableLevelModel.IsFullyLoaded);
                    
                    levelPreview.SetAdditionalParams(additionalParams);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    return;
                }
            }
        }

        private void OnLevelPreviewClick(LevelPreview levelPreview)
        {
            if(levelPreview.PreviewBaseInitParams?.IsCompleted ?? true)
                return;
            
            if(!levelPreview.PreviewAdditionalInitParams?.IsFullyLoaded ?? true)
                return;
            
            _customSceneManager.LoadIngameScene(levelPreview.PreviewBaseInitParams.Id).Forget();
        }
    }
}