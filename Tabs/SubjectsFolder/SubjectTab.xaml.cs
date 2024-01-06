using School_Project.Information;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using School_Project.Management;
using School_Project.Tabs.Templates;

namespace School_Project.Tabs.SubjectsFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectTab : ContentTab {

        private ElementList<Subject> list;

        public SubjectTab() {
            InitializeComponent();
            list = new ElementList<Subject>(false);
            list.Click += (view) => {
                //AddSubject edit = new AddSubject(view.Element);
                //edit.Finish += (element) => {
                //    list.Load();
                //};
                //Navigation.PushModalAsync(new NavigationPage(edit));
                SubjectInfo info = new SubjectInfo(view.Element);
                info.Finish += () => list.Load();
                Navigation.PushModalAsync(new NavigationPage(info));
            };
            AddItem += Add;
            TabContent = list;
        }

        private void Add() {
            AddSubject addSubject = new AddSubject();
            addSubject.Finish += (subject) => list.Load();
            Navigation.PushModalAsync(addSubject);
        }
    }
}