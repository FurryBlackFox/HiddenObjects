using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class ServerDataProvider
    {
        public async UniTask<string> GetTextFromUrl(string url)
        {
            using var request = UnityWebRequest.Get(url);
            var requestResult = await TrySendWebRequestInternal(request);
            
            var result = requestResult 
                ? request.downloadHandler.text
                : null;
            
            return result;
        }

        public async UniTask<Texture2D> GetTextureFromUrl(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            var requestResult = await TrySendWebRequestInternal(request);
            
            var result = requestResult 
                ? DownloadHandlerTexture.GetContent(request)
                : null;
            
            return result;
        }
        
        public async UniTask<Sprite> GetSpriteFromUrl(string url)
        {
            var texture = await GetTextureFromUrl(url);

            if (!texture)
                return null;

            var rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
            var sprite = Sprite.Create(texture, rect, 0.5f * Vector2.one);
            return sprite;
        }

        private async UniTask<bool> TrySendWebRequestInternal(UnityWebRequest request)
        {
            try
            {
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
                        return false;
                    default:
                        Debug.LogError("Unsupported Request Error");
                        return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
            
            return true;
        }
    }
}