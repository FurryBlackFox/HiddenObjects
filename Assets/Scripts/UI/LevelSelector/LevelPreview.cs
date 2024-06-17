using System;
using TMPro;
using UI.UIElements;
using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace UI.LevelSelector
{
    public class LevelPreview : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<LevelPreview>
        {
        }

        [SerializeField] private GameObject completedView;
        [SerializeField] private GameObject downloadErrorView;
        [SerializeField] private Button button;
        
        [SerializeField] private Image previewImage;
        [SerializeField] private TextMeshProUGUI levelNameText;
        [SerializeField] private ProgressBar progressBar;
        
        public LevelPreviewBaseInitParams PreviewBaseInitParams { get; private set; }
        public LevelPreviewAdditionalInitParams PreviewAdditionalInitParams  { get; private set; }
        
        
        private Action _onButtonClickedCallback;

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        public void SetBaseParams(LevelPreviewBaseInitParams levelPreviewBaseInitParams, Action onButtonClickedCallback)
        {
            PreviewBaseInitParams = levelPreviewBaseInitParams;
            _onButtonClickedCallback = onButtonClickedCallback;
            
            levelNameText.SetText(levelPreviewBaseInitParams.LevelName);
            
            progressBar.SetProgress(PreviewBaseInitParams.CurrentProgress, PreviewBaseInitParams.MaxProgress);
            
            completedView.SetActive(PreviewBaseInitParams.IsCompleted);
        }

        public void SetAdditionalParams(LevelPreviewAdditionalInitParams previewAdditionalInitParams)
        {
            PreviewAdditionalInitParams = previewAdditionalInitParams;
            
            downloadErrorView.SetActive(!PreviewAdditionalInitParams.IsFullyLoaded);
            
            if(!PreviewAdditionalInitParams.IsFullyLoaded)
                return;
            
            if(PreviewAdditionalInitParams.PreviewSprite)
                previewImage.sprite = PreviewAdditionalInitParams.PreviewSprite;
        }

        private void OnButtonClick()
        {
            _onButtonClickedCallback?.Invoke();
        }
    }
}