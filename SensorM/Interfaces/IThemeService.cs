using SensorM.Services;

namespace SensorM.Interfaces
{
    public interface IThemeService
    {
        Theme CurrentTheme { get; }
        void SetLightTheme();
        void SetDarkTheme();
        void SetTheme(Theme theme);
    }
}