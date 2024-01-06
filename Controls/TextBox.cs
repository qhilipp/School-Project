using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Controls {
    public class TextBox : Frame {

        public delegate void TextChangeDelegate(string txt);
        public TextChangeDelegate TextChange;

        private CustomEntry entry;

        public string Text {
            get {
                return entry.Text;
            }
            set {
                entry.Text = value;
            }
        }
        public string Placeholder {
            get {
                return entry.Placeholder;
            }
            set {
                entry.Placeholder = value;
            }
        }
        public Keyboard Keyboard {
            get {
                return entry.Keyboard;
            }
            set {
                entry.Keyboard = value;
            }
        }
        public int MaxLength {
            get {
                return entry.MaxLength;
            }
            set {
                entry.MaxLength = value;
            }
        }

        private int input = 0;
        public int Input {
            get {
                return input;
            }
            set {
                input = value;
                if(input == 0) {
                    entry.Keyboard = Keyboard.Default;
                } else if(input == 1) {
                    entry.Keyboard = Keyboard.Numeric;
                } else if(input == 2) {
                    entry.Keyboard = Keyboard.Telephone;
                } else if(input == 3) {
                    entry.Keyboard = Keyboard.Numeric;
                }
            }
        }

        public static int String = 0;
        public static int Int = 1;
        public static int Grade = 2;
        public static int Points = 3;

        private bool inEvent = false;

        public TextBox() {

            entry = new CustomEntry();
            entry.IsSpellCheckEnabled = false;
            entry.IsTextPredictionEnabled = false;
            Content = entry;

            //Padding = new Thickness(15, 10);
            BackgroundColor = ColorManager.OverlayColor;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            CornerRadius = Settings.CornerRadius;

            entry.FontSize = Settings.FontSize;
            entry.TextColor = ColorManager.TextColor;
            entry.PlaceholderColor = ColorManager.PlaceholderColor;
            Text = "";
            entry.PropertyChanged += (s, e) => {
                if(e.PropertyName == "BackgroundColor" && entry.BackgroundColor != Color.Transparent) {
                    entry.BackgroundColor = Color.Transparent;
                }
            };
            entry.Unfocused += (s, e) => {
                entry.BackgroundColor = Color.Transparent;
            };
            entry.TextChanged += (s, e) => {
                entry.PlaceholderColor = ColorManager.PlaceholderColor;
                BackgroundColor = ColorManager.OverlayColor;
                if(!inEvent) inEvent = true;
                string legal = "";
                if(Input == 0) {
                    legal = entry.Text;
                } else if(Input == 1) {
                    foreach(char c in entry.Text) {
                        if(c >= '0' && c <= '9') legal += c;
                    }
                } else if(Input == 2) {
                    legal = entry.Text;
                    if(legal.Length > 0) {
                        if(legal[0] < '1') {
                            legal = legal.Insert(0, "1");
                            legal = legal.Remove(1, 1);
                        }
                        if(legal.Length == 2) {
                            if(legal[1] != '+' && legal[1] != '-' || legal[0] == '6') {
                                legal = legal.Remove(1, 1);
                            }
                        } else if(legal.Length > 2) {
                            legal = legal.Substring(0, 2);
                        }
                    }
                } else if(Input == 3) {
                    foreach(char c in entry.Text) {
                        if(c >= '0' && c <= '9') legal += c;
                    }
                    if(legal != "" && int.Parse(legal) > 15) legal = "15";
                }
                if(inEvent) Text = legal;
                inEvent = false;
                TextChange?.Invoke(legal);
            };
        }

        public void Error() {
            BackgroundColor = ColorManager.ErrorColor;
        }

    }
}
