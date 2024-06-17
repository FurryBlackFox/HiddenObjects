using UnityEngine;

namespace Levels
{
    public class DownloadableLevelModel
    {
        public Sprite PreviewSprite { get; set; }

        public bool IsFullyLoaded { get; set; }

        public DownloadableLevelModel(bool isFullyLoaded, Sprite previewSprite)
        {
            IsFullyLoaded = isFullyLoaded;
            PreviewSprite = previewSprite;
        }
    }
}