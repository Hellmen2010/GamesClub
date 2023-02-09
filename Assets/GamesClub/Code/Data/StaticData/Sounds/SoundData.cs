using UnityEngine;

namespace GamesClub.Code.Data.StaticData.Sounds
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Static Data/SoundData")]
    public class SoundData : ScriptableObject
    {
        public AudioClipData[] AudioEffectClips;
        public AudioClip BackgroundMusic;
    }

}