using School_Project.Management;
using School_Project.Tabs.PracticeFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Information {
    public class TextPractice : Practice {

        public string Answer { get; set; }

        public TextPractice() {
            ClassID = 1;
        }

        public override void FromStrings(string[] props) {
            Answer = props[0];
        }
        public override string ToString() {
            return $"{base.ToString()}\n{Answer}";
        }

    }
}
