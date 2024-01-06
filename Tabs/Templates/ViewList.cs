using School_Project.Controls;
using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Tabs.Templates {
    public class ViewList : StackLayout {

        public delegate void ClickDelegate(View v);
        public event ClickDelegate Click;

        public delegate void AddItemDelegate();
        public AddItemDelegate AddItem;

        private StackLayout layout;

        private bool hasAddButton = true;
        public bool HasAddButton {
            get {
                return hasAddButton;
            }
            set {
                if(value && layout != null && layout.Children.Count == 1) {
                    layout.Children.Add(new NeutralButton {
                        Text = "Hinzufügen",
                        HorizontalOptions = LayoutOptions.Fill,
                        Click = (btn) => {
                            AddItem?.Invoke();
                        }
                    });
                } else if(layout != null && layout.Children.Count == 2) {
                    layout.Children.RemoveAt(1);
                }
                hasAddButton = value;
            }
        }

        public ViewList() {

            Spacing = Settings.Spacing;

        }

        public void Add(View v) {
            v.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => Click?.Invoke(v)) });
            Children.Add(v);
        }

    }
}
