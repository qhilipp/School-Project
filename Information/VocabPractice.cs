using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Information {
    public class VocabPractice : Practice {

        public string Foreign { get; set; }
        public double Color { get; private set; }

        public List<string> Properties { get; set; }
        public List<string> Values { get; set; }

        public VocabPractice() {
            Properties = new List<string>();
            Values = new List<string>();
            ClassID = 2;
            Color = new Random().NextDouble();
        }

        public override void FromStrings(string[] props) {
            Foreign = props[0];
            Color = double.Parse(props[1]);
            for(int i = 2; i < props.Length; i += 2) {
                Properties.Add(props[i]);
                Values.Add(props[i + 1]);
            }
        }

        public override string ToString() {
            string txt = base.ToString() + $"\n{Foreign}\n{Color}";
            for(int i = 0; i < Properties.Count; i++) {
                txt += $"\n{Properties[i]}\n{Values[i]}";
            }
            return txt;
        }

    }
}
