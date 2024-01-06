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
    public partial class AddTextPracticeTab : StackLayout {
        public AddTextPracticeTab() {
            InitializeComponent();
        }

        public TextPractice Construct() {
            return new TextPractice {
                Question = question.Text,
                Answer = answer.Text
            };
        }

    }
}