using School_Project.Information;
using School_Project.Management;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.Templates {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeView : ElementView<Theme> {
        public ThemeView() {
            InitializeComponent();
        }

        protected override void Change() {
            name.Text = Element.Name;
            BackgroundColor = ColorManager.GetColor(Element.Subject.Color);
        }
    }
}