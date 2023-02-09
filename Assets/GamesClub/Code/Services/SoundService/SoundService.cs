using System.Collections.Generic;
using System.Linq;
using GamesClub.Code.Data.Enums;
using GamesClub.Code.Data.Progress;
using GamesClub.Code.Data.StaticData.Sounds;
using UnityEngine;

namespace GamesClub.Code.Services.SoundService
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        public bool MusicMuted
        {
            get => _musicSource.mute;
            set => _musicSource.mute = !value;
        }

        public bool EffectsMuted
        {
            get => _effectsSource.mute;
            set
            {
                _loopEffectsSource.mute = !value;
                _effectsSource.mute = !value;
            }
        }


        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _effectsSource;
        [SerializeField] private AudioSource _loopEffectsSource;
        
        private Dictionary<SoundId, AudioClipData> _sounds;

        public void Construct(SoundData soundData, Settings userSettings)
        {
            _sounds = soundData.AudioEffectClips.ToDictionary(s => s.Id);
            _musicSource.clip = soundData.BackgroundMusic;
            _musicSource.volume = userSettings.MusicVolume;
            _musicSource.mute = !userSettings.IsMusicSoundActive;
            
            _effectsSource.volume = userSettings.EffectsVolume;
            _effectsSource.mute = !userSettings.IsEffectsSoundActive;
            _loopEffectsSource.volume = userSettings.EffectsVolume;
            _loopEffectsSource.mute = !userSettings.IsEffectsSoundActive;

        }

        private void Awake() => DontDestroyOnLoad(this);

        public void EnableBackgroundMusic() => _musicSource.Play();

        public void DisableBackgroundMusic() => _musicSource.Stop();
        
        public void EnableLoopEffect(SoundId soundId)
        {
            _loopEffectsSource.clip = _sounds[soundId].Clip;
            _loopEffectsSource.Play();
        }

        public void DisableLoopEffect() => _loopEffectsSource.Stop();
        
        public void PlayEffectSound(SoundId soundId) => 
            _effectsSource.PlayOneShot(_sounds[soundId].Clip);

        public void SetMusicVolume(float volume) =>
            _musicSource.volume = volume;

        public void SetEffectsVolume(float volume)
        {
            _loopEffectsSource.volume = volume;
            _effectsSource.volume = volume;
        }

        public float GetEffectsVolume => _effectsSource.volume;
        
        public float GetMusicVolume => _musicSource.volume;
        
        public void SetBackgroundVolume(float volume) =>
            _musicSource.volume = volume;

        public float MuteUnmuteMusic() => _musicSource.volume = _musicSource.volume > 0 ? 0 : 1;

        public float MuteUnmuteEffects()
        {
            _loopEffectsSource.volume = _effectsSource.volume > 0 ? 0 : 1;
            return _effectsSource.volume = _effectsSource.volume > 0 ? 0 : 1;
        }
    }
}