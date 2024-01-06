using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Management {
    public class ColorManager {

        private static bool darkMode = Application.Current.RequestedTheme == OSAppTheme.Dark;
        public static bool DarkMode {
            get {
                return darkMode;
            }
        }

        public static Color TextColor {
            get {
                return darkMode ? Color.FromHex("#ffffff") : Color.FromHex("#000000");
            }
        }

        public static Color BackgroundColor {
            get {
                return darkMode ? Color.FromHex("#000000") : Color.FromHex("#ffffff");
            }
        }

        public static Color OverlayColor {
            get {
                return darkMode ? Color.FromHex("#101319") : Color.FromHex("#ecf0f1");
            }
        }

        public static Color AccentColor1 {
            get {
                return darkMode ? Color.FromHex("#394bc3") : Color.FromHex("#707edd");
            }
        }

        public static Color AccentColor2 {
            get {
                return darkMode ? Color.FromHex("#893bca") : Color.FromHex("#ad7ad7");
            }
        }

        public static Color ErrorColor {
            get {
                return darkMode ? Color.FromHex("#220505") : Color.FromHex("#b26f6f");
            }
        }

        public static Color WarningColor {
            get {
                return darkMode ? Color.FromHex("#cc9918") : Color.FromHex("ffb700");
            }
        }

        public static Color PlaceholderColor {
            get {
                return darkMode ? Color.FromRgba(1, 1, 1, .2) : Color.FromRgba(0, 0, 0, .2);
            }
        }

        public static int Length {
            get {
                return lightColors.Length;
            }
        }

        private static Color[] lightColors = { 
            Color.FromHex("#1abc9c"), 
            Color.FromHex("#2ecc71"), 
            Color.FromHex("#3498db"), 
            Color.FromHex("#9b59b6"), 
            Color.FromHex("#34495e"), 
            Color.FromHex("#f1c40f"),
            Color.FromHex("#e67e22"),
            Color.FromHex("#e74c3c"),
            Color.FromHex("#ecf0f1"),
            Color.FromHex("#95a5a6"),
        };

        private static Color[] darkColors = {
            Color.FromHex("#16a085"),
            Color.FromHex("#27ae60"),
            Color.FromHex("#2980b9"),
            Color.FromHex("#8e44ad"),
            Color.FromHex("#2c3e50"),
            Color.FromHex("#f39c12"),
            Color.FromHex("#d35400"),
            Color.FromHex("#c0392b"),
            Color.FromHex("#bdc3c7"),
            Color.FromHex("#7f8c8d"),
        };

        public static Color[] Colors {
            get {
                return darkMode ? darkColors : lightColors;
            }
        }

        public static Color GetColor(int i) {
            return darkMode ? darkColors[i] : lightColors[i];
        }

    }
}
