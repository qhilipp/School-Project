using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.ExamFolder;
using School_Project.Tabs.GradesFolder;
using School_Project.Tabs.HomeworkFolder;
using School_Project.Tabs.NotesFolder;
using School_Project.Tabs.PracticeFolder;
using School_Project.Tabs.Templates;
using School_Project.Tabs.TimetableFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.SubjectsFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectInfo : ContentPage {

        public delegate void FinishDelegate();
        public FinishDelegate Finish;

        public SubjectInfo(Subject subject) {
            InitializeComponent();

            homeworkHeader.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    (App.Current.MainPage as MainPage).Select(2);
                    Navigation.PopModalAsync();
                })
            });

            examHeader.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    (App.Current.MainPage as MainPage).Select(1);
                    Navigation.PopModalAsync();
                })
            });

            appointmentHeader.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    (App.Current.MainPage as MainPage).Select(0);
                    Navigation.PopModalAsync();
                })
            });

            themeHeader.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    (App.Current.MainPage as MainPage).Select(2);
                    Navigation.PopModalAsync();
                })
            });

            gradesHeader.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    (App.Current.MainPage as MainPage).Select(6);
                    Navigation.PopModalAsync();
                })
            });

            name.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    Finish?.Invoke();
                    Navigation.PopModalAsync();
                })
            });

            exams.GetAddpage += () => {
                return new AddExam(subject: subject);
            };

            homework.GetAddpage += () => {
                return new AddHomework(subject: subject);
            };

            appointments.GetAddpage += () => {
                return new AddAppointment(subject: subject);
            };

            appointments.GetElementView += (appointment) => {
                return new AppointmentView { Element = appointment };
            };

            themes.AddItem += (theme) => {
                theme.Subject = subject;
                Managers.ThemeManager.Save(theme);
                //grades.Content = subject.Addition ? null : new GradesPanel(subject);
            };

            notes.Click += (btn) => {
                ContentPage dirPage = new ContentPage { Content = new ThemeTab(subject, (theme) => { return new NoteTab(theme); }) { Finish = () => Refresh(subject) } };
                NavigationPage.SetHasNavigationBar(dirPage, false);
                Navigation.PushModalAsync(dirPage);
            };
            practices.Click += (btn) => {
                ContentPage dirPage = new ContentPage { Content = new ThemeTab(subject, (theme) => { return new PracticeTab(theme); }) { Finish = () => Refresh(subject) } };
                NavigationPage.SetHasNavigationBar(dirPage, false);
                Navigation.PushModalAsync(dirPage);
            };
            edit.Click += (btn) => {
                AddSubject edit = new AddSubject(subject);
                edit.AutoSave = true;
                edit.Finish += (s) => {
                    Managers.SubjectManager.Edit(subject, s);
                    subject = s;
                    Refresh(subject);
                    scroll.ScrollToAsync(0, 0, true);
                };
                Navigation.PushModalAsync(edit);
            };

            Refresh(subject);

        }

        private bool RefreshBasic(Subject subject) {
            if(subject == null) {
                this.FadeTo(0, 1000);
                Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                    Navigation.PopModalAsync();
                    return false;
                });
                return false;
            }
            gradesLayout.IsVisible = !subject.Addition;
            name.Text = subject.Name;
            withat.Text = (subject.Teacher == "" ? "" : "mit " + subject.Teacher + " ") + (subject.Room == "" ? "" : "in " + subject.Room);
            if(withat.Text != "")
                withat.Text = withat.Text.ToUpper()[0] + withat.Text.Substring(1);
            description.Text = (subject.LK ? "LK " : "") + (subject.Abi ? "Abitur-Fach " : "") + (subject.Written ? "Schriftlich " : "") + (subject.Addition ? "Zusatz-Fach" : "");
            return true;
        }

        public void Refresh(Subject subject) {
            if(!RefreshBasic(subject)) return;
            homework.Clear();
            foreach(Homework element in Managers.HomeworkManager.Load()) {
                if(element.Subject.ToKey() == subject.ToKey())
                    homework.Add(element);
            }

            exams.Clear();
            foreach(Exam element in Managers.ExamManager.Load()) {
                if(element.Subject.ToKey() == subject.ToKey())
                    exams.Add(element);
            }

            themes.Clear();
            foreach(Theme element in Managers.ThemeManager.Load(subject))
                themes.Add(element);

            appointments.Clear();
            foreach(var pair in Managers.AppointmentManager.Elements) {
                if(pair.Value.Subject.ToKey() == subject.ToKey())
                    appointments.Add(pair.Value);
            }

            grades.Content = subject.Addition ? null : new GradesPanel(subject);
        }
        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            layout.WidthRequest = width / height < 1 ? width : height;
        }

    }
}