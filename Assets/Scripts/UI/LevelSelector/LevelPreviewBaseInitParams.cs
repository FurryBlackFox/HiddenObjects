using UnityEngine;

namespace UI.LevelSelector
{
    public class LevelPreviewBaseInitParams
    {
        public int Id { get; }

        public string LevelName { get; }
        
        public int CurrentProgress { get; }

        public int MaxProgress { get; }
        public bool IsCompleted { get; }

        
        public LevelPreviewBaseInitParams(int id, string levelName, int currentProgress, int maxProgress, bool isCompleted)
        {
            Id = id;
            LevelName = levelName;
            CurrentProgress = currentProgress;
            MaxProgress = maxProgress;
            IsCompleted = isCompleted;
        }
    }
}