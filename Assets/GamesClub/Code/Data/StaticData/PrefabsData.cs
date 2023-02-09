using GamesClub.Code.Core.ClawCraneGame.ClawCrane;
using GamesClub.Code.Core.ClawCraneGame.Joystick;
using GamesClub.Code.Core.ClawCraneGame.UI;
using GamesClub.Code.Core.MemoryGame.Card;
using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.GameVariant;
using GamesClub.Code.Core.UI.ResultPopUp;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Core.UI.Settings;
using GamesClub.Code.Core.UI.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Prefabs Data", menuName = "Static Data/Prefabs Data")]
    public class PrefabsData : ScriptableObject
    {
        [Header("UI")]
        public GameObject RootCanvasPrefab;
        public SettingsView SettingsPrefab;
        public ScoreView ScoreViewPrefab;
        public BackButton BackButtonPrefab;
        public Button SettingsButtonPrefab;
        public GameObject MainMenuPrefab;
        public Button PlayButtonPrefab;
        public Transform GamesHolderPrefab;
        public GameVariantView GameVariant;

        [Header("MemoryGame")] 
        public CardView Card;
        public Transform CardHolder;
        public TimerView TimerPrefab;
        public ResultPopUpView ResultPopUpPrefab;
        
        [Header("ClawCraneGame")] 
        public ClawCraneControllView clawCraneControllPrefab;
        public ClawCraneView ClawCranePrefab;
        public ClawCraneWinPopUp clawCraneClawCraneWinPopUpPrefab;
        public JoystickView JoystickPrefab;
    }
}