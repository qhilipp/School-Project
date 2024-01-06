using School_Project.Management;
using School_Project.Tabs.HomeworkFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace School_Project.Information {
    public class Homework : IElement<Homework> {

        public Subject Subject {
            get; set;
        }

        public long Date {
            get; set;
        }

        public string Description {
            get; set;
        }

        public bool Finished {
            get; set;
        }

        public Homework Clone() {
            return new Homework() {
                Subject = Subject.Clone(),
                Date = Date,
                Description = Description,
                Finished = Finished
            };
        }

        public void FromStrings(string[] props) {
            Subject = Managers.SubjectManager.Elements[props[0]];
            Date = long.Parse(props[1]);
            Finished = props[2] == "1";
            Description = props[3];
        }

        public FileManager<Homework> GetManager() {
            return Managers.HomeworkManager;
        }

        public string GetName(bool plural) {
            return plural ? "Aufgaben" : "Aufgabe";
        }

        public ElementView<Homework> GetView() {
            return new HomeworkView { Element = this };
        }

        public string ToKey() {
            return Date + "";
        }

        public override string ToString() {
            return $"{Subject.ToKey()},{Date},{(Finished ? "1" : "0")},{Description}";
        }

        public int GetScore() {
            int res = 0;
            if(Finished) res = 100000000;
            int diff = (DateTime.Now - DateTime.FromFileTime(Date)).Minutes;
            res += diff < 0 ? -diff : diff / 100;
            return -res;
        }

        public AddPage<Homework> GetAddPage() {
            return new AddHomework();
        }
    }
}
