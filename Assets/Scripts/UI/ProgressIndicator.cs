using System;
using Ingame;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ProgressIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI progressText;
        
        private LevelProgressService _levelProgressService;

        [Inject]
        private void Construct(LevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }

        private void Awake()
        {
            _levelProgressService.OnProgressUpdated += UpdateProgress;
        }

        private void OnDestroy()
        {
            _levelProgressService.OnProgressUpdated -= UpdateProgress;
        }

        private void Start()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            var leftoverProgress = _levelProgressService.TargetProgress - _levelProgressService.CurrentProgress;
            progressText.SetText(leftoverProgress.ToString());
        }
    }
}