using UnityEngine;

namespace GamesClub.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "GameVariantData", menuName = "Static Data/GameVariantData")]
    public class GameVariantData : ScriptableObject
    {
        public GameViewData[] GameViewsData;
    }
}