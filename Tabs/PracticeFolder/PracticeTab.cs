using School_Project.Controls;
using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace School_Project.Tabs.PracticeFolder {
    public class PracticeTab : ElementTab<Practice> {

        private Theme theme;

        public PracticeTab(Theme theme) {

            this.theme = theme;

            Title = theme.Name + " " + new MultiPractice().GetName(true);
            AddItem += () => {
                AddPractice addPractice = new AddPractice(theme.Subject);
                addPractice.Finish += (practice) => {
                    practice.Theme = theme;
                    Managers.PracticeManager.Save(practice);
                    Refresh();
                };
                Navigation.PushModalAsync(addPractice);
            };
            Load();

        }

        protected override void Load() {
            list.Load(Managers.PracticeManager.Load(theme));
        }

        protected override void Select(Practice element) {
            List<Practice> elements = list.GetItems();
            ContentView display = GetPracticeDisplay(elements, elements.IndexOf(element), Load);
            Navigation.PushModalAsync(new ContentPage { Content = display });
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            if(Buttons.Count == 2) Buttons[0].WidthRequest = Buttons[1].WidthRequest;
        }

        public static ContentView GetPracticeDisplay(List<Practice> elements, int index, FinishDelegate finish = null) {
            if(elements[index].GetType() == typeof(MultiPractice)) {
                return new MultiPracticeDisplay(elements, index) { Finish = () => finish?.Invoke() };
            } else if(elements[index].GetType() == typeof(TextPractice)) {
                return new TextPracticeDisplay(elements, index) { Finish = () => finish?.Invoke() };
            } else if(elements[index].GetType() == typeof(VocabPractice)) {
                return new VocabPracticeDisplay(elements, index) { Finish = () => finish?.Invoke() };
            }
            return null;
        }

    }
}