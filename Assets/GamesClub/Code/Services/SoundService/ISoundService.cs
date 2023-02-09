using GamesClub.Code.Data.Enums;
using GamesClub.Code.Data.Progress;
using GamesClub.Code.Data.StaticData.Sounds;

namespace GamesClub.Code.Services.SoundService
{
    public interface ISoundService
    {
        void Construct(SoundData soundData, Settings userSettings);
        void EnableBackgroundMusic();
        void DisableBackgroundMusic();
        void EnableLoopEffect(SoundId soundId);
        void DisableLoopEffect();
        void PlayEffectSound(SoundId soundId);
        void SetMusicVolume(float volume);
        void SetEffectsVolume(float volume);
        float GetEffectsVolume { get; }
        float GetMusicVolume { get; }
        bool MusicMuted { get; set; }
        bool EffectsMuted { get; set; }
        void SetBackgroundVolume(float volume);
        float MuteUnmuteMusic();
        float MuteUnmuteEffects();
    }
}