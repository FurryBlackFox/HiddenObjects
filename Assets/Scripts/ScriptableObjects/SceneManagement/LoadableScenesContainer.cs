using System.Collections.Generic;
using System.Linq;
using Services;
using UnityEngine;

namespace ScriptableObjects.SceneManagement
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Loadable Scenes Container")]
    public class LoadableScenesContainer : ScriptableObject
    {
        [field: SerializeField]
        private List<LoadableSceneData> LoadableSceneDataList { get; set; }

        public string GetLoadableSceneName(LoadableScene loadableScene)
        {
            var sceneData = LoadableSceneDataList.FirstOrDefault(d => d.LoadableScene == loadableScene);
            return sceneData?.SceneName;
        }
    }
}