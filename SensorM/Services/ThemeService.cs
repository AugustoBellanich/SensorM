using Microsoft.Maui.Controls;
using SensorM.Interface;

namespace SensorM.Services
{
    public class ThemeService : IThemeService
    {
        public Theme CurrentTheme { get; private set; } = Theme.Light;

        public void SetLightTheme()
        {
            CurrentTheme = Theme.Light;
            if (Application.Current.Resources.TryGetValue("LightBackgroundColor", out var lightBackgroundColor))
            {
                Application.Current.Resources["CurrentBackgroundColor"] = lightBackgroundColor;
            }

            if (Application.Current.Resources.TryGetValue("LightTextColor", out var lightTextColor))
            {
                Application.Current.Resources["CurrentTextColor"] = lightTextColor;
            }
        }

        public void SetDarkTheme()
        {
            CurrentTheme = Theme.Dark;
            if (Application.Current.Resources.TryGetValue("DarkBackgroundColor", out var darkBackgroundColor))
            {
                Application.Current.Resources["CurrentBackgroundColor"] = darkBackgroundColor;
            }

            if (Application.Current.Resources.TryGetValue("DarkTextColor", out var darkTextColor))
            {
                Application.Current.Resources["CurrentTextColor"] = darkTextColor;
            }
        }

        public void SetTheme(Theme theme)
        {
            CurrentTheme = theme;
            if (theme == Theme.Dark)
            {
                SetDarkTheme();
            }
            else
            {
                SetLightTheme();
            }
        }
    }
}
