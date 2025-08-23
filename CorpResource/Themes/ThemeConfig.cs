using MudBlazor;

namespace CorpResource.Themes;

public class ThemeConfig
{
    public static MudTheme LightTheme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#1976d2",
            Secondary = "#9c27b0",
            Background = "#f5f5f5",
            AppbarBackground = "#1976d2",
            DrawerBackground = "#ffffff",
            DrawerText = "rgba(0,0,0, 0.7)",
            DrawerIcon = "rgba(0,0,0, 0.7)",
            Success = "#4caf50",
            Info = "#2196f3",
            Warning = "#ff9800",
            Error = "#f44336",
            TextPrimary = "rgba(0,0,0, 0.87)",
            TextSecondary = "rgba(0,0,0, 0.54)",
            ActionDefault = "rgba(0,0,0, 0.54)",
            ActionDisabled = "rgba(0,0,0, 0.26)",
            ActionDisabledBackground = "rgba(0,0,0, 0.12)",
            Divider = "rgba(0,0,0, 0.12)",
            DividerLight = "rgba(255,255,255, 0.12)"
        }
    };
}
