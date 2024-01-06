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

namespace School_Project.Tabs.Templates {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("Options")]
    public abstract partial class AddPage<T> : ContentPage where T : class, IElement<T>, new() {

        public delegate void FinishDelegate(T element);
        public FinishDelegate Finish;

        private StackLayout layout;
        private StackLayout options;
        private StackLayout buttons;

        private bool loaded = false;

        public const int OptionSpacing = 15;

        public IList<View> Options {
            get {
                return options.Children;
            }
        }

        public T Element {
            get; set;
        }
        public T Old {
            get; set;
        }

        public bool AutoSave { get; set; }

        public AddPage(T old) {

            AutoSave = true;

            Element = new T();
            Old = old;
            BackgroundColor = ColorManager.BackgroundColor;
            Resources = new ResourceDictionary();
            Resources.Add(new Style(typeof(StackLayout)) { Setters = { new Setter { Property = StackLayout.SpacingProperty, Value = OptionSpacing } } });
            //Resources.Add(new Style(typeof(Label)) { 
            //    Setters = { new Setter { Property = Label.FontSizeProperty, Value = Management.Settings.UISettings.SmallFontSize },
            //        new Setter {Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center }
            //    }
            //});

            NavigationPage.SetHasNavigationBar(this, false);

            ScrollView scrollView = new ScrollView {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            Content = scrollView;

            layout = new StackLayout {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(10)
            };
            scrollView.Content = layout;

            Controls.HeaderLabel title = new Controls.HeaderLabel {
                Text = new T().GetName() + (old == null ? " erstellen" : " bearbeiten")
            };
            title.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => Navigation.PopModalAsync())
            });
            layout.Children.Add(title);

            options = new StackLayout {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Spacing = OptionSpacing
            };
            layout.Children.Add(options);

            buttons = new StackLayout {
                Spacing = 5
            };
            layout.Children.Add(buttons);

            buttons.Children.Add(new RemoveButton {
                Text = old == null ? "Abbrechen" : "Entfernen",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Click = Cancel
            });

            buttons.Children.Add(new AddButton {
                Text = old == null ? "Erstellen" : "Speichern",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Click = Add
            });

        }

        private void Add(AddButton btn) {
            string msg = Check();
            if(msg == "") {
                Construct();
                if(AutoSave) {
                    if(Old != null) {
                        Edit();
                        new T().GetManager().Edit(Old, Element);
                    } else {
                        new T().GetManager().Save(Element);
                    }
                }
                Finish?.Invoke(Element);
                Navigation.PopModalAsync();
            } else {
                DisplayAlert("School Project", msg, "Ok");
            }
        }
        private async void Cancel(RemoveButton btn) {
            if(Old != null) {
                bool delete = await DisplayAlert("School Project", DeleteRequest(), "Ja", "Nein");
                if(!delete)
                    return;
                Delete();
                new T().GetManager().Delete(Old);
                Finish?.Invoke(null);
            }
            await Navigation.PopModalAsync();
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            double ratio = width / height;
            if(ratio > 1) {
                layout.WidthRequest = height;
            }
        }
        protected override void OnAppearing() {
            base.OnAppearing();
            if(Old != null && !loaded) {
                Load();
                loaded = true;
            }
        }

        protected abstract string Check();
        protected abstract void Edit();
        protected abstract void Delete();
        protected abstract void Load();
        protected abstract void Construct();

        protected virtual string DeleteRequest() {
            return "Wirklich löschen?";
        }

    }
}