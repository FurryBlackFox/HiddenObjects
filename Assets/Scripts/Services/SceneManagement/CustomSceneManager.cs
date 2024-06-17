using Cysharp.Threading.Tasks;
using Ingame;
using ScriptableObjects.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class CustomSceneManager
    {
        private LoadableScenesContainer _loadableScenesContainer;

        private Scene _loadingScreenScene;
        private Scene _mainScene;

        public CustomSceneManager(LoadableScenesContainer loadableScenesContainer)
        {
            _loadableScenesContainer = loadableScenesContainer;
            _mainScene = SceneManager.GetActiveScene();
        }

        public async UniTask LoadScene(LoadableScene loadableScene, bool showLoadingScreen = true,
            UniTask beforeSceneActivationTask = default)
        {
            var sceneName = _loadableScenesContainer.GetLoadableSceneName(loadableScene);
            
            if(string.IsNullOrEmpty(sceneName))
                return;

            await LoadSceneInternal(sceneName, showLoadingScreen, beforeSceneActivationTask);
        }
        
        public async UniTask LoadIngameScene(int levelId)
        {
            var task = UniTask.Defer(() => BuildIngameScene(levelId));
            await LoadScene(LoadableScene.Ingame, beforeSceneActivationTask: task);
        }

        private async UniTask BuildIngameScene(int levelId)
        {
            var levelBuilder = TryToFindComponentOnScene<LevelBuilderService>();
            await levelBuilder.BuildLevel(levelId);
        }
        
        public T TryToFindComponentOnScene<T>() where T : MonoBehaviour
        {
            foreach (var sceneGameObject in _mainScene.GetRootGameObjects())
            {
                if (sceneGameObject.TryGetComponent<T>(out var target))
                {
                    return target;
                }
            }

            return null;
        }
        
        private async UniTask LoadSceneInternal(string sceneName, bool showLoadingScreen,
            UniTask beforeSceneActivationTask = default)
        {
            if(showLoadingScreen)
                await ShowLoadingScreen();
            
            var sceneUnloadAsync = SceneManager.UnloadSceneAsync(_mainScene);
            await sceneUnloadAsync;

            var sceneLoadAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            await sceneLoadAsync;
            
            var newScene = SceneManager.GetSceneByName(sceneName);
            _mainScene = newScene;
            
            await beforeSceneActivationTask;

            SceneManager.SetActiveScene(_mainScene);

            await HideLoadingScreen();
        }
        
        private async UniTask ShowLoadingScreen()
        {
            if (!_loadingScreenScene.IsValid())
            {
                var loadingSceneName = _loadableScenesContainer.GetLoadableSceneName(LoadableScene.LoadingScreen);
                var asyncOperation = SceneManager.LoadSceneAsync(loadingSceneName, LoadSceneMode.Additive);
                await asyncOperation;
                _loadingScreenScene = SceneManager.GetSceneByName(loadingSceneName);
            }

            SceneManager.SetActiveScene(_loadingScreenScene);
        }

        private async UniTask HideLoadingScreen()
        {
            if(!_loadingScreenScene.IsValid())
                return;
            
            var asyncOperation = SceneManager.UnloadSceneAsync(_loadingScreenScene);
            if (asyncOperation == null)
            {
                return;
            }
            await asyncOperation;
        }
    }
}