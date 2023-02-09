using TMPro;
using UnityEngine;

namespace GamesClub.Code.Core.UI.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        public void SetTime(float time)
        {
            if (time < 0) time = 0;
            _timerText.text = time.ToString("F1");
        }
    }
}