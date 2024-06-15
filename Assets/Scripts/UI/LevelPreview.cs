using System;
using DefaultNamespace.UI;
using TMPro;
using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class LevelPreview : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<LevelPreview>
        {
        }
        
        [SerializeField] private Button button;
        
        [SerializeField] private Image previewImage;
        [SerializeField] private TextMeshProUGUI levelNameText;
        [SerializeField] private ProgressBar progressBar;
        
        private ILevelPreviewModel _previewModel;
        private Action _onButtonClickedCallback;

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        public void Init(ILevelPreviewModel levelPreviewModel, Action onButtonClickedCallback)
        {
            _previewModel = levelPreviewModel;
            _onButtonClickedCallback = onButtonClickedCallback;
            
            previewImage.sprite = _previewModel.PreviewSprite;
            levelNameText.SetText(levelPreviewModel.LevelName);
            
            progressBar.SetProgress(_previewModel.CurrentProgress, _previewModel.MaxProgress);
        }

        private void OnButtonClick()
        {
            _onButtonClickedCallback?.Invoke();
        }
    }
}