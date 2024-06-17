using System;
using Services;
using Services.SceneManagement;
using UnityEngine;

namespace ScriptableObjects.SceneManagement
{
    [Serializable]
    public class LoadableSceneData
    {
        [field: SerializeField]
        public LoadableScene LoadableScene { get; private set; }
        
        [field: SerializeField]
        public string SceneName { get; private set; }
    }
}