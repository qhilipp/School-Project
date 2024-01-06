using School_Project.Management;
using Xamarin.Forms;

namespace School_Project.Controls {
    public class HeaderLabel : Label {

        public HeaderLabel() {
            FontSize = 80;
            TextColor = ColorManager.TextColor;
            FontAttributes = FontAttributes.Bold;
            HorizontalOptions = LayoutOptions.Fill;
            HorizontalTextAlignment = TextAlignment.Center;
        }

    }
}
