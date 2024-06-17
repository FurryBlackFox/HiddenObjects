using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIElements
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private Image progressImage;

        public float CurrentProgress { get; private set; }

        public void SetProgress(float currentValue, float maxValue)
        {
            CurrentProgress = currentValue > 0 ? currentValue / maxValue : 0;
            
            if(progressImage.type != Image.Type.Filled)
                return;
            
            progressImage.fillAmount = CurrentProgress;
            progressText.SetText($"{currentValue}/{maxValue}");
        }
    }
}