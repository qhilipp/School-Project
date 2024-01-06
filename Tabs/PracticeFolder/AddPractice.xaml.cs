using School_Project.Information;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Project.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.PracticeFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPractice : AddPage<Practice> {
        public AddPractice(Subject subject, Practice old = null) : base(old) {
            InitializeComponent();
            AutoSave = false;
            tabs.AddTab("Multiple-Choice", new AddMultiPracticeTab());
            tabs.AddTab("Text", new AddTextPracticeTab());
            tabs.AddTab("Vokabeln", new AddVocabPracticeTab(subject));
        }

        protected override string Check() {
            string errorMsg = "";
            return errorMsg;
        }

        protected override void Construct() {
            if(tabs.SelectedIndex == 0) {
                Element = (tabs.SelectedTab as AddMultiPracticeTab).Construct();
            } else if(tabs.SelectedIndex == 1) {
                Element = (tabs.SelectedTab as AddTextPracticeTab).Construct();
            } else if(tabs.SelectedIndex == 2) {
                Element = (tabs.SelectedTab as AddVocabPracticeTab).Construct();
            }
        }

        protected override void Delete() {
            throw new NotImplementedException();
        }

        protected override void Edit() {
            throw new NotImplementedException();
        }

        protected override void Load() {
            throw new NotImplementedException();
        }
    }
}
