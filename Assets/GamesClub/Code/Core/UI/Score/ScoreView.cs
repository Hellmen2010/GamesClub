using System;
using TMPro;
using UnityEngine;

namespace GamesClub.Code.Core.UI.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        public void Show() => gameObject.SetActive(true);
        
        public void Hide() => gameObject.SetActive(false);

        public void SetScoreText(float value) => 
            _score.text = "Score: " + Math.Round(value, 2);
    }
}