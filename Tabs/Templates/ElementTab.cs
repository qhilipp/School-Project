using School_Project.Management;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace School_Project.Tabs.Templates {
    public abstract class ElementTab<T> : ContentTab where T : class, IElement<T>, new() {

        protected ElementList<T> list;
        double initHeight;

        public ElementTab() {

            HasTitle = true;
            Title = new T().GetName();

            list = new ElementList<T>(false, false);
            list.Click += (view) => {
                Select(view.Element);
            };
            TabContent = list;

        }

        public void Refresh() {
            list.Clear();
            Load();
        }

        protected void AddToList(T element) {
            list.Add(element);
            list.HeightRequest = initHeight;
        }

        protected abstract void Select(T element);
        protected abstract void Load();

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            if(initHeight == 0 && list != null) initHeight = list.Height;
        }

    }
}
