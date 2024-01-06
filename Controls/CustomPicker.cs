using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Controls {
    public class CustomPicker : Picker {

        public CustomPicker() {
            FontSize = Settings.FontSize;
            BackgroundColor = ColorManager.OverlayColor;
        }

    }
}
