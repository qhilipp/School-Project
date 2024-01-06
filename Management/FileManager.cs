using School_Project.Information;
using School_Project.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace School_Project.Management {
    public class FileManager<T> where T : class, IElement<T>, new() {

        // C:\Users\Philipp\AppData\Local\Packages\b8b1ae58-1289-4b4b-87d8-ea2c89e80c12_dtm0s7veer28j\LocalState

        public Dictionary<string, T> Elements;

        protected string dir;
        public string Dir {
            get {
                return dir;
            }
            set {
                dir = value;
            }
        }

        public virtual bool Init() {
            dir = Path.Combine(Settings.SemesterDir, typeof(T).Name.ToLower() + "s.txt");
            bool exists = File.Exists(Dir);
            if(!exists) {
                File.Create(Dir).Close();
                Elements = new Dictionary<string, T>();
            } else
                Load();
            return exists;
        }

        public virtual List<T> Load() {
            Elements = new Dictionary<string, T>();
            List<T> list = new List<T>();
            StreamReader sr = new StreamReader(Dir);
            string line = sr.ReadLine();
            while(line != null) {
                string[] props = line.Split(',');
                T s = new T();
                s.FromStrings(props);
                list.Add(s);
                Elements[s.ToKey()] = s;
                line = sr.ReadLine();
            }
            sr.Close();
            return list;
        }

        public virtual void Save(T element) {
            //if(Exists(element)) return;
            StreamWriter sw = new StreamWriter(Dir, true);
            sw.WriteLine(element);
            sw.Close();
            Elements[element.ToKey()] = element;
        }

        public virtual void Edit(T oldElement, T newElement) {
            string tempFile = Path.Combine(FileSystem.AppDataDirectory, typeof(T).Name.ToLower() + "s_temp.txt");
            using(var sr = new StreamReader(Dir))
            using(var sw = new StreamWriter(tempFile)) {
                string line;
                while((line = sr.ReadLine()) != null) {
                    if(line != oldElement.ToString())
                        sw.WriteLine(line);
                    else if(newElement != null)
                        sw.WriteLine(newElement);
                }
            }
            File.Delete(Dir);
            File.Move(tempFile, Dir);
            Elements.Remove(oldElement.ToKey());
            if(newElement != null) Elements[newElement.ToKey()] = newElement;
        }

        public virtual void Delete(T element) {
            Edit(element, null);
            Elements.Remove(element.ToKey());
        }

        public virtual bool Exists(T element) {
            StreamReader sr = new StreamReader(Dir);
            string line;
            while((line = sr.ReadLine()) != null) {
                if(line == element.ToString()) {
                    sr.Close();
                    return true;
                }
            }
            sr.Close();
            return false;
        }

    }
}
