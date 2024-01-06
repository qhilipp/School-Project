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

namespace School_Project.Tabs.GradesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectGradeView : ElementView<Subject> {
        public SubjectGradeView() {
            InitializeComponent();
        }

        protected override void Change() {
            name.Text = Element.Name;
            avg.Text = $"Durchschnitt: {(Element.Known == 0 ? "Unbekannt" : (Settings.Format == 0 ? Element.Avg + "" : GradesPanel.ToGrade(Element.Avg)))}";
            BackgroundColor = ColorManager.GetColor(Element.Color);
        }

    }
}