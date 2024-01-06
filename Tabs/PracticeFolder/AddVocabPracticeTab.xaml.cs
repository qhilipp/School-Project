using School_Project.Controls;
using School_Project.Information;
using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.PracticeFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddVocabPracticeTab : StackLayout {
        public AddVocabPracticeTab(Subject s) {
            InitializeComponent();
            foreign.Placeholder = GetCorrect(s.Name);
            AddProp();
            addProp.Click += (btn) => AddProp();
            removeProp.Click += (btn) => {
                if(props.Children.Count > 0)
                    props.Children.RemoveAt(props.Children.Count - 1);
            };
        }

        private void AddProp() {
            StackLayout layout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Spacing = Settings.Spacing
            };
            layout.Children.Add(new TextBox {
                Placeholder = "Eigenschaft"
            });
            layout.Children.Add(new TextBox {
                Placeholder = "Wert"
            });
            props.Children.Add(layout);
        }

        private string GetCorrect(string subject) {
            string add = "ischer";
            int i = 1;
            while(i <= add.Length && subject[subject.Length - 1] != add[add.Length - i]) {
                i++;
            }
            return subject + add.Substring(add.Length - i + 1, i - 1) + " Begriff";
        }

        public VocabPractice Construct() {
            VocabPractice practice = new VocabPractice {
                Question = german.Text,
                Foreign = foreign.Text
            };
            foreach(var view in props.Children) {
                practice.Properties.Add(((view as StackLayout).Children[0] as TextBox).Text);
                practice.Values.Add(((view as StackLayout).Children[1] as TextBox).Text);
            }
            return practice;
        }

    }
}