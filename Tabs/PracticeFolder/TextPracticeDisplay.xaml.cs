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
    public partial class TextPracticeDisplay : PracticeDisplay<TextPractice> {

        public TextPracticeDisplay(List<Practice> practices, int index) : base(practices, index) {
            InitializeComponent();
            question.Text = practices[index].Question;
        }

        public override bool Check() {
            string clearedCorrectAnswer = "";
            foreach(char c in Practice.Answer.ToLower()) {
                if(c >= 'a' && c <= 'z') {
                    clearedCorrectAnswer += c;
                }
            }
            string clearedGivenAnswer = "";
            foreach(char c in answer.Text.ToLower()) {
                if(c >= 'a' && c <= 'z') {
                    clearedGivenAnswer += c;
                }
            }
            bool isCorrect = clearedCorrectAnswer == clearedGivenAnswer;
            if(!isCorrect) {
                Answers.Add(new Label {
                    Text = Practice.Answer,
                    TextColor = Color.Red,
                    HorizontalTextAlignment = TextAlignment.Center
                });
            }
            return isCorrect;
        }

    }
}