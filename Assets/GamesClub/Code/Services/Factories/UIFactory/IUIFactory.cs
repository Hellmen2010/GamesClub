using GamesClub.Code.Core.UI.GameVariant;
using GamesClub.Code.Core.UI.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Services.Factories.UIFactory
{
    public interface IUIFactory
    {
        GameObject CreateRootCanvas();
        SettingsView CreateSettings(Transform parent);
        void CreateScore(Transform rootCanvas);
        void CreateBackButton(Transform rootCanvas);
        void CreateMainMenu(Transform transform);
        Button CreatePlayButton(Transform root);
        GameVariantView[] CreateGameVariants(Transform parent);
        void CreateTimer();
        void CreateResultPopUp(Transform parent);
    }
}