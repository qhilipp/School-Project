using School_Project.Management;
using School_Project.Views.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorSelector : NeutralButton {

        private int color;
        public int Color {
            get {
                return color;
            }
            set {
                color = value;
                TextColor = ColorManager.GetColor(color);
            }
        }
        
        public ColorSelector() {
            InitializeComponent();
            Color = 8;
            Click += (btn) => {
                ColorPickerPage cpp = new ColorPickerPage();
                cpp.Finish += (int color) => {
                    Color = color;
                };
                Navigation.PushModalAsync(cpp);
            };
        }
    }
}