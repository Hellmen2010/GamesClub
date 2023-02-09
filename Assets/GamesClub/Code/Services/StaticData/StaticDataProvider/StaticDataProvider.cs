using GamesClub.Code.Data.StaticData;
using GamesClub.Code.Data.StaticData.ClawCraneGame;
using GamesClub.Code.Data.StaticData.MemoryGame;
using GamesClub.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace GamesClub.Code.Services.StaticData.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string PrefabsDataPath = "StaticData/Prefabs Data";
        private const string GameVariantDataPath = "StaticData/GameVariantsData";
        private const string MemoryGameConfigPath = "StaticData/MemoryGameConfig";
        private const string ClawCraneGameConfigPath = "StaticData/ClawCraneGameConfig";
        private const string SoundDataPath = "StaticData/SoundData";

        public PrefabsData LoadPrefabsData() => 
            Resources.Load<PrefabsData>(PrefabsDataPath);

        public GameVariantData LoadGameVariantsData() => 
            Resources.Load<GameVariantData>(GameVariantDataPath);

        public MemoryGameConfig LoadMemoryGameConfig() => 
            Resources.Load<MemoryGameConfig>(MemoryGameConfigPath);

        public ClawCraneGameConfig LoadClawCraneGameConfig() => 
            Resources.Load<ClawCraneGameConfig>(ClawCraneGameConfigPath);

        public SoundData LoadSoundData() => 
            Resources.Load<SoundData>(SoundDataPath);
    }
}