using School_Project.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace School_Project.Management {
    public class Settings {

        private static string path = Path.Combine(FileSystem.AppDataDirectory, "settings.txt");

        public static bool IsMobile {
            get {
                return Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS;
            }
        }

        public static double FontSize { 
            get {
                return IsMobile ? 30 : 25;
            }
        }
        public static double SmallFontSize { 
            get {
                return FontSize * .8;
            }
        }

        public static int CornerRadius { 
            get {
                return IsMobile ? 20 : 15;
            }
        }
        public static double Spacing { 
            get {
                return 10;
            }
        }
        public static double Padding {
            get {
                return IsMobile ? 0 : 10;
            }
        }

        private static int format = 0;
        public static int Format {
            get {
                return format;
            }
            set {
                format = value;
                Save();
            }
        }

        private static int semester = 0;
        public static int Semester {
            get {
                return semester;
            }
            set {
                semester = value;
                Save();
                Managers.Init();
            }
        }

        public static string SemesterDir {
            get {
                return Path.Combine(Dir, Semester + "");
            }
        }

        public static string Dir {
            get {
                return Path.Combine(FileSystem.AppDataDirectory, "Semsters");
            }
        }

        public static int Count {
            get {
                return Directory.GetDirectories(Dir).Length;
            }
        }

        public static Dictionary<string, Subject> Create(bool transfer = false) {
            Directory.CreateDirectory(Path.Combine(Dir, Count + ""));
            if(transfer) {
                return Managers.SubjectManager.Elements;
            }
            return null;
        }


        public static void Init() {
            if(!File.Exists(path)) Save();
            Load();
            if(!Directory.Exists(Dir)) Directory.CreateDirectory(Dir);
            if(Count == 0) Create();
        }

        public static void Save() {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(Format);
            sw.WriteLine(Semester);
            sw.Close();
        }

        public static void Load() {
            StreamReader sr = new StreamReader(path);
            format = int.Parse(sr.ReadLine());
            semester = int.Parse(sr.ReadLine());
            sr.Close();
        }

    }
}
