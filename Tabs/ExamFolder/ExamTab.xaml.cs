using School_Project.Information;
using School_Project.Tabs.HomeworkFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.ExamFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamTab : ContentTab {

        private ElementList<Exam> list;

        public ExamTab() {
            InitializeComponent();
            list = new ElementList<Exam>(false);
            list.Click += (view) => {
                AddExam edit = new AddExam(view.Element);
                edit.Finish += (homework) => list.Load();
                Navigation.PushModalAsync(edit);
            };
            AddItem += Add;
            TabContent = list;
        }

        private void Add() {
            AddExam addPage = new AddExam();
            addPage.Finish += (exam) => list.Load();
            Navigation.PushModalAsync(addPage);
        }

    }
}