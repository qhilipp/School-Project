using School_Project.Information;
using School_Project.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace School_Project.Management {
    public class PracticeManager : FileManager<Practice> {

        public override bool Init() {
            bool exists = true;
            foreach(var pair in Managers.SubjectManager.Elements) {
                List<Theme> themes = Managers.ThemeManager.Load(pair.Value);
                foreach(Theme theme in themes) {
                    string dir = Path.Combine(Settings.SemesterDir, "Practices", theme.Subject.ToKey());
                    if(!Directory.Exists(dir)) {
                        exists = false;
                        Directory.CreateDirectory(dir);
                    }
                }
            }
            return exists;
        }

        public override void Save(Practice element) {
            StreamWriter sw = new StreamWriter(Path.Combine(Settings.SemesterDir, "Practices", element.Theme.Subject.ToKey(), element.Theme.ToKey(), element.ToKey() + ".txt"));
            sw.Write(element.ClassID + element.ToString());
            sw.Close();
        }

        public override List<Practice> Load() {
            throw new Exception("Die Load Methode darf für den PracticeManager nicht aufgerufen werden.");
        }

        public List<Practice> Load(Theme theme) {
            List<Practice> practices = new List<Practice>();
            Elements = new Dictionary<string, Practice>();
            foreach(string file in Directory.GetFiles(Path.Combine(Settings.SemesterDir, "Practices", theme.Subject.ToKey(), theme.Name))) {
                if(new FileInfo(file).Name == "grades.txt") continue;
                StreamReader sr = new StreamReader(file);
                string[] lines = sr.ReadToEnd().Split("\n".ToCharArray());
                char classID = lines[0] [0];
                lines[0] = lines[0].Substring(1);
                sr.Close();
                Practice practice = null;
                if(classID == '0') {
                    practice = new MultiPractice();
                } else if(classID == '1') {
                    practice = new TextPractice();
                } else if(classID == '2') {
                    practice = new VocabPractice();
                }
                practice.FromInitStrings(lines);
                practices.Add(practice);
                Elements[practice.ToKey()] = practice;
            }
            return practices;
        }

    }
}
