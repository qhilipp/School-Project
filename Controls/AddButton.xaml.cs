using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddButton : Frame {

        private string text = "Hinzufügen";
        public string Text {
            get {
                return text;
            }
            set {
                text = value;
                label.Text = text;
            }
        }

        public delegate void ClickDelegate(AddButton btn);
        public ClickDelegate Click;

        public AddButton() {
            InitializeComponent();
            label.FontSize = Management.Settings.FontSize * 1.2;
            CornerRadius = Management.Settings.CornerRadius;
            GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => {               
                Click?.Invoke(this);
            }) });
        }
    }
}