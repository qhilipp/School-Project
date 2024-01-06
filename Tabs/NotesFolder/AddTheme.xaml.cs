using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.NotesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTheme : AddPage<Theme> {

        public AddTheme(Subject subject, Theme old = null) : base(old) {
            Element.Subject = subject;
            InitializeComponent();
        }

        protected override string Check() {
            string errorMsg = "";
            if(name.Text == "") {
                name.Error();
                errorMsg = "Du musst einen Namen angeben";
            }
            if(Managers.ThemeManager.Elements.ContainsKey(name.Text)) {
                name.Error();
                errorMsg = "Dieses Theme existiert bereits";
            }
            return errorMsg;
        }

        protected override void Construct() {
            Element.Name = name.Text;
        }

        protected override void Delete() {
            throw new System.NotImplementedException();
        }

        protected override void Edit() {
            throw new System.NotImplementedException();
        }

        protected override void Load() {
            name.Text = Old.Name;
        }
    }
}