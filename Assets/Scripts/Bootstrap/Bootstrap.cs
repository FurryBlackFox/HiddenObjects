using System;
using Cysharp.Threading.Tasks;
using Services;
using Services.SceneManagement;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        private CustomSceneManager _customSceneManager;
        
        [Inject]
        private void Construct(CustomSceneManager customSceneManager)
        {
            _customSceneManager = customSceneManager;
        }
        
        private void Start()
        {
            LoadMainMenu().Forget();
        }

        private async UniTask LoadMainMenu()
        {
            await _customSceneManager.LoadScene(LoadableScene.MainMenu);
        }
    }
}