using School_Project.Controls;
using School_Project.Information;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Tabs.PracticeFolder {
    public class PracticeDisplay<P> : ContentView where P : Practice {

        public delegate void FinishDelegate();
        public FinishDelegate Finish;

        StackLayout content, buttons;

        private bool isCheck = false;

        public IList<View> Answers {
            get {
                return content.Children;
            }
        }

        public LayoutOptions AnswerOptions {
            get {
                return content.HorizontalOptions;
            }
            set {
                content.HorizontalOptions = value;
            }
        }

        public P Practice { get; set; }

        public PracticeDisplay(List<Practice> practices, int index) {

            Practice = (P) practices[index];
            content = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand };
            AddButton apply = new AddButton { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.EndAndExpand, Text = "Überprüfen" };
            apply.Click += (btn) => {
                if(!isCheck) {
                    isCheck = true;
                    bool isCorrect = Check();
                    Practice.PushScore(isCorrect);
                    Label lbl = new Label {
                        Text = isCorrect ? "Richtig" : "Falsch",
                        TextColor = isCorrect ? Color.Green : Color.Red,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Opacity = 0
                    };
                    (Content as StackLayout).Children.Insert(1, lbl);
                    lbl.FadeTo(1, 500);
                    apply.Text = "Weiter";
                } else {
                    ContentView display = PracticeTab.GetPracticeDisplay(practices, index == practices.Count - 1 ? 0 : index + 1);
                    (Navigation.ModalStack[Navigation.ModalStack.Count - 1] as ContentPage).Content = display;
                }
            };

            buttons = new StackLayout {
                Children = {
                    new RemoveButton {
                        Text = "Zurück",
                        Click = (btn) => {
                            Finish?.Invoke();
                            Navigation.PopModalAsync();
                        }
                    },
                    apply
                }
            };

            Content = new StackLayout {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Fill,
                Padding = new Thickness(10),
                Children = {
                    content,
                    buttons
                }
            };

        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            double ratio = width / height;
            if(ratio > 1) {
                Content.WidthRequest = height;
            }
        }

        public virtual bool Check() {
            return true;
        }

    }
}
