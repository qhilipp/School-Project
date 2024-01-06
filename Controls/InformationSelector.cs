using School_Project.Management;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Controls {
    public class InformationSelector<T> : NeutralButton where T : class, IElement<T>, new() {

        public delegate string ToNameDelegate(T element);
        private ToNameDelegate ToName;

        public delegate void ChangeDelegate();
        public event ChangeDelegate Change;

        private T selectedElement;
        public T SelectedElement {
            get {
                return selectedElement;
            }
            set {
                selectedElement = value;
                Text = ToName(value);
                Change?.Invoke();
            }
        }

        public InformationSelector(ToNameDelegate toName) {

            ToName = toName;
            Text = new T().GetName() + " wählen";

            Click += (btn) => {
                ElementList<T> list = new ElementList<T>(true);
                list.Click += (view) => {
                    SelectedElement = view.Element;
                    Navigation.PopModalAsync();
                };
                list.AddItem += (element) => {
                    if(element == null) return;
                    SelectedElement = element;
                    Navigation.PopModalAsync();
                };
                ContentPage page = new ContentPage { Content = list, Padding = 10, BackgroundColor = ColorManager.BackgroundColor };
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushModalAsync(page);
            };

        }
        
    }
}
