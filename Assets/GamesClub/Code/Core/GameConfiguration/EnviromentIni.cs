using UnityEngine;

namespace GamesClub.Code.Core.SceneIni
{
    public class EnviromentIni : MonoBehaviour
    {
        [SerializeField] private GameObject _ipadEnviroment;
        [SerializeField] private GameObject _iphoneEnviroment;
        
        private const int iPadHeight = 1200;
        
        private void Start() => SetQuality();

        private void SetQuality()
        {
            if (Screen.width >= iPadHeight)
            {
                _ipadEnviroment.SetActive(true);
                _iphoneEnviroment.SetActive(false);
            }
            else
            {
                _ipadEnviroment.SetActive(false);
                _iphoneEnviroment.SetActive(true);
            }
        }
    }
}