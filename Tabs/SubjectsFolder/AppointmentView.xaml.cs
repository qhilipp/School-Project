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

namespace School_Project.Tabs.SubjectsFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentView : ElementView<Appointment> {
        public AppointmentView() {
            InitializeComponent();
        }

        protected override void Change() {
            day.Text = Element.GetDay();
            time.Text = "Um " + Element.GetTime();
            room.Text = "In " + Element.Subject.Room;
            BackgroundColor = ColorManager.GetColor(Element.Subject.Color);
        }

    }
}