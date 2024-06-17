using UnityEngine;

namespace UI.LevelSelector
{
    public class LevelPreviewAdditionalInitParams
    {
        public Sprite PreviewSprite { get; }
        public bool IsFullyLoaded { get; }

        public LevelPreviewAdditionalInitParams(Sprite previewSprite, bool isFullyLoaded)
        {
            PreviewSprite = previewSprite;
            IsFullyLoaded = isFullyLoaded;
        }
    }
}