using UnityEngine;
using Zenject;

namespace Ingame
{
    public class ClickableObject : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<ClickableObject>
        {
        }


        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D objectCollider;

        private LevelProgressService _levelProgressService;
        private ClickableObjectInitParams _initParams;

        [Inject]
        private void Construct(LevelProgressService levelProgressService)
        {
            _levelProgressService = levelProgressService;
        }

        public void Init(ClickableObjectInitParams initParams)
        {
            _initParams = initParams;

            transform.position = initParams.position;
            
            spriteRenderer.sprite = initParams.Sprite;
            
            var rendererBounds = spriteRenderer.localBounds;
            var scaleRatio = initParams.targetSize / rendererBounds.size;
            var minScale = Mathf.Min(scaleRatio.x, scaleRatio.y);
            transform.localScale = Vector2.one * minScale;
            
            var bounds = spriteRenderer.localBounds;
            objectCollider.offset = bounds.center - spriteRenderer.transform.position;
            objectCollider.size = bounds.size;
        }
        
        public void OnClick()
        {
            _levelProgressService.CountClick();
        }
    }
}