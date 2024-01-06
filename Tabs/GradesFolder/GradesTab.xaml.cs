using School_Project.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Project.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using School_Project.Management;
using School_Project.Tabs.NotesFolder;

namespace School_Project.Tabs.GradesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesTab : ContentPage {

        public delegate void FinishDelegate();
        public FinishDelegate Finish;

        public GradesTab(Subject subject) {
            InitializeComponent();
            GradesPanel panel = new GradesPanel(subject) { VerticalOptions = LayoutOptions.CenterAndExpand };
            layout.Children.Add(panel);
            name.Text = subject.Name + " Noten";
            name.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => {
                Finish?.Invoke();
                Navigation.PopModalAsync();
            }) });
            addTheme.Click += (btn) => {
                panel.OpenAddTheme();
            };
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            double ratio = width / height;
            addTheme.HorizontalOptions = ratio < 1 ? LayoutOptions.Fill : LayoutOptions.Center;
            if(ratio > 1) {
                layout.WidthRequest = height;
            }
        }

    }
}