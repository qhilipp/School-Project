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

namespace School_Project.Tabs.HomeworkFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeworkView : ElementView<Homework> {
        public HomeworkView() : base(false) {
            InitializeComponent();
            mainContent.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { Click?.Invoke(); }) });
            finish.Change += (val) => {
                Homework old = Element.Clone();
                Element.Finished = val;
                Managers.HomeworkManager.Edit(old, Element);
            };
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                UpdateTime();
                return true;
            });
        }

        protected override void Change() {
            name.Text = Element.Subject.Name;
            description.Text = Element.Description;
            BackgroundColor = ColorManager.GetColor(Element.Subject.Color);
            finish.IsChecked = Element.Finished;
            UpdateTime();
        }

        public void UpdateTime() {
            TimeSpan diff = DateTime.FromFileTime(Element.Date).Subtract(DateTime.Now);
            if(diff.TotalHours < -1) {
                date.Text = "Seit " + Quantity(-diff.Hours, "Stunde") + " fällig";
            } else if(diff.TotalSeconds < 1) {
                date.Text = "Seit " + Quantity(-diff.Minutes, "Minute") + " fällig";
            } else if(diff.TotalMinutes < 1) {
                date.Text = "Noch " + Quantity(diff.Seconds, "Sekunde");
            } else if(diff.TotalHours < 1) {
                date.Text = "Noch " + Quantity(diff.Minutes, "Minute");
            } else if(diff.TotalHours < 24) {
                date.Text = "Noch " + Quantity(diff.Hours, "Stunde");
            } else if(diff.Days <= 7) {
                if(Element.Date < DateTime.Today.AddDays(2).ToFileTime()) {
                    date.Text = "Abgabe Morgen";
                } else if(Element.Date < DateTime.Today.AddDays(3).ToFileTime()) {
                    date.Text = "Abgabe Übermorgen";
                } else {
                    date.Text = "Abgabe am " + DayName(DateTime.FromFileTime(Element.Date).DayOfWeek);
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