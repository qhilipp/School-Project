using School_Project.Controls;
using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.NotesFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.GradesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradesPanel : ContentView {

        private Subject subject;

        public GradesPanel(Subject subject) {
            InitializeComponent();
            this.subject = subject;
            foreach(Theme theme in Managers.ThemeManager.Load(subject)) {
                themes.Children.Add(new GradeView(theme) { Change = Update });
            }
            format.Add("Punkte", "Zensuren");
            format.Change += () => {
                Settings.Format = format.SelectedIndex;
                Update();
                foreach(View v in themes.Children) (v as GradeView).Format = format.SelectedIndex;
            };
            format.SelectedIndex = Settings.Format;
            if(subject.Written) {
                layout.Children.Insert(layout.Children.IndexOf(format) + 1, new StackLayout {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                        new Label {
                            Text = "Mündliche Noten",
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        },
                        new Label {
                            Text = "Schriftliche Noten",
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
                });
            }
            if(Managers.ThemeManager.Load(subject).Count == 0) {
                Content = new StackLayout {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Children = {
                        new Label {
                            Text = "Keine Themen vorhanden",
                            HorizontalTextAlignment = TextAlignment.Center,
                            TextColor = ColorManager.PlaceholderColor,
                            FontSize = Settings.SmallFontSize
                        }
                    }
                };
            }
        }

        public void OpenAddTheme() {
            AddTheme add = new AddTheme(subject);
            add.Finish += (theme) => {
                Content = layout;
                themes.Children.Add(new GradeView(theme) { Change = Update });
                Update();
                layout.HeightRequest = 100;
            };
            Navigation.PushModalAsync(add);
        }

        private void Update() {
            if(Managers.ThemeManager.Load(subject).Count != 0) {
                int known = subject.Known;
                int fullyKnown = subject.FullyKnown;
                int av = subject.Avg;
                if(!subject.IsFullyKnown) {
                    int worst = subject.Worst;
                    int best = subject.Best;
                    span.Text = $"Zwischen {(format.SelectedIndex == 0 ? worst + "" : ToGrade(worst))} und {(format.SelectedIndex == 0 ? best + "" : ToGrade(best))} {(format.SelectedIndex == 0 ? "Punkten" : "")}";
                }
                if(known != 0) {
                    avg.Text = $"Durchschnitt: {(format.SelectedIndex == 0 ? av + "" : ToGrade(av))} {(format.SelectedIndex == 0 ? (av == 1 ? "Punkt" : "Punkte") : "")}";
                }
                span.IsVisible = fullyKnown != themes.Children.Count;
            } else {
                avg.IsVisible = false;
                span.IsVisible = false;
            }
        }

        public static int Round(double d) {
            if(d - (int) d >= .5) return (int) d + 1;
            return (int) d;
        }

        public static int RoundUp(double d) {
            if(d == (int) d) return (int) d;
            return (int) d + 1;
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            double ratio = width / height;
            if(ratio > 1) {
                layout.WidthRequest = height;
            }
        }

        public static string ToGrade(int points) {
            if(points <= 0) return "6";
            string[] nums = { "5", "4", "3", "2", "1" };
            string[] suf = { "+", "-", "" };
            return nums[(points - 1) / 3] + suf[points % 3];
        }

        public static double ToGrade(double points) {
            return Math.Round(-points / 3 + 6, 2);
        }

        public static int ToPoints(string grade) {
            if(grade == "6") return 0;
            int points = 16 - int.Parse(grade[0] + "") * 3;
            if(grade.Length == 2)
                points += grade[1] == '+' ? 2 : 0;
            else
                points += 1;
            return points;
        }

    }
}