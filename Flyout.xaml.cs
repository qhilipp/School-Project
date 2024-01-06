using School_Project.Management;
using School_Project.Tabs.ExamFolder;
using School_Project.Tabs.GradesFolder;
using School_Project.Tabs.HomeworkFolder;
using School_Project.Tabs.NotesFolder;
using School_Project.Tabs.PracticeFolder;
using School_Project.Tabs.SemesterFolder;
using School_Project.Tabs.SubjectsFolder;
using School_Project.Tabs.TimetableFolder;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Flyout : ContentPage {

        public delegate void SelectDelegate(View content);
        public SelectDelegate Select;

        private Dictionary<string, Type> tabs = new Dictionary<string, Type>();

        public Flyout() {
            InitializeComponent();
            frame.CornerRadius = Settings.CornerRadius;
            AddTab<TimetableTab>(timetable);
            AddTab<ExamTab>(exams);
            AddTab<HomeworkTab>(homework);
            AddTab<SubjectTab>(subjects);
            AddTab<NoteSubjectTab>(notes);
            AddTab<PracticeSubjectTab>(practice);
            AddTab<GradeSubjectTab>(grades);
            AddTab<SemesterTab>(semester);
        }

        private void AddTab<T>(Label lbl) where T : View, new() {
            if(Device.RuntimePlatform == Device.Android ||Device.RuntimePlatform == Device.iOS) lbl.FontSize = Settings.FontSize * 1.2;
            lbl.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => {
                SelectTab(lbl.Text);
            }) });
            tabs[lbl.Text] = typeof(T);
        }
        
        private void Highlight(Label lbl)  {
            foreach(var l in layout.Children) {
                ((Label) l).FontAttributes = FontAttributes.None;
            }
            lbl.FontAttributes = FontAttributes.Bold;
        }

        private void SelectTab(Label lbl) {
            Highlight(lbl);
            Select?.Invoke((View) Activator.CreateInstance(tabs[lbl.Text]));
        }

        public void SelectTab(string name) {
            Label lbl = null;
            foreach(View v in layout.Children) {
                if((v as Label).Text == name) {
                    lbl = v as Label;
                    break;
                }
            }
            SelectTab(lbl);
        }

        public void SelectTab(int index) {
            SelectTab((Label) layout.Children[index]);
        }

    }
}