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
    public partial class SubjectView : ElementView<Subject> {

        public SubjectView() {
            InitializeComponent();
        }

        protected override void Change() {
            name.Text = Element.Name;
            BackgroundColor = ColorManager.GetColor(Element.Color);
        }
    }
}