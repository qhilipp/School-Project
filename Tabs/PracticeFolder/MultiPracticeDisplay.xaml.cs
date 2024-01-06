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
    public partial class MultiPracticeDisplay : PracticeDisplay<MultiPractice> {

        MultiPractice multiPractice;

        public MultiPracticeDisplay(List<Practice> practices, int index) : base(practices, index) {
            InitializeComponent();
            multiPractice = new MultiPractice {
                AnswerTxts = new string[(practices[index] as MultiPractice).AnswerTxts.Count].ToList(),
                AnswerVals = new bool[(practices[index] as MultiPractice).AnswerTxts.Count].ToList()
            };
            question.Text = practices[index].Question;
            AnswerOptions = LayoutOptions.Center;
            List<int> indecies = new List<int>();
            for(int i = 0; i < (practices[index] as MultiPractice).AnswerVals.Count; i++) indecies.Add(i);
            for(int i = 0; i < multiPractice.AnswerTxts.Count; i++) {
                int j = new Random().Next(indecies.Count);
                multiPractice.AnswerTxts[i] = (practices[index] as MultiPractice).AnswerTxts[indecies[j]];
                multiPractice.AnswerVals[i] = (practices[index] as MultiPractice).AnswerVals[indecies[j]];
                indecies.RemoveAt(j);
            }
            foreach(string answer in multiPractice.AnswerTxts) {
                answers.Children.Add(new StackLayout {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                        new Controls.CheckBox { Text = answer }
                    }
                });
            }
        }

        public override bool Check() {
            bool isCorrect = true;
            for(int i = 0; i < answers.Children.Count; i++) {
                Controls.CheckBox thisCheckBox = (answers.Children[i] as StackLayout).Children[0] as Controls.CheckBox;
                thisCheckBox.IsEnabled = false;
                bool thisCorrect = thisCheckBox.IsChecked == multiPractice.AnswerVals[i];
                if(!thisCorrect)
                    isCorrect = false;
                (answers.Children[i] as StackLayout).Children.Insert(0, new Controls.CheckBox {
                    IsChecked = multiPractice.AnswerVals[i],
                    Color = thisCorrect ? Color.Green : Color.Red,
                    IsEnabled = false
                });
            }
            return isCorrect;
        }

    }
}
