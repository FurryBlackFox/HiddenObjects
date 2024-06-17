using Newtonsoft.Json;
using UnityEngine;

namespace Services
{
    public class SaveService
    {
        public void SaveData<T>(string saveKey, T savableModel) where T : class
        {
            var serializedModel = JsonConvert.SerializeObject(savableModel);
            PlayerPrefs.SetString(saveKey, serializedModel);
        }

        public T LoadData<T>(string saveKey) where T : class
        {
            var serializedModel = PlayerPrefs.GetString(saveKey, "");
            var savedModel = string.IsNullOrEmpty(serializedModel)
                ? null
                : JsonConvert.DeserializeObject<T>(serializedModel);
            return savedModel;
        }
    }
}