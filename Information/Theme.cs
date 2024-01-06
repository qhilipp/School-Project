using School_Project.Management;
using School_Project.Tabs.NotesFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Information {
    public class Theme : IElement<Theme> {

        public string Name { get; set; }
        public int Oral { get; set; }
        public int Written { get; set; }
        public Subject Subject { get; set;}

        public double Avg {
            get {
                if(Oral == -1) {
                    if(Written == -1) {
                        return 0;
                    }
                    return Written;
                }
                if(Written == -1) {
                    return Oral;
                }
                return (Oral + Written) / 2.0;
            }
        }

        public int Best {
            get {
                int oral = Oral == -1 ? 15 : Oral;
                if(Subject.Written) {
                    int written = Written == -1 ? 15 : Written;
                    return (oral + written) / 2;
                }
                return oral;
            }
        }

        public int Worst {
            get {
                int oral = Oral == -1 ? 0 : Oral;
                if(Subject.Written) {
                    int written = Written == -1 ? 0 : Written;
                    return (oral + written) / 2;
                }
                return oral;
            }
        }

        public bool IsKnown {
            get {
                if(Oral != -1 || Written != -1) return true;
                return !Subject.Written && Oral != -1;
            }
        }

        public bool IsFullyKnown {
            get {
                return (Oral != -1 && Written != -1) || (Oral != -1 && !Subject.Written);
            }
        }

        public Theme() {
            Oral = -1;
            Written = -1;
        }

        public Theme Clone() {
            Theme nt = new Theme();
            nt.FromStrings(ToString().Split(",".ToCharArray()));
            return nt;
        }

        public override string ToString() {
            return $"{Oral},{Written}";
        }

        public void FromStrings(string[] props) {
            Oral = int.Parse(props[0]);
            Written = int.Parse(props[1]);
        }

        public FileManager<Theme> GetManager() {
            return Managers.ThemeManager;
        }

        public string GetName(bool plural = false) {
            return plural ? "Themen" : "Thema";
        }

        public string ToKey() {
            return Name.ToLower();
        }

        public ElementView<Theme> GetView() {
            return new ThemeView { Element = this };
        }

        public int GetScore() {
            return 1;
        }

        public AddPage<Theme> GetAddPage() {
            return new AddTheme(Subject);
        }
    }
}
