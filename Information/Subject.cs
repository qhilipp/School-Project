using School_Project.Management;
using School_Project.Tabs.GradesFolder;
using School_Project.Tabs.SubjectsFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Information {
    public class Subject : IElement<Subject> {

        public string Name {
            get; set;
        }

        public string Teacher {
            get; set;
        }

        public string Room {
            get; set;
        }

        public int Color {
            get; set;
        }

        public bool LK {
            get; set;
        }

        public bool Written {
            get; set;
        }

        public bool Abi {
            get; set;
        }

        public bool Addition {
            get; set;
        }

        public int Avg {
            get {
                double sum = 0;
                int count = 0;
                foreach(Theme theme in Managers.ThemeManager.Load(this)) {
                    if(theme.IsKnown) {
                        sum += theme.Avg;
                        count++;
                    }
                }
                return GradesPanel.Round(sum / count);
            }
        }

        public int Best {
            get {
                List<Theme> themes = Managers.ThemeManager.Load(this);
                double sum = 0;
                foreach(Theme theme in themes) {
                    sum += theme.Best;
                }
                return GradesPanel.RoundUp(sum / themes.Count);
            }
        }

        public int Worst {
            get {
                List<Theme> themes = Managers.ThemeManager.Load(this);
                int sum = 0;
                foreach(Theme theme in themes) {
                    sum += theme.Worst;
                }
                return sum / themes.Count;
            }
        }

        public int Known {
            get {
                int count = 0;
                foreach(Theme theme in Managers.ThemeManager.Load(this)) {
                    if(theme.IsKnown) count++;
                }
                return count;
            }
        }

        public int FullyKnown {
            get {
                int count = 0;
                foreach(Theme theme in Managers.ThemeManager.Load(this)) {
                    if(theme.IsFullyKnown) count++;
                }
                return count;
            }
        }

        public bool IsFullyKnown {
            get {
                return Managers.ThemeManager.Load(this).Count - FullyKnown == 0;
            }
        }

        public void FromStrings(string[] props) {
            Name = props[0];
            Teacher = props[1];
            Room = props[2];
            Color = int.Parse(props[3]);
            LK = props[4] == "1";
            Written = props[5] == "1";
            Abi = props[6] == "1";
            Addition = props[7] == "1";
        }

        public string ToKey() {
            return Name.ToLower();
        }

        public override string ToString() {
            return $"{Name},{Teacher},{Room},{Color},{(LK ? "1" : "0")},{(Written ? "1" : "0")},{(Abi ? "1" : "0")},{(Addition ? "1" : "0")}";
        }

        public Subject Clone() {
            return new Subject() { Name = Name, Teacher = Teacher, Room = Room, Color = Color, LK = LK, Written = Written, Abi = Abi, Addition = Addition };
        }

        public FileManager<Subject> GetManager() {
            return Managers.SubjectManager;
        }

        public string GetName(bool plural) {
            return plural ? "Fächer" : "Fach";
        }

        public ElementView<Subject> GetView() {
            return new SubjectView { Element = this };
        }

        public int GetScore() {
            double alphabetScore = 0;
            for(int i = 0; i < Name.Length; i++) {
                alphabetScore += Math.Pow(Name[i], -i + 1);
            }
            return (LK ? 100 : 0) + (Abi ? 50 : 0) + (Written ? 25 : 0) + (Addition ? -50 : 0) - (int) alphabetScore;
        }

        public AddPage<Subject> GetAddPage() {
            return new AddSubject();
        }
    }
}
