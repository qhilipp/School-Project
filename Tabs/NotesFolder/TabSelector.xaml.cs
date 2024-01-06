using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Content")]
    public partial class TabSelector : StackLayout {

        public int SelectedIndex {
            get {
                for(int i = 0; i < bar.Children.Count; i++) {
                    if((bar.Children[i] as Label).FontAttributes == FontAttributes.Bold) return i;
                }
                return -1;
            }
        }

        public View SelectedTab { 
            get {
                return content.Content;    
            }
            private set {
                content.Content = value;
            }
        }

        public TabSelector() {
            InitializeComponent();
        }

        public void AddTab(string name, View tab) {
            Label lbl = null;
            lbl = new Label {
                Text = name,
                GestureRecognizers = {
                    new TapGestureRecognizer {
                        Command = new Command(() => {
                            foreach(var v in bar.Children) {
                                (v as Label).FontAttributes = FontAttributes.None;
                                (v as Label).TextColor = Color.Green;
                                (v as Label).TextColor = Management.ColorManager.TextColor;
                            }
                            lbl.FontAttributes = FontAttributes.Bold;
                            lbl.TextColor = Color.Green;
                            lbl.TextColor = Management.ColorManager.TextColor;
                            SelectedTab = tab;
                        })
                    }
                },
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };
            if(bar.Children.Count == 0) {
                lbl.FontAttributes = FontAttributes.Bold;
                SelectedTab = tab;
            }
            bar.Children.Add(lbl);
        }

    }
}