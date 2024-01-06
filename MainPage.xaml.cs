using School_Project.Management;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage {

        public MainPage(bool first) {
            InitializeComponent();
            Padding = new Thickness(Settings.Padding);
            BackgroundColor = Color.Green;
            flyout.Select += (content) => {
                Detail = new ContentPage { Content = content, BackgroundColor = ColorManager.BackgroundColor };
                OnSizeAllocated(Width, Height);
            };
            if(first) {
                flyout.SelectTab(3);
                DisplayAlert("School Project", "Hi!\nOrganisiere Deinen Alltag, indem Du deine Hausaufgaben, Klausuren, Termine, etc. einträgst.\nBeginne damit, deine Fächer einzutragen. Klicke dazu auf \"Hinzufügen\"", "Ok");
            } else {
                flyout.SelectTab(0);
            }
        }

        public void Select(int index) {
            flyout.SelectTab(index);
        }

    }
}