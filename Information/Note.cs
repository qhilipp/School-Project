using School_Project.Management;
using School_Project.Tabs.NotesFolder;
using School_Project.Tabs.Templates;

namespace School_Project.Information {
    public class Note : IElement<Note> {

        public string Name { get; set; }
        public Theme Theme { get; set; }
        public string Content { get; set; }
        public bool IsLink { get; set; }

        public Note Clone() {
            return new Note {
                Name = Name,
                Theme = Theme.Clone(),
                Content = Content,
                IsLink = IsLink
            };
        }

        public override string ToString() {
            return $"{Name},{Theme.ToKey()},{(IsLink ? "1" : "0")}{(IsLink ? "," + Content : "")}";
        }

        public void FromStrings(string[] props) {
            Name = props[0];
            Theme = Managers.ThemeManager.Elements[props[1].ToLower()];
            IsLink = props[2] == "1";
            Content = props[3];
        }

        public FileManager<Note> GetManager() {
            return Managers.NoteManager;
        }

        public string GetName(bool plural = false) {
            return plural ? "Notizen" : "Notiz";
        }

        public ElementView<Note> GetView() {
            return new NoteView { Element = this };
        }

        public string ToKey() {
            return Name.ToLower();
        }

        public int GetScore() {
            return 1;
        }

        public AddPage<Note> GetAddPage() {
            return null;
        }
    }
}
