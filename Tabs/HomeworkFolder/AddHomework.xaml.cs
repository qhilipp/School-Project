using Plugin.LocalNotification;
using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.HomeworkFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddHomework : AddPage<Homework> {

        public AddHomework(Homework old = null, Subject subject = null) : base(old) {
            InitializeComponent();
            if(old == null) {
                int day = (int) DateTime.Today.DayOfWeek - 1;
                int time = (int) DateTime.Now.TimeOfDay.TotalMinutes;
                Appointment appointment = Managers.GetAppointmentAt(day, time);
                if(appointment != null) {
                    selectSubject.SelectedElement = appointment.Subject;
                    Appointment a = Managers.GetNextAppointmentFrom(day, time);
                    int currentTime = day * 1440 + time;
                    int wantedTime = a.Day * 1440 + a.Start;
                    int nDay;
                    if(wantedTime > currentTime) {
                        nDay = a.Day - day;
                    } else {
                        nDay = 7 - day + a.Day;
                    }
                    selectDate.Date = DateTime.Today.AddDays(nDay);
                    selectTime.Time = a.Start;
                } else {
                    selectDate.Date = DateTime.Today.AddDays(1);
                    selectTime.Time = time;
                }
                if(subject != null) {
                    selectSubject.SelectedElement = subject;
                }
                if(Managers.SubjectManager.Elements.Count == 1) selectSubject.SelectedElement = Managers.SubjectManager.Elements.Values.ToList()[0];
            }
        }

        protected override string Check() {
            string errorMsg = "";
            if(selectSubject.SelectedElement == null) {
                errorMsg = "Du musst ein Fach auswählen";
                selectSubject.Error();
            }
            if(!selectTime.IsSelected) {
                errorMsg = "Du musst einen Zeitpunkt auswählen";
                selectTime.Error();
            }
            return errorMsg;
        }
        protected override void Construct() {
            Element.Subject = selectSubject.SelectedElement;
            Element.Date = selectDate.Date.AddMinutes(selectTime.Time).ToFileTime();
            Element.Description = description.Text;
            if(Settings.IsMobile) {
                NotificationCenter.Current.Show(new NotificationRequest {
                    Title = "Hausaufgabe",
                    Description = $"Die {Element.Subject.Name} Hausaufgabe muss bald abgegeben werden",
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
            DateTime dateTime = DateTime.FromFileTime(Old.Date);
            selectDate.Date = dateTime;
            selectTime.Time = dateTime.Hour * 60 + dateTime.Minute;
            description.Text = Old.Description;
        }

    }
}