using System;
using GamesClub.Code.Services.PersistentProgress;
using GamesClub.Code.Services.SaveLoad;
using GamesClub.Code.Services.SoundService;

namespace GamesClub.Code.Core.UI.Settings
{
    public class SettingsPanel : IDisposable
    {
        private readonly SettingsView _view;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoad;
        private readonly ISoundService _soundService;

        public SettingsPanel(SettingsView view, IPersistentProgress persistentProgress, ISaveLoad saveLoad, ISoundService soundService)
        {
            _view = view;
            _persistentProgress = persistentProgress;
            _saveLoad = saveLoad;
            _soundService = soundService;

            SubscribeView();
        }
        
        public void Dispose()
        {
            _view.EffectSoundSettingsView.OnSwitch -= OnEffectSoundSwitch;
            _view.MusicSoundSettingsView.OnSwitch -= OnMusicSoundSwitch;
            _view.EffectSoundSettingsView.OnVolumeChanged -= OnEffectVolumeChanged;
            _view.MusicSoundSettingsView.OnVolumeChanged -= OnMusicVolumeChanged;
        }

        private void SubscribeView()
        {
            _view.EffectSoundSettingsView.OnSwitch += OnEffectSoundSwitch;
            _view.MusicSoundSettingsView.OnSwitch += OnMusicSoundSwitch;
            _view.EffectSoundSettingsView.OnVolumeChanged += OnEffectVolumeChanged;
            _view.MusicSoundSettingsView.OnVolumeChanged += OnMusicVolumeChanged;
        }

        private void OnEffectSoundSwitch(bool isActive)
        {
            _soundService.EffectsMuted = _persistentProgress.Progress.Settings.IsEffectsSoundActive = isActive;
            _saveLoad.SaveProgress();
        }

        private void OnMusicSoundSwitch(bool isActive)
        {
            _soundService.MusicMuted = _persistentProgress.Progress.Settings.IsMusicSoundActive = isActive;
            _saveLoad.SaveProgress();
        }

        private void OnEffectVolumeChanged(float volume)
        {
            _persistentProgress.Progress.Settings.EffectsVolume = volume;
            _soundService.SetEffectsVolume(volume);
            _saveLoad.SaveProgress();
        }

        private void OnMusicVolumeChanged(float volume)
        {
            _persistentProgress.Progress.Settings.MusicVolume = volume;
            _soundService.SetBackgroundVolume(volume);
            _saveLoad.SaveProgress();
        }
    }
}