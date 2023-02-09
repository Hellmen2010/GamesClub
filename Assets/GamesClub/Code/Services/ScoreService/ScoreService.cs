using GamesClub.Code.Services.PersistentProgress;
using GamesClub.Code.Services.SaveLoad;

namespace GamesClub.Code.Services.ScoreService
{
    public class ScoreService : IScoreService
    {
        private readonly IPersistentProgress _progress;
        private readonly ISaveLoad _saveLoad;

        public ScoreService(IPersistentProgress progress, ISaveLoad saveLoad)
        {
            _progress = progress;
            _saveLoad = saveLoad;
        }

        public void ChangeScore(float score)
        {
            _progress.Progress.Records.TotalScore += score;
            _saveLoad.SaveProgress();
        }

        public float Score => _progress.Progress.Records.TotalScore;
    }
}