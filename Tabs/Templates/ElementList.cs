using School_Project.Controls;
using School_Project.Management;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace School_Project.Tabs.Templates {
    public class ElementList<T> : Frame where T : class, IElement<T>, new() {

        public delegate AddPage<T> GetAddPageDelegate();
        public GetAddPageDelegate GetAddpage;

        public delegate ElementView<T> GetElementViewDelegate(T element);
        public GetElementViewDelegate GetElementView;

        public delegate void ClickDelegate(ElementView<T> view);
        public event ClickDelegate Click;

        public delegate void AddItemDelegate(T element);
        public AddItemDelegate AddItem;

        private StackLayout layout;
        private StackLayout list;

        private Label infoLabel;
        private NeutralButton addButton;

        public bool KeepAddButton { get; set; }

        private bool hasAddButton = true;
        public bool HasAddButton {
            get {
                return hasAddButton;
            }
            set {
                if(value && list.Children.Count == 0 || KeepAddButton && !layout.Children.Contains(addButton)) {
                    layout.Children.Add(addButton);
                }
                if((!value || list.Children.Count != 0 && !KeepAddButton) && layout.Children.Contains(addButton)) {
                    layout.Children.Remove(addButton);
                }
                hasAddButton = value;
            }
        }

        public bool AutoSave { get; set; }

        public ElementList(bool hasAddButton = true, bool preLoad = true) {
            Init(hasAddButton, preLoad);
        }

        public ElementList() {
            Init(true, false);
        }

        private void Init(bool hasAddButton, bool preLoad) {
            AutoSave = true;
            HorizontalOptions = LayoutOptions.Fill;
            VerticalOptions = LayoutOptions.Fill;
            Padding = 0;
            BackgroundColor = ColorManager.BackgroundColor;
            CornerRadius = Settings.CornerRadius;

            ScrollView scroll = new ScrollView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };
            Content = scroll;

            layout = new StackLayout {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
            };
            scroll.Content = layout;

            list = new StackLayout { Padding = 0, HorizontalOptions = LayoutOptions.Fill };
            layout.Children.Add(list);

            infoLabel = new Label {
                Text = "Keine " + new T().GetName(true) + " vorhanden",
                TextColor = ColorManager.PlaceholderColor,
                FontSize = Settings.SmallFontSize,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };
            layout.Children.Add(infoLabel);
            addButton = new NeutralButton {
                Text = new T().GetName() + " erstellen",
                HorizontalOptions = LayoutOptions.Center,
                Click = (btn) => {
                    AddPage<T> add = null;
                    if(GetAddpage != null) {
                        add = GetAddpage();
                    } else {
                        add = new T().GetAddPage();
                    }
                    add.AutoSave = AutoSave;
                    add.Finish += (element) => {
                        Add(element);
                        AddItem?.Invoke(element);
                    };
                    Navigation.PushModalAsync(add);
                }
            };
            layout.Children.Add(addButton);

            HasAddButton = hasAddButton;
            if(preLoad) Load();
            else UpdateInfoLabel();
        }

        public List<T> GetItems() {
            List<T> items = new List<T>();
            foreach(var view in list.Children) {
                items.Add((view as ElementView<T>).Element);
            }
            return items;
        }

        public void Clear() {
            list.Children.Clear();
            UpdateInfoLabel();
        }

        public void Load() {
            Load(new T().GetManager().Load());
        }

        public void Load(List<T> elements) {
            Clear();
            elements = elements.OrderBy(s => -s.GetScore()).ToList();
            foreach(T element in elements) Add(element);
            if(elements.Count != 0) UpdateInfoLabel();
        }

        public void UpdateInfoLabel() {
            infoLabel.IsVisible = list.Children.Count == 0;
            list.IsVisible = list.Children.Count != 0;
            HasAddButton = hasAddButton;
        }

        public void Add(T element) {
            AddItem?.Invoke(element);
            ElementView<T> view = null;
            if(GetElementView == null) view = element.GetView();
            else view = GetElementView(element);
            view.Click += () => Click?.Invoke(view);
            list.Children.Add(view);
            UpdateInfoLabel();
        }
        public void Remove(T element) {
            for(int i = Children.Count - 1; i >= 0; i--) {
                if(((ElementView<T>) list.Children[i]).Element.ToKey() == element.ToKey()) {
                    list.Children.RemoveAt(i);
                }
            }
            UpdateInfoLabel();
        }

    }
}
