using School_Project.Management;
using School_Project.Tabs.PracticeFolder;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Information {
    public class Practice : IElement<Practice> {

        public Theme Theme { get; set; }
        public string Question { get; set; }
        public int ClassID { get; set; }
        public bool[] Score { get; set; }

        public Practice() {
            Score = new bool[10];
        }

        public void PushScore(bool score) {
            bool[] temp = new bool[Score.Length];
            for(int i = 1; i < Score.Length; i++) {
                temp[i] = Score[i - 1];
            }
            temp[0] = score;
            Score = temp;
            Managers.PracticeManager.Save(this);
        }

        public override string ToString() {
            return $"{Theme.ToKey()}\n{Question}\n{ScoreToString()}";
        }

        public string ScoreToString() {
            string s = "";
            foreach(bool b in Score) {
                s += (b ? "1" : "0") + ",";
            }
            return s.Substring(0, s.Length - 1);
        }

        public virtual void FromStrings(string[] props) { }

        public void FromInitStrings(string[] props) {
            Theme = Managers.ThemeManager.Elements[props[0]];
            Question = props[1];
            List<bool> scores = new List<bool>();
            foreach(string s in props[2].Split(",".ToCharArray())) {
                scores.Add(s == "1");
            }
            Score = scores.ToArray();
            string[] left = new List<string>(props).GetRange(3, props.Length - 3).ToArray();
            FromStrings(left);
        }

        public virtual int GetScore() {
            int score = 0;
            for(int i = 0; i < Score.Length; i++) {
                score -= (int) Math.Pow(2, Score.Length - i - 1) * (Score[i] ? 1 : 0);
            }
            return score;
        }

        public string ToKey() {
            string key = "";
            foreach(char c in Question.ToLower()) {
                if(c >= 'a' && c <= 'z') key += c;
            }
            return key;
        }

        public Practice Clone() {
            throw new NotImplementedException();
        }

        public ElementView<Practice> GetView() {
            return new PracticeView { Element = this };
        }

        public FileManager<Practice> GetManager() {
            return Managers.PracticeManager;
        }

        public string GetName(bool plural = false) {
            return plural ? "Übungen" : "Übung";
        }

        public AddPage<Practice> GetAddPage() {
            return new AddPractice(Theme.Subject);
        }
    }
}
