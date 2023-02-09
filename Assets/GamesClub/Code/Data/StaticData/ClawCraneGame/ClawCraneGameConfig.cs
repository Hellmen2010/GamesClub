using UnityEngine;

namespace GamesClub.Code.Data.StaticData.ClawCraneGame
{
    [CreateAssetMenu(fileName = "ClawCraneGameConfig", menuName = "Static Data/ClawCraneGameConfig")]
    public class ClawCraneGameConfig : ScriptableObject
    {
        public float MoveSpeed;
        public BallData[] Wins;
        public float LeftBoundary;
        public float RightBoundary;
        public float YBallsPosition;
        public float AnimationTime;
    }
}