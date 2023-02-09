using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Core.UI.Settings
{
    public class SettingsView : MonoBehaviour
    {
        public SoundSettingsView EffectSoundSettingsView;
        public SoundSettingsView MusicSoundSettingsView;
        
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _backgroundCloseButton;

        private void Awake()
        {
            _closeButton.onClick.AddListener(Close);
            _backgroundCloseButton.onClick.AddListener(Close);
            gameObject.SetActive(false);
        }

        public void Construct(Data.Progress.Settings userSettings)
        {
            EffectSoundSettingsView.Construct(userSettings.IsEffectsSoundActive, userSettings.EffectsVolume);
            MusicSoundSettingsView.Construct(userSettings.IsMusicSoundActive, userSettings.MusicVolume);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
            _backgroundCloseButton.onClick.RemoveListener(Close);
        }

        private void Close() => gameObject.SetActive(false);

        public void SwitchSettingsView() => 
            gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}