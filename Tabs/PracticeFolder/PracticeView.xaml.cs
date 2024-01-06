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

namespace School_Project.Tabs.PracticeFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeView : ElementView<Practice> {
        public PracticeView() {
            InitializeComponent();
        }

        protected override void Change() {
            question.Text = Element.Question;
            BackgroundColor = ColorManager.GetColor(Element.Theme.Subject.Color);
        }
    }
}