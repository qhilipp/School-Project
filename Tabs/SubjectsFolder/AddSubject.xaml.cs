using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.SubjectsFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddSubject : AddPage<Subject> {

        public AddSubject(Subject old = null) : base(old) {
            InitializeComponent();
            Color[] colors = (Color[]) ColorManager.Colors.Clone();
            foreach(var pair in Managers.SubjectManager.Elements) {
                colors[pair.Value.Color] = Color.Transparent;
            }
            int firstNonExisting = 0;
            while(firstNonExisting < colors.Length && firstNonExisting < colors.Length && colors[firstNonExisting] == Color.Transparent) firstNonExisting++;
            if(firstNonExisting == colors.Length) firstNonExisting = 0;
            color.Color = firstNonExisting;
            Finish += (element) => {
                if(element != null) {
                    Managers.ThemeManager.Init();
                    Managers.NoteManager.Init();
                    Managers.PracticeManager.Init();
                }
            };
            lk.Change += (val) => {
                written.IsChecked = true;
                abi.IsChecked = true;
            };
        }

        protected override string Check() {
            string errorMsg = "";
            if(name.Text == "") {
                name.Error();
                errorMsg = "Du musst einen Namen für das Fach angeben";
            }
            foreach(var pair in Managers.SubjectManager.Elements) {
                if(pair.Key == name.Text.ToLower()) {
                    if((Old != null && Old.ToKey() != pair.Key) || Old == null) {
                        errorMsg = "Dieses Fach existiert bereits";
                        break;
                    }
                }
            }
            return errorMsg;
        }
        protected override void Construct() {
            Element.Name = name.Text.Replace(",", ";");
            Element.Teacher = teacher.Text.Replace(",", ";");
            Element.Room = room.Text.Replace(",", ";");
            Element.LK = lk.IsChecked;
            Element.Written = written.IsChecked;
            Element.Abi = abi.IsChecked;
            Element.Addition = addition.IsChecked;
            Element.Color = color.Color;
        }
        protected override void Delete() {
            Remove(Old, Managers.HomeworkManager, (Homework h) => { return h.Subject.ToKey(); });
            Remove(Old, Managers.AppointmentManager, (Appointment a) => { return a.Subject.ToKey(); });
            Remove(Old, Managers.ExamManager, (Exam e) => { return e.Subject.ToKey(); });
            Managers.ThemeManager.Delete(new Theme { Subject = Old });
        }
        protected override void Edit() {
            Remove(Old, Managers.HomeworkManager, (Homework h) => { return h.Subject.ToKey(); }, Element, (homework, subject) => { homework.Subject = subject; });
            Remove(Old, Managers.AppointmentManager, (Appointment a) => { return a.Subject.ToKey(); }, Element, (appointment, subject) => { appointment.Subject = subject; });
            Remove(Old, Managers.ExamManager, (Exam e) => { return e.Subject.ToKey(); }, Element, (exam, subject) => { exam.Subject = subject; });
            Managers.ThemeManager.Edit(new Theme { Subject = Old }, new Theme { Subject = Element });
        }
        protected override void Load() {
            name.Text = Old.Name;
            teacher.Text = Old.Teacher;
            room.Text = Old.Room;
            color.TextColor = ColorManager.GetColor(Old.Color);
            lk.IsChecked = Old.LK;
            written.IsChecked = Old.Written;
            abi.IsChecked = Old.Abi;
            addition.IsChecked = Old.Addition;
            color.Color = Old.Color;
        }

        protected override string DeleteRequest() {
            return $"Soll dieses Fach wirklich unwiederruflich gelöscht werden? Alle Termine und Hausaufgaben im Zusammenhang mit diesem Fach werden ebenfalls gelöscht.";
        }

        private delegate string ToNameDelegate<T>(T t);
        private delegate void AddSubjectDelegate<T>(T t, Subject subject);
        private void Remove<T>(Subject subject, FileManager<T> manager, ToNameDelegate<T> toName, Subject replacement = null, AddSubjectDelegate<T> addSubject = null) where T : class, IElement<T>, new() {
            List<KeyValuePair<string, T>> list = manager.Elements.ToList();
            for(int i = list.Count - 1; i >= 0; i--) {
                if(toName(list[i].Value) == subject.ToKey()) {
                    manager.Delete(list[i].Value);
                    manager.Elements.Remove(list[i].Value.ToKey());
                    if(replacement != null) {
                        T newT = list[i].Value;
                        addSubject(newT, replacement);
                        manager.Elements[newT.ToKey()] = newT;
                        manager.Save(list[i].Value);
                    }
                }
            }
        }

    }
}