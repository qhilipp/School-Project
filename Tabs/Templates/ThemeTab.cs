using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace School_Project.Tabs.NotesFolder {
    public class ThemeTab : ElementTab<Theme> {

        public delegate View CreateTabDelegate(Theme theme);
        private CreateTabDelegate CreateTab;

        private Subject subject;

        public ThemeTab(Subject subject, CreateTabDelegate createTab) {

            this.subject = subject;
            CreateTab = createTab;
            Title = subject.Name + " " + new Theme().GetName(true);
            AddItem += () => {
                AddTheme addTheme = new AddTheme(subject);
                addTheme.AutoSave = false;
                addTheme.Finish += (theme) => {
                    theme.Subject = subject;
                    Managers.ThemeManager.Save(theme);
                    AddToList(theme);
                };
                Navigation.PushModalAsync(addTheme);
            };
            Load();
        }

        protected override void Load() {
            foreach(Theme theme in Managers.ThemeManager.Load(subject)) AddToList(theme);
        }

        protected override void Select(Theme element) {
            ContentPage page = new ContentPage { Content = CreateTab(element) };
            NavigationPage.SetHasNavigationBar(page, false);
            Navigation.PushModalAsync(page);
        }
    }
}
