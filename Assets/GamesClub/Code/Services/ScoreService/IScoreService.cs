namespace GamesClub.Code.Services.ScoreService
{
    public interface IScoreService
    {
        void ChangeScore(float score);
        float Score { get; }
    }
}