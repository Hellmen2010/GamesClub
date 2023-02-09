using GamesClub.Code.Data.Progress;

namespace GamesClub.Code.Services.PersistentProgress
{
    public interface IPersistentProgress
    {
        PlayerProgress Progress { get; set; }
    }
}