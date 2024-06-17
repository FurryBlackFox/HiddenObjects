using System;
using Cysharp.Threading.Tasks;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ChangeSceneButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private LoadableScene targetScene;
        
        private CustomSceneManager _customSceneManager;

        [Inject]
        private void Construct(CustomSceneManager customSceneManager)
        {
            _customSceneManager = customSceneManager;
        }

        private void Awake()
        {
            button.onClick.AddListener(ChangeScene);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(ChangeScene);
        }
        
        private void ChangeScene()
        {
            _customSceneManager.LoadScene(targetScene).Forget();
        }
    }
}