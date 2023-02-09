using System;
using GamesClub.Code.Data.Enums;
using UnityEngine;

namespace GamesClub.Code.Data.StaticData
{
    [Serializable]
    public class GameViewData
    {
        public Sprite GameSpriteIphone;
        public Sprite GameSpriteIpad;
        public Games GameType;
        public string GameStateName;
    }
}