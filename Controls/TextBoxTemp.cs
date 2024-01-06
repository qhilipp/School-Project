using System;
using School_Project.Management;
using Xamarin.Forms;

namespace School_Project.Controls {
    public class TextBoxTemp : Frame {

        public string Text {
            get {
                return entry.Text;
            }
            set {
                entry.Text = value;
            }
        }
        //public delegate void TextChangeDelegate(string txt);
        //public TextChangeDelegate TextChange { get; set; }

        Entry entry;

        public TextBoxTemp() {

            Padding = new Thickness(15, 10);
            BackgroundColor = ColorManager.OverlayColor;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            CornerRadius = 15;

            entry = new Entry();
            entry.BackgroundColor = Color.Transparent;
            //entry.TextChanged += (object sender, TextChangedEventArgs e) => { TextChange?.Invoke(entry.Text); };
            Content = entry;

        }
    }
}
