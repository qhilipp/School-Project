using School_Project.Information;
using System.Collections.Generic;
using System.Linq;

namespace School_Project.Management {
    public class Managers {

        public static FileManager<Subject> SubjectManager;
        public static FileManager<Appointment> AppointmentManager;
        public static FileManager<Homework> HomeworkManager;
        public static FileManager<Exam> ExamManager;
        public static ThemeManager ThemeManager;
        public static NoteManager NoteManager;
        public static PracticeManager PracticeManager;

        public static bool Init() {
            Settings.Init();
            SubjectManager = new FileManager<Subject>();
            AppointmentManager = new FileManager<Appointment>();
            HomeworkManager = new FileManager<Homework>();
            ExamManager = new FileManager<Exam>();
            ThemeManager = new ThemeManager();
            NoteManager = new NoteManager();
            PracticeManager = new PracticeManager();
            bool b = SubjectManager.Init();
            AppointmentManager.Init();
            HomeworkManager.Init();
            ExamManager.Init();
            NoteManager.Init();
            PracticeManager.Init();
            return b;
        }

        public static Appointment GetAppointmentAt(int day, int time) {
            Appointment appointment = null;
            foreach(var pair in AppointmentManager.Elements) {
                if(pair.Value.Day == day) {
                    if(pair.Value.Start < time && pair.Value.Start + pair.Value.Duration > time) {
                        appointment = pair.Value;
                    }
                }
            }
            return appointment;
        }

        public static Appointment GetNextAppointmentFrom(int day, int time) {
            Appointment appointment = GetAppointmentAt(day, time);
            List<Appointment> appointments = new List<Appointment>();
            foreach(var pair in AppointmentManager.Elements) {
                if(pair.Value.Subject.ToKey() == appointment.Subject.ToKey()) appointments.Add(pair.Value);
            }
            appointments = (from a in appointments orderby a.Day * 1440 + a.Start select a).ToList();
            int index = appointments.IndexOf(appointment) + 1;
            if(index >= appointments.Count) index = 0;
            return appointments[index];
        }

    }
}