using School_Project.Management;
using School_Project.Tabs.PracticeFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Information {
    public class MultiPractice : Practice {

        public List<string> AnswerTxts { get; set; }
        public List<bool> AnswerVals { get; set; }

        public MultiPractice() {
            AnswerTxts = new List<string>();
            AnswerVals = new List<bool>();
            ClassID = 0;
        }

        public override string ToString() {
            string txt = base.ToString();
            for(int i = 0; i < AnswerTxts.Count; i++) {
                txt += "\n" + (AnswerVals[i] ? "1" : "0") + AnswerTxts[i];
            }
            return txt;
        }

        public override void FromStrings(string[] props) {
            for(int i = 0; i < props.Length; i++) {
                AnswerVals.Add(props[i][0] == '1');
                AnswerTxts.Add(props[i].Substring(1));
            }
        }

    }

}
