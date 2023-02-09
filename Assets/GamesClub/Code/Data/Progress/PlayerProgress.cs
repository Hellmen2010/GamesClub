using System;

namespace GamesClub.Code.Data.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public Settings Settings;
        public Records Records;

        public PlayerProgress()
        {
            Settings = new Settings();
            Records = new Records();
        }
    }
}