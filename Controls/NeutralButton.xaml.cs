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
    public partial class NeutralButton : Frame {

        public string Text {
            get {
                return label.Text;
            }
            set {
                label.Text = value;
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

        public delegate void ClickDelegate(NeutralButton btn);
        public ClickDelegate Click;

        public NeutralButton() {
            InitializeComponent();
            CornerRadius = Settings.CornerRadius;
            BackgroundColor = ColorManager.OverlayColor;
            GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => {
                BackgroundColor = ColorManager.OverlayColor;
                Click?.Invoke(this);
            }) });
        }

        public void Error() {
            BackgroundColor = ColorManager.ErrorColor;
        }

    }
}