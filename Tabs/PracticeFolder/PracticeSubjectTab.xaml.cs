using School_Project.Information;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using School_Project.Management;
using School_Project.Tabs.Templates;
using School_Project.Tabs.NotesFolder;
using School_Project.Tabs.SubjectsFolder;

namespace School_Project.Tabs.PracticeFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PracticeSubjectTab : ElementList<Subject> {

        public PracticeSubjectTab() : base(true) {
            InitializeComponent();
            Click += (view) => {
                ContentPage dirPage = new ContentPage { Content = new ThemeTab(view.Element, (theme) => { return new PracticeTab(theme); }), };
                NavigationPage.SetHasNavigationBar(dirPage, false);
                Navigation.PushModalAsync(dirPage);
            };
        }

    }
}