using UnityEngine;

namespace GamesClub.Code.Services.Factories.ClawCraneFactory
{
    public interface IClawCraneFactory
    {
        void CreateControllPanelView(Transform parent);
        Transform CreateUIRoot();
        void CreateClawCrane();
        void CreateWinPopUp(Transform parent);
    }
}