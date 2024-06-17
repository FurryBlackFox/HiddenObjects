using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Levels;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

namespace Services
{
    public class LevelDataProvider
    {
        private Dictionary<int, LevelData> _levelDataDictionary = new();
        
        private SaveService _saveService;
        private ServerDataProvider _serverDataProvider;

        public LevelDataProvider(SaveService saveService, ServerDataProvider serverDataProvider)
        {
            _saveService = saveService;
            _serverDataProvider = serverDataProvider;
        }

        private async UniTask LoadModels()
        {
            var externalLevelModels = await GetExternalLevelModels();

            foreach (var externalLevelModel in externalLevelModels)
            {
                var savedModel = LoadRuntimeLevelModel(externalLevelModel.Id);
                
                var levelData = new LevelData(externalLevelModel, savedModel);
                _levelDataDictionary.TryAdd(externalLevelModel.Id, levelData);
            }
        }
        
        private async UniTask<IEnumerable<ExternalLevelModel>> GetExternalLevelModels()
        {
            var serverData = await _serverDataProvider.GetTextFromUrl(Consts.Links.LevelDataListUrl);
            
            if (string.IsNullOrEmpty(serverData))
                return Enumerable.Empty<ExternalLevelModel>();
            
            try
            {
                var levelModelsList = JsonConvert.DeserializeObject<List<ExternalLevelModel>>(serverData);
                return levelModelsList;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return Enumerable.Empty<ExternalLevelModel>();
            }
        }

        public async UniTask<LevelData> DownloadMissingPartsForModel(int id)
        {
            var levelData = await GetLevelData(id);
            
            if (levelData == null)
                return null;

            Sprite sprite = null;
            
            try
            {
                sprite = await _serverDataProvider.GetSpriteFromUrl(levelData.ExternalLevelModel.ImageUrl);

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            var downloadableLevelModel = new DownloadableLevelModel(sprite, sprite);
            levelData.DownloadableLevelModel = downloadableLevelModel;
            return levelData;
        }

        private string GetModelSaveKey(int id)
        {
            return Consts.SaveKeys.RuntimeModelKey + id;
        }

        private RuntimeLevelModel LoadRuntimeLevelModel(int id)
        {
            var runtimeModel = _saveService.LoadData<RuntimeLevelModel>(GetModelSaveKey(id));
            runtimeModel ??= new RuntimeLevelModel(id);
            return runtimeModel;
        }

        public async UniTask<IEnumerable<LevelData>> GetFullLevelData()
        {
            if (_levelDataDictionary.Count == 0)
                await LoadModels();
            
            return _levelDataDictionary.Values;
        }
        
        public async UniTask<LevelData> GetLevelData(int id)
        {
            if (_levelDataDictionary.Count == 0)
                await LoadModels();

            _levelDataDictionary.TryGetValue(id, out var levelData);
            return levelData;
        }
        
        public async UniTask UpdateRuntimeModel(int id, RuntimeLevelModel runtimeLevelModel)
        {
            if (_levelDataDictionary.Count == 0)
                await LoadModels();

            _levelDataDictionary.TryGetValue(id, out var levelData);
            
            if(levelData == null)
                return;
            
            levelData.RuntimeLevelModel = runtimeLevelModel;
            _saveService.SaveData(GetModelSaveKey(id), runtimeLevelModel);
        }
    }
}