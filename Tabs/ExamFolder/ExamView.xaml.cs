using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using School_Project.Tabs.TimetableFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.ExamFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamView : ElementView<Exam> {
        public ExamView() {
            InitializeComponent();
        }

        protected override void Change() {
            name.Text = Element.Subject.Name;
            themes.Text = Element.Themes;
            BackgroundColor = ColorManager.GetColor(Element.Subject.Color);
            UpdateTime();
        }

        public void UpdateTime() {
            TimeSpan diff = DateTime.FromFileTime(Element.Date).Subtract(DateTime.Now);
            if(diff.TotalHours < -24) {
                DateTime date = DateTime.FromFileTime(Element.Date);
                this.date.Text = "Am " + (date.Day < 10 ? "0" : "") + date.Day + "." + (date.Month < 10 ? "0" : "") + date.Month + "." + date.Year + " um " + date.Hour + ":" + (date.Minute < 10 ? "0" : "") + date.Minute + " Uhr geschrieben";
            } else if(diff.TotalHours < -1) {
                date.Text = "Seit " + Quantity(-diff.Hours, "Stunde") + " vorbei";
            } else if(diff.TotalSeconds < 1) {
                date.Text = "Seit " + Quantity(-diff.Minutes, "Minute") + " vorbei";
            } else if(diff.TotalMinutes < 1) {
                date.Text = "Noch " + Quantity(diff.Seconds, "Sekunde");
            } else if(diff.TotalHours < 1) {
                date.Text = "Noch " + Quantity(diff.Minutes, "Minute");
            } else if(diff.TotalHours < 24) {
                date.Text = "Noch " + Quantity(diff.Hours, "Stunde");
            } else if(diff.Days <= 7) {
                if(Element.Date < DateTime.Today.AddDays(1).ToFileTime()) {
                    DateTime start = DateTime.FromFileTime(Element.Date);
                    DateTime end = start.AddMinutes(Element.Duration);
                    date.Text = "Heute von " + start.Hour + ":" + (start.Minute < 10 ? "0" : "") + start.Minute + " bis " + end.Hour + start.Hour + ":" + (end.Minute < 10 ? "0" : "") + end.Minute;
                } else if(Element.Date < DateTime.Today.AddDays(2).ToFileTime()) {
                    date.Text = "Morgen";
                } else if(Element.Date < DateTime.Today.AddDays(3).ToFileTime()) {
                    date.Text = "Übermorgen";
                } else {
                    date.Text = "Am " + DayName(DateTime.FromFileTime(Element.Date).DayOfWeek);
                }
            } else {
                date.Text = "Noch " + diff.Days + " Tage";
            }
        }
        private string Quantity(int n, string singular, string plural = null) {
            if(plural == null)
                plural = singular + "n";
            return n + " " + (Math.Abs(n) == 1 ? singular : plural);
        }
        private string DayName(DayOfWeek day) {
            return new Appointment { Day = day.ToInt() }.GetDay();
        }

    }
}