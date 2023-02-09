using UnityEngine;

namespace GamesClub.Code.Core.ClawCraneGame.Joystick
{
    public class JoystickView : MonoBehaviour
    {
        [SerializeField] private GameObject _up;
        [SerializeField] private GameObject _left;
        [SerializeField] private GameObject _right;

        public void Right()
        {
            _up.SetActive(false);
            _left.SetActive(false);
            _right.SetActive(true);
        }
        
        public void Left()
        {
            _up.SetActive(false);
            _left.SetActive(true);
            _right.SetActive(false);
        }
        
        public void Up()
        {
            _up.SetActive(true);
            _left.SetActive(false);
            _right.SetActive(false);
        }
    }
}