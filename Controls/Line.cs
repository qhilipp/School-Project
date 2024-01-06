using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Controls {
    public class Line : BoxView {

        public Line() {
            Margin = new Thickness(25, 10);
            BackgroundColor = ColorManager.PlaceholderColor;
            HeightRequest = 1;
        }

    }
}
