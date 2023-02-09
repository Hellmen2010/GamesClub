using System;
using System.Linq;
using GamesClub.Code.Data.Enums;
using GamesClub.Code.Data.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Core.UI.GameVariant
{
    public class GameVariantView : MonoBehaviour
    {
        public event Action<Type> OnPlay;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _background;
        private Type _gameState;
        
        public Games GameType { get; private set; }

        public void Construct(GameViewData gameViewData)
        {
            //_background.sprite = Screen.width >= 1200 
              //  ? gameViewData.GameSpriteIpad 
                //: gameViewData.GameSpriteIphone;

            if (Screen.width >= 1200)
            {
                _background.sprite = gameViewData.GameSpriteIpad;
                _background.rectTransform.sizeDelta -= new Vector2(0, 100);
            }
            else
            {
                _background.sprite = gameViewData.GameSpriteIphone;
            }
            
            
            GameType = gameViewData.GameType;
            _titleText.text = gameViewData.GameType.ToString().Replace("_", " ");
            if(_titleText.text.Equals("Coming Soon", StringComparison.CurrentCultureIgnoreCase))
                _playButton.gameObject.SetActive(false);

            _gameState = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => 
                t.GetTypes()).First(t => String.Equals(t.Name, gameViewData.GameStateName, StringComparison.Ordinal));
        }
        
        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayButton);
        }

        private void OnPlayButton()
        {
            OnPlay?.Invoke(_gameState);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButton);
        }
    }
}