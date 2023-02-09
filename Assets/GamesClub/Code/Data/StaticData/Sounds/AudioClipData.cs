using System;
using GamesClub.Code.Data.Enums;
using UnityEngine;

namespace GamesClub.Code.Data.StaticData.Sounds
{
    [Serializable]
    public class AudioClipData
    {
        public AudioClip Clip;
        public SoundId Id;
    }

}