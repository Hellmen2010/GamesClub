using GamesClub.Code.Data.StaticData;
using GamesClub.Code.Data.StaticData.ClawCraneGame;
using GamesClub.Code.Data.StaticData.MemoryGame;
using GamesClub.Code.Data.StaticData.Sounds;
using GamesClub.Code.Services.StaticData.StaticDataProvider;
using UnityEngine;

namespace GamesClub.Code.Services.StaticData
{
    public class StaticData : IStaticData
    {
        public PrefabsData Prefabs { get; private set; }
        public GameVariantData GameViews { get; private set; }
        public MemoryGameConfig MemoryGameConfig { get; private set; }

        public SoundData Sounds { get; private set; }
        public Vector2[] CardsCoord { get; private set; }
        public ClawCraneGameConfig ClawCraneGameConfig { get; private set; }

        private readonly IStaticDataProvider _staticDataProvider;


        public StaticData(IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            LoadStaticData();
            CreateCardsCoords();
        }

        private void CreateCardsCoords()
        {
            CardsCoord = new Vector2[MemoryGameConfig.Pairs.Length * 2];
            
            for (int i = 0; i < CardsCoord.Length; i++)
            {
                var row = i / MemoryGameConfig.CardsInRow;
                var column = i % MemoryGameConfig.CardsInRow;
                
                CardsCoord[i] = new Vector2(
                    column * MemoryGameConfig.LengthBtwCardsHorizontal,
                    (int)row * MemoryGameConfig.LengthBtwCardsVertical);
            }
        }

        private void LoadStaticData()
        {
            Prefabs = _staticDataProvider.LoadPrefabsData();
            GameViews = _staticDataProvider.LoadGameVariantsData();
            MemoryGameConfig = _staticDataProvider.LoadMemoryGameConfig();
            ClawCraneGameConfig = _staticDataProvider.LoadClawCraneGameConfig();
            Sounds = _staticDataProvider.LoadSoundData();
            Debug.Log(1);
        }
    }
}