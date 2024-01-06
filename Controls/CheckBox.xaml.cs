using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckBox : ContentView {

        public Color Color {
            get {
                return checkBox.Color;
            }
            set {
                checkBox.Color = value;
                label.TextColor = value;
            }
        }

        public Color BoxColor {
            get {
                return checkBox.Color;
            }
            set {
                checkBox.Color = value;
            }
        }

        public Color TextColor {
            get {
                return label.TextColor;
            }
            set {
                label.TextColor = value;
            }
        }

        private bool isChecked;
        public bool IsChecked {
            get {
                return isChecked;
            }
            set {
                SetState(value);
                Change?.Invoke(isChecked);
            }
        }

        public string Text {
            get {
                return label.Text;
            }
            set {
                label.Text = value;
            }
        }

        public delegate void ChangeDelegate(bool now);
        public ChangeDelegate Change;

        public CheckBox() {
            InitializeComponent();
            BoxColor = ColorManager.AccentColor1;
            GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => {
                if(IsEnabled) {
                    IsChecked = !IsChecked;
                }
            }) });
            checkBox.CheckedChanged += (s, e) => {
                if(IsEnabled || checkBox.IsChecked != IsChecked) {
                    isChecked = checkBox.IsChecked;
                    Change?.Invoke(isChecked);
                }
            };
        }

        public void SetState(bool value) {
            isChecked = value;
            checkBox.IsChecked = isChecked;
        }

    }
}