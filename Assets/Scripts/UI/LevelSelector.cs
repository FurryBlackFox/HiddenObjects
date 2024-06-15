using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Levels;
using Newtonsoft.Json;
using UI;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.UI
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private RectTransform spawnRoot;
        
        private LevelPreview.Factory _levelPreviewFactory;
        private IExternalLevelModelsProvider _externalLevelModelsProvider;
        
        [Inject]
        private void Construct(IExternalLevelModelsProvider externalLevelModelsProvider, LevelPreview.Factory levelPreviewFactory)
        {
            _levelPreviewFactory = levelPreviewFactory;
            _externalLevelModelsProvider = externalLevelModelsProvider;
        }
        
        private void Awake()
        {
            RequestServerData().Forget();
        }

        private async UniTaskVoid RequestServerData()
        {
            var externalLevelModels = await _externalLevelModelsProvider.GetExternalLevelModels();
            foreach (var externalLevelModel in externalLevelModels)
            {
                Debug.Log(externalLevelModel);
                var levelPreview = _levelPreviewFactory.Create();
                levelPreview.transform.SetParent(spawnRoot);
                var levelModel = new LevelModel(externalLevelModel);
                levelPreview.Init(levelModel, null);
            }
        }
    }
}