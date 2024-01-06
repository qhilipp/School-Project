using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.TimetableFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAppointment : AddPage<Appointment> {
        public AddAppointment(int d = 0, Appointment old = null, Subject subject = null) : base(old) {
            InitializeComponent();
            day.Add("Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag", "Samstag", "Sonntag");
            day.SelectedIndex = d;
            if(old == null) {
                if(Managers.SubjectManager.Elements.Count == 1) selectSubject.SelectedElement = Managers.SubjectManager.Elements.Values.ToList()[0];
            }
            if(old == null && subject != null) selectSubject.SelectedElement = subject;
        }

        protected override string Check() {
            string errorMsg = "";
            if(selectSubject.SelectedElement == null) {
                errorMsg = "Du musst ein Fach auswählen";
                selectSubject.Error();
            }
            if(!time.IsSelected) {
                errorMsg = "Du musst einen Zeitpunkt auswählen";
                time.Error();
            }
            if(duration.Text == "" || duration.Text == null) {
                errorMsg = "Du musst eine Dauer angeben";
                duration.Error();
            } else if(int.Parse(duration.Text) < 5) {
                errorMsg = "Ein Termin muss min. 5 Minuten dauern";
                duration.Error();
            }
            foreach(KeyValuePair<string, Appointment> pair in Managers.AppointmentManager.Elements) {
                if(Old != null && pair.Key == Old.ToKey()) continue;
                if(pair.Value.Day == day.SelectedIndex) {
                    int s1 = pair.Value.Start;
                    int d1 = pair.Value.Duration;
                    int s2 = time.Time;
                    int d2 = int.Parse(duration.Text);
                    if(!(s1 >= s2 + d2 || s2 >= s1 + d1)) {
                        errorMsg = "Du hast bereits einen Termin in diesem Zeitabschnitt";
                        time.Error();
                        duration.Error();
                    }
                }
            }
            return errorMsg;
        }

        protected override void Construct() {
            Element.Subject = selectSubject.SelectedElement;
            Element.Start = time.Time;
            Element.Day = day.SelectedIndex;
            Element.Duration = int.Parse(duration.Text);
        }
        protected override void Delete() {}
        protected override void Edit() {}

        protected override void Load() {
            day.SelectedIndex = Old.Day;
            selectSubject.SelectedElement = Old.Subject;
            time.Time = Old.Start;
            duration.Text = Old.Duration + "";
        }
    }
}