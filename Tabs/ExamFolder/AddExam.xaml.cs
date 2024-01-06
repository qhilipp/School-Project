using Plugin.LocalNotification;
using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.ExamFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExam : AddPage<Exam> {
        public AddExam(Exam old = null, Subject subject = null) : base(old) {
            InitializeComponent();
            selectSubject.Change += () => {
                room.Text = selectSubject.SelectedElement.Room;
                teacher.Text = selectSubject.SelectedElement.Teacher;
            };
            if(old == null) {
                int day = (int) DateTime.Today.DayOfWeek - 1;
                int time = (int) DateTime.Now.TimeOfDay.TotalMinutes;
                Appointment appointment = Managers.GetAppointmentAt(day, time);
                if(appointment != null) {
                    selectSubject.SelectedElement = appointment.Subject;
                    selectTime.Time = appointment.Start;
                } else {
                    selectTime.Time = time;
                }
                selectDate.Date = DateTime.Today.AddDays(7);
                if(subject != null) {
                    selectSubject.SelectedElement = subject;
                }
                if(Managers.SubjectManager.Elements.Count == 1) selectSubject.SelectedElement = Managers.SubjectManager.Elements.Values.ToList()[0];
            }
        }

        protected override string Check() {
            string errorMsg = "";
            if(selectSubject.SelectedElement == null) {
                selectSubject.Error();
                errorMsg = "Du musst ein Fach auswählen";
            }
            return errorMsg;
        }
        protected override void Construct() {
            Element.Subject = selectSubject.SelectedElement.Clone();
            Element.Room = room.Text;
            Element.Teacher = teacher.Text;
            Element.Date = selectDate.Date.AddMinutes(selectTime.Time).ToFileTime();
            Element.Themes = themes.Text;
            Managers.ThemeManager.Save(new Theme { Name = Element.Themes, Subject = Element.Subject });
            if(Settings.IsMobile) {
                NotificationCenter.Current.Show(new NotificationRequest {
                    Title = "Klausur",
                    Description = $"Die {Element.Subject.Name} Klausur wird bald geschrieben",
                    Schedule = {
                        NotifyTime = DateTime.FromFileTime(Element.Date)
                    }
                });
            }
        }
        protected override void Delete() {}
        protected override void Edit() {}
        protected override void Load() {
            selectSubject.SelectedElement = Old.Subject;
            room.Text = Old.Room;
            teacher.Text = Old.Teacher;
            selectDate.Date = DateTime.FromFileTime(Old.Date);
            selectTime.Time = (int) DateTime.FromFileTime(Old.Date).TimeOfDay.TotalMinutes;
            themes.Text = Old.Themes;
        }

    }
}