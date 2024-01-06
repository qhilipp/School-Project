using School_Project.Management;
using School_Project.Tabs.Templates;
using School_Project.Tabs.TimetableFolder;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Information {
    public class Appointment : IElement<Appointment> {

        public int Start {
            get; set;
        }

        public int Duration {
            get; set;
        }

        public Subject Subject {
            get; set;
        }

        public int Day {
            get; set;
        }

        public void FromStrings(string[] props) {
            Start = int.Parse(props[0]);
            Duration = int.Parse(props[1]);
            Subject = Managers.SubjectManager.Elements[props[2]];
            Day = int.Parse(props[3]);
        }

        public string ToKey() {
            return $"{Day},{Start}";
        }

        public override string ToString() {
            return $"{Start},{Duration},{Subject.ToKey()},{Day}";
        }

        public Appointment Clone() {
            return new Appointment() { Start = Start, Duration = Duration, Subject = Subject.Clone(), Day = Day };
        }

        public FileManager<Appointment> GetManager() {
            return Managers.AppointmentManager;
        }

        public string GetName(bool plural) {
            return plural ? "Termine" : "Termin";
        }

        public ElementView<Appointment> GetView() {
            return new AppointmentView { Element = this };
        }

        public int GetScore() {
            return Day * 10000 + Start;
        }

        public string GetTime() {
            string startH = Start / 60 + "";
            string startM = Start % 60 + "";
            string endH = (Start + Duration) / 60 + "";
            string endM = (Start + Duration) % 60 + "";
            return startH + ":" + startM + (startM.Length == 1 ? "0" : "") + " Uhr bis " + endH + ":" + endM + (endM.Length == 1 ? "0" : "") + " Uhr";
        }
        public string GetDay() {
            string[] days = { "Montag", "Dienstag", "Mittwoch", "Donenrstag", "Freitag", "Samstag", "Sonntag"};
            return days[Day];
        }

        public AddPage<Appointment> GetAddPage() {
            return new AddAppointment();
        }

    }
}