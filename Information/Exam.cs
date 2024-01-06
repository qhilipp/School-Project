using School_Project.Management;
using School_Project.Tabs.ExamFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Information {
    public class Exam : IElement<Exam> {

        private Subject subject;
        public Subject Subject { 
            get {
                return subject;
            }
            set {
                subject = value.Clone();
            }
        }
        public long Date { get; set; }
        public string Themes { get; set; }
        public int Duration { get; set; }
        public string Room { get; set; }
        public string Teacher { get; set; }

        public Exam Clone() {
            return new Exam {
                Subject = Subject.Clone(),
                Date = Date,
                Themes = Themes
            };
        }

        public override string ToString() {
            return $"{Subject.ToKey()},{Room},{Teacher},{Date},{Themes},{Duration}";
        }

        public void FromStrings(string[] props) {
            Subject = Managers.SubjectManager.Elements[props[0]].Clone();
            Room = props[1];
            Teacher = props[2];
            Date = long.Parse(props[3]);
            Themes = props[4];
            Duration = int.Parse(props[5]);
        }

        public FileManager<Exam> GetManager() {
            return Managers.ExamManager;
        }

        public string GetName(bool plural = false) {
            return plural ? "Klausuren" : "Klausur";
        }

        public string ToKey() {
            return Date + "";
        }

        public ElementView<Exam> GetView() {
            return new ExamView { Element = this };
        }

        public int GetScore() {
            int diff = (int) (DateTime.Now.ToFileTime() - Date);
            int res = diff < 0 ? -diff : diff / 100;
            return res;
        }

        public AddPage<Exam> GetAddPage() {
            return new AddExam();
        }
    }
}