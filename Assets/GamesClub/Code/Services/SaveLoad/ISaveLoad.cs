using GamesClub.Code.Data.Progress;

namespace GamesClub.Code.Services.SaveLoad
{
    public interface ISaveLoad
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}