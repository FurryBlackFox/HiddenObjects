using UnityEngine;

namespace DefaultNamespace.UI
{
    public interface ILevelPreviewModel
    {
        public int Id { get; }

        public Sprite PreviewSprite { get; }

        public string LevelName { get; }
        
        public int CurrentProgress { get; }

        public int MaxProgress { get; }
    }
}