using DefaultNamespace.UI;
using UnityEngine;

namespace Levels
{
    public class LevelModel : ILevelPreviewModel
    {
        public int Id { get; private set; }
        public Sprite PreviewSprite { get; private set; }
        public string LevelName { get; private set; }
        public int CurrentProgress { get; private set; }
        public int MaxProgress { get; private set; }

        public LevelModel(ExternalLevelModel externalLevelModel)
        {
            Id = externalLevelModel.Id;
            LevelName = externalLevelModel.ImageName;
            MaxProgress = externalLevelModel.MaxCounterProgress;
            CurrentProgress = Random.Range(0, MaxProgress);
        }
    }
}