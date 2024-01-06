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
    public partial class VocabPracticeDisplay : PracticeDisplay<VocabPractice> {
        public VocabPracticeDisplay(List<Practice> practices, int index) : base(practices, index) {
            InitializeComponent();
            Answers.Add(new VocabCard(practices[index] as VocabPractice, new Random().Next(100) > 50));
        }

        public override bool Check() {
            return (Answers[0] as VocabCard).Check();
        }

    }
}