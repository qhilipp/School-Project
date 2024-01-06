using School_Project.Information;
using School_Project.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace School_Project.Management {
    public class ThemeManager : FileManager<Theme> {

        private List<string> Dirs = new List<string>{ "Notes", "Practices" };

        public override void Edit(Theme oldElement, Theme newElement) {
            foreach(string ThemeDir in Dirs) {
                string dir = Path.Combine(Settings.SemesterDir, ThemeDir);
                string fromDir = Path.Combine(dir, oldElement.Subject.ToKey());
                string toDir = Path.Combine(dir, newElement.Subject.ToKey());
                if(fromDir != toDir) Directory.Move(fromDir, toDir);
            }
        }

        public override bool Init() {
            bool exists = true;
            foreach(string ThemeDir in Dirs) {
                string dir = Path.Combine(Settings.SemesterDir, ThemeDir);
                bool thisExists = Directory.Exists(dir);
                if(!thisExists) {
                    exists = false;
                    Directory.CreateDirectory(dir);
                }
                foreach(var pair in Managers.SubjectManager.Elements) {
                    string path = Path.Combine(dir, pair.Key);
                    Directory.CreateDirectory(path);
                }
            }
            return exists;
        }

        public override List<Theme> Load() {
            throw new Exception("Die Load Methode darf für den ThemeManager nicht aufgerufen werden.");
        }

        public List<Theme> Load(Subject subject) {
            Elements = new Dictionary<string, Theme>();
            Dir = Path.Combine(Settings.SemesterDir, Dirs[0], subject.ToKey());
            List<Theme> Themes = new List<Theme>();
            foreach(string folder in Directory.GetDirectories(Dir)) {
                string name = new DirectoryInfo(folder).Name;
                Theme theme = new Theme { Name = name, Subject = subject };
                StreamReader sr = new StreamReader(Path.Combine(folder, "grades.txt"));
                theme.FromStrings(sr.ReadToEnd().Split(",".ToCharArray()));
                sr.Close();
                Themes.Add(theme);
                Elements[theme.ToKey()] = theme;
            }
            return Themes;
        }

        public override void Save(Theme element) {
            foreach(string ThemeDir in Dirs) {
                string dir = Path.Combine(Settings.SemesterDir, ThemeDir, element.Subject.ToKey(), element.Name);
                Directory.CreateDirectory(dir);
                Elements[element.ToKey()] = element;
                StreamWriter sw = new StreamWriter(Path.Combine(dir, "grades.txt"));
                sw.Write(element);
                sw.Close();
            }
        }

        public override void Delete(Theme element) {
            foreach(string ThemeDir in Dirs) Directory.Delete(Path.Combine(Settings.SemesterDir, ThemeDir, element.Subject.ToKey()), true);
        }

    }
}
