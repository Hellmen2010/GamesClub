using GamesClub.Code.Data.StaticData;
using GamesClub.Code.Data.StaticData.ClawCraneGame;
using GamesClub.Code.Data.StaticData.MemoryGame;
using GamesClub.Code.Data.StaticData.Sounds;

namespace GamesClub.Code.Services.StaticData.StaticDataProvider
{
    public interface IStaticDataProvider
    {
        PrefabsData LoadPrefabsData();
        GameVariantData LoadGameVariantsData();
        MemoryGameConfig LoadMemoryGameConfig();
        ClawCraneGameConfig LoadClawCraneGameConfig();
        SoundData LoadSoundData();
    }
}