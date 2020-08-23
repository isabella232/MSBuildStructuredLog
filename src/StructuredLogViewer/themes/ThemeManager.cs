using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
//using AdonisUI;
using ResourceLocator = AdonisUI.ResourceLocator;

namespace StructuredLogViewer
{
    public class ThemeManager
    {
        static ThemeManager()
        {
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
        }

        public static bool UseDarkTheme { get; set; }

        private static void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SystemParameters.HighContrast))
            {
                UpdateTheme();
            }
        }

        private static bool useAdonisDarkTheme;
        public static bool UseAdonisDarkTheme
        {
            get => useAdonisDarkTheme;

            set
            {
                if (useAdonisDarkTheme == value)
                {
                    return;
                }

                useAdonisDarkTheme = value;
                var appResources = Application.Current.Resources;
                if (value)
                {
                    ResourceLocator.SetColorScheme(appResources, ResourceLocator.DarkColorScheme);

                    appResources.MergedDictionaries.Add(
                        new ResourceDictionary()
                        {
                            Source = new Uri("pack://application:,,,/AdonisUI;component/ColorSchemes/dark.xaml")
                        });
                    appResources.MergedDictionaries.Add(
                        new ResourceDictionary()
                        {
                            Source = new Uri("pack://application:,,,/AdonisUI.ClassicTheme;component/Resources.xaml")
                        });
                }
                else
                {
                    ResourceLocator.RemoveAdonisResources(appResources);
                }
            }
        }

        public static readonly Color Background = GetColor("#3D3D3D");
        public static readonly Brush BackgroundBrush = new SolidColorBrush(Background);
        public static readonly Color LighterBackground = GetColor("#454545");
        public static readonly Brush LighterBackgroundBrush = new SolidColorBrush(LighterBackground);
        public static readonly Color ControlText = Color.FromRgb(153, 153, 153);
        public static readonly Brush ControlTextBrush = new SolidColorBrush(ControlText);

        private static readonly BrushConverter brushConverter = new BrushConverter();
        public static Brush GetBrush(string hex) => (Brush)brushConverter.ConvertFromString(hex);
        public static Color GetColor(string hex) => (Color)ColorConverter.ConvertFromString(hex);

        public static void UpdateTheme()
        {
            if (SystemParameters.HighContrast)
            {
                UseAdonisDarkTheme = false;
                SetResource("Theme_Background", SystemColors.AppWorkspaceBrush);
                SetResource("Theme_WhiteBackground", SystemColors.ControlBrush);
                SetResource("Theme_ToolWindowBackground", SystemColors.ControlBrush);
            }
            else if (UseDarkTheme)
            {
                SetResource("Theme_Background", LighterBackgroundBrush);
                SetResource("Theme_WhiteBackground", BackgroundBrush);
                SetResource("Theme_ToolWindowBackground", LighterBackgroundBrush);
                SetResource("Theme_InfoBarBackground", GetBrush("#202040"));

                UseAdonisDarkTheme = true;

                SetResource(SystemColors.ControlBrushKey, LighterBackgroundBrush);
                SetResource(SystemColors.ControlTextBrushKey, ControlTextBrush);
                SetResource(SystemColors.WindowBrushKey, BackgroundBrush);
                SetResource(SystemColors.MenuBarBrushKey, LighterBackgroundBrush);
                SetResource(SystemColors.MenuHighlightBrushKey, LighterBackgroundBrush);
                SetResource(SystemColors.MenuTextBrushKey, ControlTextBrush);
                SetResource(SystemColors.MenuBrushKey, BackgroundBrush);
                SetResource(SystemColors.MenuBarColorKey, Background);
                SetResource(SystemColors.MenuHighlightColorKey, LighterBackground);
                SetResource(SystemColors.MenuTextColorKey, ControlText);
                SetResource(SystemColors.MenuColorKey, Background);
                return;
            }
            else
            {
                UseAdonisDarkTheme = false;
                SetResource("Theme_Background", new SolidColorBrush(Color.FromRgb(238, 238, 242)));
                SetResource("Theme_WhiteBackground", Brushes.White);
                SetResource("Theme_ToolWindowBackground", Brushes.WhiteSmoke);
            }

            SetResource(SystemColors.ControlBrushKey, SystemColors.ControlBrush);
            SetResource(SystemColors.ControlTextBrushKey, SystemColors.ControlTextBrush);
            SetResource(SystemColors.WindowBrushKey, SystemColors.WindowBrush);
            SetResource(SystemColors.MenuBarBrushKey, SystemColors.MenuBarBrush);
            SetResource(SystemColors.MenuHighlightBrushKey, SystemColors.MenuHighlightBrush);
            SetResource(SystemColors.MenuTextBrushKey, SystemColors.MenuTextBrush);
            SetResource(SystemColors.MenuBrushKey, SystemColors.MenuBrush);
            SetResource(SystemColors.MenuBarColorKey, SystemColors.MenuBarColor);
            SetResource(SystemColors.MenuHighlightColorKey, SystemColors.MenuHighlightColor);
            SetResource(SystemColors.MenuTextColorKey, SystemColors.MenuTextColor);
            SetResource(SystemColors.MenuColorKey, SystemColors.MenuColor);
            SetResource("Theme_InfoBarBackground", SystemColors.InfoBrush);
        }

        private static void SetResource(object key, object value)
        {
            Application.Current.Resources[key] = value;
        }
    }
}