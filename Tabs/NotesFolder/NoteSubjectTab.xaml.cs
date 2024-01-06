using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.SubjectsFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.NotesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteSubjectTab : ElementList<Subject> {

        public NoteSubjectTab() : base(true) {
            InitializeComponent();
            Click += (view) => {
                ContentPage dirPage = new ContentPage { Content = new ThemeTab(view.Element, (theme) => { return new NoteTab(theme); }) };
                NavigationPage.SetHasNavigationBar(dirPage, false);
                Navigation.PushModalAsync(dirPage);
            };
        }

    }
}