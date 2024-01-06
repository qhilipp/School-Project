using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.SemesterFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SemesterTab : Templates.ContentTab {
        public SemesterTab() {
            InitializeComponent();
            for(int i = 0; i < Settings.Count; i++) {
                currentSemester.Add("Semester " + (i + 1));
            }
            currentSemester.Change += () => {
                Settings.Semester = currentSemester.SelectedIndex;
            };
            AddItem += async () => {
                bool transfer = false;
                if(Managers.SubjectManager.Elements.Count != 0)
                    transfer = await Application.Current.MainPage.DisplayAlert("School Project", "Möchtest Du vorhandene Fächer aus dem aktuellen Semester in das neue Semester übertragen?", "Ja", "Nein");
                var subjects = Settings.Create(transfer);
                currentSemester.Add("Semester " + Settings.Count);
                currentSemester.SelectedIndex = Settings.Count + 1;
                if(subjects != null)
                    foreach(var pair in subjects) {
                        Managers.SubjectManager.Save(pair.Value);
                    }
                Managers.ThemeManager.Init();
                Managers.NoteManager.Init();
                Managers.PracticeManager.Init();
            };
            currentSemester.SelectedIndex = Settings.Semester;
        }

    }
}