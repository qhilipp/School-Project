using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Views.Subjects {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPickerPage : ContentPage {

        public delegate void FinishDelegate(int color);
        public FinishDelegate Finish;

        public ColorPickerPage() {
            InitializeComponent();
            layout.ColumnSpacing = Settings.Spacing;
            layout.RowSpacing = Settings.Spacing;
            for(int i = 0; i < ColorManager.Length; i++) {
                Frame f = new Frame() { BackgroundColor = ColorManager.GetColor(i), CornerRadius = Settings.CornerRadius };
                f.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => {
                    Finish?.Invoke(layout.ColumnDefinitions.Count * Grid.GetRow(f) + Grid.GetColumn(f));
                    Navigation.PopModalAsync();
                }) });
                layout.Children.Add(f, i % 2, i / 2);
            }
        
        }
    }
}