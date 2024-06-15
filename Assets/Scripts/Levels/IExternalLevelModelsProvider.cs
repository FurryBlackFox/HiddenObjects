using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Levels
{
    public interface IExternalLevelModelsProvider
    {
        public UniTask<List<ExternalLevelModel>> GetExternalLevelModels();
    }
}