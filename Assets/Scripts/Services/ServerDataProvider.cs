using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Levels;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace Services
{
    public class ServerDataProvider : IExternalLevelModelsProvider
    {
        public async UniTask<List<ExternalLevelModel>> GetExternalLevelModels()
        {
            var serverData = await SendWebRequest(Consts.Links.LevelDataListUrl);
            
            if (string.IsNullOrEmpty(serverData))
                return null;
            
            try
            {
                var levelModelsList = JsonConvert.DeserializeObject<List<ExternalLevelModel>>(serverData);
                return levelModelsList;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return null;
            }
        }


        private async UniTask<string> SendWebRequest(string url)
        {
            using var request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.InProgress:
                    Debug.LogError("Still in progress");
                    break;
                case UnityWebRequest.Result.Success:
                    break;
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError($"Request error: {request.result}, {request.error}");
                    return null;
                default:
                    Debug.LogError("Unsupported Request Error");
                    return null;
            }
            var result = request.downloadHandler.text;
            Debug.Log(result);
            return result;
        }
    }
}