using School_Project.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Xamarin.Essentials;
using School_Project.Management;

namespace School_Project.Management {
    public class NoteManager : FileManager<Note> {

        public override bool Init() {
            bool exists = true;
            foreach(var pair in Managers.SubjectManager.Elements) {
                List<Theme> themes = Managers.ThemeManager.Load(pair.Value);
                foreach(Theme theme in themes) {
                    string dir = Path.Combine(Settings.SemesterDir, "Notes", theme.Subject.ToKey());
                    if(!Directory.Exists(dir)) {
                        exists = false;
                        Directory.CreateDirectory(dir);
                    }
                }
            }
            return exists;
        }

        public override List<Note> Load() {
            throw new Exception("Die Load Methode darf für den NoteManager nicht aufgerufen werden.");
        }

        public List<Note> Load(Theme theme) {
            Elements = new Dictionary<string, Note>();
            Dir = Path.Combine(Settings.SemesterDir, "Notes", theme.Subject.ToKey(), theme.Name);
            List<Note> notes = new List<Note>();
            foreach(string file in Directory.GetFiles(Dir)) {
                if(new FileInfo(file).Name == "grades.txt") continue;
                if(new FileInfo(file).Extension == ".html") continue;
                if(!File.Exists(file)) continue;
                StreamReader sr = new StreamReader(file);
                List<string> infos = sr.ReadToEnd().Split(",".ToCharArray()).ToList();
                sr.Close();
                string htmlFile = file.Split(".".ToCharArray())[0] + ".html";
                if(File.Exists(htmlFile)) {
                    StreamReader content = new StreamReader(htmlFile);
                    infos.Add(content.ReadToEnd());
                    content.Close();
                }
                Note note = new Note();
                note.FromStrings(infos.ToArray());
                notes.Add(note);
                Elements[note.ToKey()] = note;
            }
            return notes;
        }

        public override void Save(Note element) {
            string path = Path.Combine(Settings.SemesterDir, "Notes", element.Theme.Subject.ToKey(), element.Theme.ToKey(), element.ToKey());
            StreamWriter sw = new StreamWriter(path + ".txt");
            sw.Write(element);
            sw.Close();
            if(!element.IsLink) {
                StreamWriter html = new StreamWriter(path + ".html");
                html.Write(element.Content);
                html.Close();
            }
            Elements[element.ToKey()] = element;
        }

        public override void Edit(Note oldElement, Note newElement) {
            string path = Path.Combine(Settings.SemesterDir, "Notes", oldElement.Theme.Subject.ToKey(), oldElement.Theme.ToKey(), oldElement.ToKey());
            File.Delete(path + ".txt");
            File.Delete(path + ".html");
            Elements.Remove(oldElement.ToKey());
            Save(newElement);
        }

    }
}
