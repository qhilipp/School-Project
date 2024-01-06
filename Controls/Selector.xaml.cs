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
    public partial class Selector : Frame {

        public delegate void ChangeDelegate();
        public ChangeDelegate Change;

        private string selectedItem;
        public string SelectedItem {
            get {
                return selectedItem;
            }
            set {
                selectedItem = value;
                picker.SelectedItem = selectedItem;
                Change?.Invoke();
            }
        }

        public int SelectedIndex {
            get {
                return picker.SelectedIndex;
            }
            set {
                picker.SelectedIndex = value;
                Change?.Invoke();
            }
        }

        public Selector() {
            InitializeComponent();
            CornerRadius = Settings.CornerRadius;
            picker.SelectedIndexChanged += (s, e) => {
                if(picker.SelectedIndex != -1) selectedItem = picker.Items[picker.SelectedIndex];
                Change?.Invoke();
            };
        }

        public void Add(string item) {
            picker.Items.Add(item);
            picker.SelectedIndex = 0;
        }
        public void Add(params string[] items) {
            foreach(string item in items) {
                picker.Items.Add(item);
            }
            picker.SelectedIndex = 0;
        }
        public void Clear() {
            picker.Items.Clear();
        }

    }
}