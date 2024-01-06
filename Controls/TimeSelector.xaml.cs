using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeSelector : Frame {

        public int Hours {
            get {
                return int.Parse(hour.SelectedItem);
            }
            set {
                hour.SelectedItem = value + "";
            }
        }

        public int Minute {
            get {
                return int.Parse(minute.SelectedItem);
            }
            set {
                minute.SelectedItem = value + "";
            }
        }

        public int Time {
            get {
                return int.Parse(hour.SelectedItem) * 60 + int.Parse(minute.SelectedItem);
            }
            set {
                int m = value % 60;
                m -= m % 5;
                hour.SelectedItem = (value / 60) + "";
                minute.SelectedItem = (m < 10 ? "0" : "") + m;
            }
        }

        public bool IsSelected {
            get {
                return hour.SelectedIndex != -1 && minute.SelectedIndex != -1;
            }
        }

        public TimeSelector() {
            InitializeComponent();
            CornerRadius = Settings.CornerRadius;
            for(int i = 0; i < 24; i++) {
                hour.Add(i + "");
            }
            for(int i = 0; i < 60; i+=5) {
                minute.Add((i < 10 ? "0" : "") + i);
            }
            Focused += (s, e) => {
                BackgroundColor = ColorManager.OverlayColor;
            };
        }

        public void Error() {
            BackgroundColor = ColorManager.ErrorColor;
        }

    }
}