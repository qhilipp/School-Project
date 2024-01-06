using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.NotesFolder;
using School_Project.Tabs.SubjectsFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace School_Project.Tabs.GradesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradeSubjectTab : StackLayout {
        public GradeSubjectTab() {
            InitializeComponent();
            list.GetElementView += (subject) => {
                return new SubjectGradeView() { Element = subject };
            };
            list.Click += (view) => {
                GradesTab page = new GradesTab(view.Element) { Finish = Update, BackgroundColor = ColorManager.BackgroundColor };
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushModalAsync(page);
            };
            Update();
        }

        public void Update() {
            list.Clear();
            var relevant = from s in Managers.SubjectManager.Load() where !s.Addition select s;
            relevant = relevant.OrderBy(s => -s.GetScore());
            foreach(Subject subject in relevant) list.Add(subject);
            double sum = 0, bestSum = 0, worstSum = 0;
            int known = 0, total = 0;
            foreach(Subject s in list.GetItems()) {
                if(Managers.ThemeManager.Load(s).Count != 0) {
                    if(s.Known != 0) {
                        sum += s.Avg * (s.LK ? 2 : 1);
                        known += s.LK ? 2 : 1;
                    }
                    bestSum += s.Best * (s.LK ? 2 : 1);
                    worstSum += s.Worst * (s.LK ? 2 : 1);
                    total += s.LK ? 2 : 1;
                }
            }
            double av = Math.Round(sum / known, 2);
            int format = Settings.Format;
            if(known != 0) avg.Text = $"Durchschnitt: {(format == 0 ? av + "" : GradesPanel.ToGrade(av) + "")} {(format == 0 ? (av == 1 ? "Punkt" : "Punkte") : "")}";
            avg.IsVisible = known != 0;
            double worst = Math.Round(worstSum / total, 2);
            double best = Math.Round(bestSum / total, 2);
            span.Text = $"Zwischen {(format == 0 ? worst + "" : GradesPanel.ToGrade(worst) + "")} und {(format == 0 ? best + "" : GradesPanel.ToGrade(best) + "")} {(format == 0 ? "Punkten" : "")}";
            span.IsVisible = worst != best;
            if(total == 0) span.IsVisible = false;
            if(list.GetItems().Count == 0) {
                avg.IsVisible = false;
                span.IsVisible = false;
            }
        }

    }
}