using School_Project.Controls;
using School_Project.Information;
using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Tabs.PracticeFolder {
    public class VocabCard : Frame {

        private bool foreign;
        StackLayout layout;
        private VocabPractice practice;

        public VocabCard(VocabPractice practice, bool foreign) {

            this.practice = practice;
            this.foreign = foreign;

            CornerRadius = Settings.CornerRadius;
            BackgroundColor = Color.FromHsla(practice.Color, ColorManager.DarkMode ? .7 : .4, ColorManager.DarkMode ? .1 : .8);
            VerticalOptions = LayoutOptions.FillAndExpand;

            layout = new StackLayout {
                Children = {
                    new Label {
                        Text = foreign ? practice.Foreign : practice.Question,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = Settings.FontSize,
                        HorizontalTextAlignment = TextAlignment.Center
                    },
                    new TextBox {
                        Placeholder = "Bedeutung"
                    }
                }
            };
            Content = layout;
            
            for(int i = 0; i < practice.Properties.Count; i++) {
                layout.Children.Add(
                    new TextBox {
                        Placeholder = practice.Properties[i]
                    }
                );
            }

        }

        public bool Check() {
            bool props = true;
            for(int i = 2; i < layout.Children.Count; i++) {
                TextBox tb = layout.Children[i] as TextBox;
                if(tb.Text != practice.Values[i - 2]) {
                    props = false;
                    tb.Text = practice.Values[i - 2];
                    tb.Error();
                    break;
                }
            }
            TextBox first = layout.Children[1] as TextBox;
            bool firstRight = first.Text == (foreign ? practice.Question : practice.Foreign);
            first.Text = foreign ? practice.Question : practice.Foreign;
            if(!firstRight) first.Error();
            return props && firstRight;
        }

    }
}
