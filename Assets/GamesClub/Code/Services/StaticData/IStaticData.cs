using GamesClub.Code.Data.StaticData;
using GamesClub.Code.Data.StaticData.ClawCraneGame;
using GamesClub.Code.Data.StaticData.MemoryGame;
using GamesClub.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace GamesClub.Code.Services.StaticData
{
    public interface IStaticData
    {
        PrefabsData Prefabs { get; }
        GameVariantData GameViews { get; }
        MemoryGameConfig MemoryGameConfig { get; }
        Vector2[] CardsCoord { get; }
        ClawCraneGameConfig ClawCraneGameConfig { get; }
        SoundData Sounds { get; }
    }
}