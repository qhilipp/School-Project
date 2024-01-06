using School_Project.Controls;
using School_Project.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.PracticeFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMultiPracticeTab : StackLayout {

        public AddMultiPracticeTab() {
            InitializeComponent();
            AddAnswer();
            AddAnswer();
            addAnswer.Click += (btn) => AddAnswer();
            removeAnswer.Click += (btn) => {
                if(answers.Children.Count > 1)
                    answers.Children.RemoveAt(answers.Children.Count - 1);
            };
        }

        private void AddAnswer() {
            StackLayout layout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
            };
            layout.Children.Add(new Controls.CheckBox {

            });
            layout.Children.Add(new TextBox {
                Placeholder = "Antwortmöglichkeit"
            });
            answers.Children.Add(layout);
        }

        public MultiPractice Construct() {
            MultiPractice practice = new MultiPractice { Question = question.Text };
            foreach(var view in answers.Children) {
                practice.AnswerVals.Add(((view as StackLayout).Children[0] as Controls.CheckBox).IsChecked);
                practice.AnswerTxts.Add(((view as StackLayout).Children[1] as TextBox).Text);
            }
            return practice;
        }

    }
}