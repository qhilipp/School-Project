using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace School_Project.Tabs.NotesFolder {
    public class NoteTab : ElementTab<Note> {

        private Theme theme;

        public NoteTab(Theme theme) {

            this.theme = theme;

            Title = theme.Name + " " + new Note().GetName(true);
            AddItem += () => {
                NoteEditor editor = new NoteEditor(theme);
                editor.Finish += (element) => {
                    Refresh();
                };
                Navigation.PushModalAsync(editor);
            };
            Load();

        }

        protected override void Load() {
            foreach(Note note in Managers.NoteManager.Load(theme)) AddToList(note);
        }

        protected override void Select(Note element) {
            NoteEditor editor = new NoteEditor(theme, element);
            editor.Finish += (note) => {
                Refresh();
            };
            Navigation.PushModalAsync(editor);
        }
    }
}