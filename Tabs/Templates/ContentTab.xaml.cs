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
    public partial class ContentTab : ContentView {

        public delegate void FinishDelegate();
        public FinishDelegate Finish;

        public delegate void AddItemDelegate();
        public AddItemDelegate AddItem;

        private Controls.HeaderLabel title;
        public string Title {
            get {
                return title.Text;
            }
            set {
                title.Text = value;
                HasTitle = true;
            }
        }

        private bool hasTitle = false;
        public bool HasTitle {
            get {
                return hasTitle;
            }
            set {
                if(value && !hasTitle) {
                    layout.Children.Insert(0, title);
                } if(!value && hasTitle) {
                    layout.Children.RemoveAt(0);
                }
                hasTitle = value;
            }
        }

        public View TabContent {
            get {
                return frame.Content;
            }
            set {
                frame.Content = value;
            }
        }

        public IList<View> Buttons {
            get {
                return buttons.Children;
            }
        }

        public IList<View> LayoutChildren {
            get => layout.Children;
        }

        public ContentTab() {
            InitializeComponent();
            add.Click += (btn) => {
                AddItem?.Invoke();
            };
            title = new Controls.HeaderLabel();
            title.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    Finish?.Invoke();
                    Navigation.PopModalAsync();
                })
            });
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            double ratio = width / height;
            buttons.Orientation = ratio < 1 ? StackOrientation.Vertical : StackOrientation.Horizontal;
            buttons.HorizontalOptions = ratio < 1 ? LayoutOptions.FillAndExpand : LayoutOptions.CenterAndExpand;
            if(title != null) title.WidthRequest = Math.Min(width, height);
        }

    }
}