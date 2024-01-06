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
    public partial class DateSelector : Frame {

        public DateTime Date {
            get {
                return new DateTime().AddYears(year.SelectedIndex + DateTime.Now.Year - 1).AddMonths(month.SelectedIndex).AddDays(day.SelectedIndex);
            }
            set {
                year.SelectedIndex = value.Year - DateTime.Now.Year;
                month.SelectedIndex = value.Month - 1;
                day.SelectedIndex = value.Day - 1;
            }
        }
        public bool IsLeapYear {
            get {
                int y = int.Parse(year.SelectedItem);
                if(y % 4 == 0) {
                    if(y % 100 == 0) {
                        if(y % 400 == 0) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        return true;
                    }
                }
                return false;
            }
        }

        public DateSelector() {
            InitializeComponent();
            CornerRadius = Settings.CornerRadius;
            year.Change += () => {
                AdjustDays();
            };
            month.Change += () => {
                AdjustDays();
            };
            for(int i = DateTime.Now.Year; i < DateTime.Now.Year + 10; i++) {
                year.Add(i + "");
            }
            month.Add("Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember");
            AdjustDays();
        }

        private void AdjustDays() {
            int index = day.SelectedIndex;
            day.Clear();
            int days;
            switch(month.SelectedIndex) {
                case 1: days = IsLeapYear ? 29 : 28; break;
                case 3: days = 30; break;
                case 5: days = 30; break;
                case 8: days = 30; break;
                case 10: days = 30; break;
                default: days = 31; break;
            }
            for(int i = 1; i <= days; i++) {
                day.Add(i + "");
            }
            if(index >= 0 && index < days) day.SelectedIndex = index;
        }

    }
}