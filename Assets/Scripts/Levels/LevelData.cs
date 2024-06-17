using Levels;

namespace Services
{
    public class LevelData
    {
        public ExternalLevelModel ExternalLevelModel { get; set; }

        public RuntimeLevelModel RuntimeLevelModel { get; set; }
        
        public DownloadableLevelModel DownloadableLevelModel { get; set; }

        public LevelData(ExternalLevelModel externalLevelModel, RuntimeLevelModel runtimeLevelModel)
        {
            ExternalLevelModel = externalLevelModel;
            RuntimeLevelModel = runtimeLevelModel;
        }
    }
}