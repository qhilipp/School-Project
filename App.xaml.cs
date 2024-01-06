using School_Project.Information;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * Started on Thursday, First of June 2021
 * by Philipp Kathöfer
 */

namespace School_Project {
    public partial class App : Application {
        public App() {
            bool b = Management.Managers.Init();
            InitializeComponent();
            try {
                MainPage = new MainPage(!b);
            } catch(Exception e) {
                Error(e);
            }
        }

        private async void Error(Exception e) {
            bool b = await MainPage.DisplayAlert("School Project Fehler", "Es gab ein Problem, möchtest Du das Problem Philipp mitteilen?", "Ja", "Nein");
            if(b) {
                await Xamarin.Essentials.Email.ComposeAsync("Problem mit School Project", $"Hi Philipp, es gab ein Problem mit School Project: {e}", "philippkathoefer@gmail.com");
            }
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
