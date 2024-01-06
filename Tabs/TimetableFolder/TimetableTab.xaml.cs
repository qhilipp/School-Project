using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.TimetableFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimetableTab : ContentTab {

        private AppointmentView firstAppointment;
        private int scale = 15;
        private double initColumnWidth = 0;
        private double[] initLabelWidth;

        public TimetableTab() {
            InitializeComponent();
            grid.ColumnSpacing = Settings.Spacing;
            for(int i = 0; i < 288; i++) {
                grid.RowDefinitions.Add(new RowDefinition() { Height = scale });
            }
            foreach(Appointment appointment in Managers.AppointmentManager.Load()) {
                Add(appointment);
            }
            AddItem += Add;
            Init();
            foreach(View v in days.Children) {
                (v as Label).GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => {
                    Add(int.Parse((string) v.BindingContext));
                }) });
            }
        }

        private void Add() {
            Add(0);
        }

        private void Add(int day) {
            AddAppointment addAppointment = new AddAppointment(day);
            addAppointment.Finish += (appointment) => {
                Add(appointment);
            };
            Navigation.PushModalAsync(addAppointment);
        }

        private async void Init() {
            await Task.Delay(50);
            if(firstAppointment != null) _ = yScroll.ScrollToAsync(firstAppointment, ScrollToPosition.Start, false);
            await xScroll.ScrollToAsync(days.Children[DateTime.Now.DayOfWeek.ToInt()], ScrollToPosition.Start, false);
            initColumnWidth = grid.ColumnDefinitions[0].Width.Value;
            initLabelWidth = new double[days.Children.Count];
            double sum = 0;
            for(int i = 0; i < days.Children.Count; i++) {
                initLabelWidth[i] = days.Children[i].Width;
                sum += days.Children[i].Width;
                days.Children[i].MinimumWidthRequest = days.Children[i].Width;
            }
            grid.MinimumWidthRequest = sum;
            CalcSize();
        }

        private double Max(params double[] vals) {
            double max = double.MinValue;
            foreach(double val in vals) {
                max = Math.Max(max, val);
            }
            return max;
        }
        private void CalcSize() {
            for(int i = 0; i < days.Children.Count; i++) {
                double width = Max(initLabelWidth[i], initColumnWidth, (Width - (grid.ColumnSpacing * days.Children.Count - 1)) / days.Children.Count);
                days.Children[i].WidthRequest = width;
                grid.ColumnDefinitions[i].Width = width;
            }
        }

        public void Add(Appointment appointment) {
            AppointmentView view = new AppointmentView { Element = appointment };
            view.Click += () => {
                AddAppointment addAppointment = new AddAppointment(old: view.Element);
                addAppointment.Finish += (newAppointment) => {
                    if(newAppointment != null) {
                        view.Element = newAppointment;
                        Grid.SetRowSpan(view, view.Element.Duration / 5);
                        Grid.SetColumn(view, view.Element.Day);
                        Grid.SetRow(view, view.Element.Start / 5);
                        yScroll.ScrollToAsync(view, ScrollToPosition.Start, true);
                    } else {
                        grid.Children.Remove(view);
                    }
                };
                Navigation.PushModalAsync(addAppointment);
            };
            grid.Children.Add(view, appointment.Day, appointment.Start / 5);
            Grid.SetRowSpan(view, appointment.Duration / 5);
            if(firstAppointment == null || firstAppointment.Element.Start > appointment.Start) {
                firstAppointment = view;
                yScroll.ScrollToAsync(firstAppointment, ScrollToPosition.Start, false);
            }
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            double ratio = width / height;
            if(ratio > 1) {
                if(initLabelWidth != null) CalcSize();
            } else if(width > 0) {
                for(int i = 0; i < days.Children.Count; i++) {
                    days.Children[i].WidthRequest = width;
                    grid.ColumnDefinitions[i].Width = width;
                }
            }
        }

    }

    public static class DateTimeExtension {
        public static int ToInt(this DayOfWeek date) {
            int d = (int) date;
            if (d == 0) return 6;
            return d - 1;
        }
    }

}