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

namespace School_Project.Tabs.GradesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradeView : StackLayout {

        public delegate void ChangeDelegate();
        public ChangeDelegate Change;

        public Theme Theme { get; private set; }

        private int format;
        public int Format {
            get {
                return format;
            }
            set {
                if(format == value)
                    return;
                format = value;
                foreach(TextBox tb in layout.Children) {
                    tb.Input = format == 0 ? TextBox.Points : TextBox.Grade;
                    if(value == 0) {
                        if(tb.Text != "")
                            tb.Text = GradesPanel.ToPoints(tb.Text) + "";
                    } else if(value == 1) {
                        if(tb.Text != "")
                            tb.Text = GradesPanel.ToGrade(int.Parse(tb.Text));
                    }
                }
            }
        }

        public GradeView(Theme theme) {
            InitializeComponent();
            Theme = theme;
            name.Text = Theme.Name;
            string oralGrade = theme.Oral == -1 ? "" : theme.Oral + "";
            layout.Children.Add(new TextBox { Text = oralGrade, Placeholder = "Mündliche Note", TextChange = OralChange, MaxLength = 2, Input = TextBox.Points });
            if(theme.Subject.Written) {
                string writtenGrade = "" + (theme.Written == -1 ? "" : theme.Written + "");
                layout.Children.Add(new TextBox { Text = writtenGrade, Placeholder = "Schriftliche Note", TextChange = WrittenChange, MaxLength = 2, Input = TextBox.Points });
            }
            Format = 0;
        }

        private void OralChange(string txt) {
            if(format == 0) {
                Theme.Oral = txt == "" ? -1 : int.Parse(txt);
            } else {
                Theme.Oral = txt == "" ? -1 : GradesPanel.ToPoints(txt);
            }
            Managers.ThemeManager.Save(Theme);
            Change?.Invoke();
        }

        private void WrittenChange(string txt) {
            if(format == 0) {
                Theme.Written = txt == "" ? -1 : int.Parse(txt);
            } else {
                Theme.Written = txt == "" ? -1 : GradesPanel.ToPoints(txt);
            }
            Managers.ThemeManager.Save(Theme);
            Change?.Invoke();
        }

    }
}