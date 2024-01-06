using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Tabs.Templates {

    [ContentProperty("Settings")]
    public class SettingsTab : Frame {

        private StackLayout layout;

        public IList<View> Settings {
            get {
                return layout.Children;
            }
        }

        public SettingsTab() {

            Padding = 0;

            ScrollView sv = new ScrollView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
            };
            Content = sv;

            layout = new StackLayout();
            sv.Content = layout;

        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            double ratio = width / height;
            if(ratio < 1) {
                HorizontalOptions = LayoutOptions.Fill;
            } else {
                HorizontalOptions = LayoutOptions.Center;
                WidthRequest = height;
            }
        }

    }
}
