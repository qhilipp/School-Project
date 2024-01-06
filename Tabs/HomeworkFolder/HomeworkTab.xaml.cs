using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.HomeworkFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeworkTab : ContentTab {

        private ElementList<Homework> list;

        public HomeworkTab() {
            InitializeComponent();
            list = new ElementList<Homework>(false);
            list.Click += (view) => {
                AddHomework edit = new AddHomework(view.Element);
                edit.Finish += (homework) => list.Load();
                Navigation.PushModalAsync(edit);
            };
            AddItem += Add;
            TabContent = list;
        }

        private void Add() {
            AddHomework addHomework = new AddHomework();
            addHomework.Finish += (homework) => list.Load();
            Navigation.PushModalAsync(addHomework);
        }

    }
}