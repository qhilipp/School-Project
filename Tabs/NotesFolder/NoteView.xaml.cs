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

namespace School_Project.Tabs.NotesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteView : ElementView<Note> {
        public NoteView() {
            InitializeComponent();
        }

        protected override void Change() {
            BackgroundColor = ColorManager.GetColor(Element.Theme.Subject.Color);
            name.Text = Element.Name;
        }

    }
}