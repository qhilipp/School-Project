using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Controls {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RemoveButton : Label {
        
        public delegate void ClickDelegate(RemoveButton btn);
        public ClickDelegate Click;

        public RemoveButton() {
            InitializeComponent();
            BackgroundColor = Color.Transparent;
            GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => { Click(this); }) });
        }
    }
}