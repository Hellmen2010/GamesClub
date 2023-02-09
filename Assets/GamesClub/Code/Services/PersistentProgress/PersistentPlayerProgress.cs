using GamesClub.Code.Data.Progress;

namespace GamesClub.Code.Services.PersistentProgress
{
    public class PersistentPlayerProgress : IPersistentProgress
    {
        public PlayerProgress Progress { get; set; }
    }
}