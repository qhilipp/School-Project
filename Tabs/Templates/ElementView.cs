using School_Project.Management;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace School_Project.Tabs.Templates {
    public abstract class ElementView<T> : Frame {

        public delegate void ClickDelegate();
        public ClickDelegate Click;

        private T element;
        public T Element {
            get {
                return element;
            }
            set {
                element = value;
                Change();
            }
        }

        public ElementView(bool hasRecognizer = true) {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            CornerRadius = Settings.CornerRadius;
            if(hasRecognizer) GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => { Click?.Invoke(); }) });
        }

        protected abstract void Change();

    }
}
