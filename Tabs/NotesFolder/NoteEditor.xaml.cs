using School_Project.Controls;
using School_Project.Information;
using School_Project.Management;
using School_Project.Tabs.Templates;
using School_Project.Views.Subjects;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace School_Project.Tabs.NotesFolder {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditor : ContentPage {

        public delegate void FinishDelegate(Note element);
        public FinishDelegate Finish;

        public Note Element { get; set; }
        private Note old;
        private Theme theme;

        Editor editor;
        WebView view;

        public NoteEditor(Theme theme, Note old = null) {
            InitializeComponent();

            this.theme = theme;
            this.old = old;
            Element = new Note { Theme = theme };

            view = new WebView {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill
            };

            editor = new Editor {
                AutoSize = EditorAutoSizeOption.Disabled,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                TextColor = ColorManager.TextColor,
                IsSpellCheckEnabled = false,
                IsTextPredictionEnabled = false,
            };
            editor.TextChanged += (s, e) => Element.Content = editor.Text;
            content.Content = editor;

            back.Text = "<" + theme.Name;
            back.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => {
                if((old == null && name.Text == "") || Save()) {
                    Finish?.Invoke(Element);
                    Navigation.PopModalAsync();
                }
            }) });

            name.TextChange += (txt) => {
                if(old == null) {
                    bool prev = Element.IsLink;
                    Element.IsLink = IsValidURL(name.Text);
                    if(Element.IsLink) view.Source = name.Text;
                    else Element.Content = editor.Text;
                    if(prev != Element.IsLink) SetView(Element.IsLink ? 2 : 0);
                }
                Element.Name = GetName(name.Text);
                if(Element.IsLink) Element.Content = name.Text;
            };

            share.Click += (btn) => {
                if(Element.IsLink) {
                    Share.RequestAsync(new ShareTextRequest {
                        Text = "Hi, diese Notiz habe ich mit School Project erstellt. Hier kannst Du sie Dir angucken: " + name.Text
                    });
                } else {
                    string dir = Path.Combine(FileSystem.AppDataDirectory, "Shared");
                    if(!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    int n = 1;
                    while(File.Exists(Path.Combine(dir, "Notiz " + n + ".html"))) n++;
                    string path = Path.Combine(dir, "Notiz " + n + ".html");
                    StreamWriter sw = new StreamWriter(path);
                    sw.Write(editor.Text);
                    sw.Close();
                    Share.RequestAsync(new ShareFileRequest { File = new ShareFile(path) });
                }
            };

            if(old != null) {
                Element = old.Clone();
                name.Placeholder = Element.IsLink ? "URL" : "Name";
                if(Element.IsLink) {
                    name.Text = Element.Content;
                } else {
                    editor.Text = Element.Content;
                    name.Text = Element.Name;
                }
                SetView(Element.IsLink ? 2 : 1);
            } else {
                SetView(0);
            }

        }

        public bool Save() {
            string errorMsg = IsValidName();
            if(errorMsg != "") {
                DisplayAlert("School Project", errorMsg, "Ok");
                return false;
            }
            Element.Theme = theme;
            if(old != null) {
                Managers.NoteManager.Edit(old, Element);
            } else {
                Managers.NoteManager.Save(Element);
            }
            return true;
        }

        public string IsValidName() {
            string errorMsg = "";
            if(GetName(name.Text) == "") errorMsg = "Der Name darf nicht nur aus Sonderzeichen bestehen";
            if(name.Text == "") errorMsg = "Du musst erst einen Namen für die Notiz angeben";
            foreach(Note note in Managers.NoteManager.Load(theme)) {
                if(note.ToKey() == GetName(name.Text)) {
                    if((old != null && old.ToKey() != note.ToKey()) || old == null) {
                        errorMsg = "Diese Notiz existiert bereits";
                        break;
                    }
                }
            }
            return errorMsg;
        }

        public void SetView(int id) {
            if(id < 2) {
                NeutralButton mode = new NeutralButton();
                mode.HorizontalOptions = LayoutOptions.End;
                mode.Text = "Editor";
                mode.Click += (btn) => {
                    int index = mode.Text == "Editor" ? 1 : 0;
                    mode.Text = index == 0 ? "Editor" : "Ansicht";
                    if(index == 0) {
                        content.Content = editor;
                    } else if(index == 1) {
                        string preset = "<style>*{" +
                        "background-color:" + "#" + ColorManager.BackgroundColor.ToHex().Substring(3, 6) + ";" +
                        "color:" + "#" + ColorManager.TextColor.ToHex().Substring(3, 6) + ";" +
                        "font-size:" + Settings.FontSize + ";" +
                        "font-family: arial;" +
                        "}</style>";
                        view.Source = new HtmlWebViewSource { Html = preset + editor.Text };
                        content.Content = view;
                    }
                };
                if(id == 1) mode.Click.Invoke(mode);
                options.Content = mode;
                content.Content = id == 0 ? editor : (View) view;
            }
            else if(id == 2) {
                view.Source = name.Text;
                content.Content = view;
                StackLayout layout = new StackLayout {
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    Spacing = 10,
                    Orientation = StackOrientation.Horizontal
                };
                options.Content = layout;
                Label pageBack = new Label {
                    Text = "<",
                    TextColor = ColorManager.PlaceholderColor,
                    VerticalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                layout.Children.Add(pageBack);
                Label pageForth = new Label {
                    Text = ">",
                    TextColor = ColorManager.PlaceholderColor,
                    VerticalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                layout.Children.Add(pageForth);
                pageBack.GestureRecognizers.Add(new TapGestureRecognizer {
                    Command = new Command(() => {
                        view.GoBack();
                    })
                });
                pageForth.GestureRecognizers.Add(new TapGestureRecognizer {
                    Command = new Command(() => {
                        view.GoForward();
                    })
                });
                view.PropertyChanged += (s, e) => {
                    pageBack.TextColor = view.CanGoBack ? ColorManager.TextColor : ColorManager.PlaceholderColor;
                    pageForth.TextColor = view.CanGoForward ? ColorManager.TextColor : ColorManager.PlaceholderColor;
                };
            }
        }

        public bool IsValidURL(string url) {
            if(url.Contains(".")) {
                var match = System.Text.RegularExpressions.Regex.IsMatch(url, @"^https?:\/\/", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if(!match) {
                    url = $"http://{url}";
                }
            }
            Uri uriResult = null;
            var result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
        public string GetName(string url) {
            if(Element.IsLink) return GetURLName(url);
            string name = "";
            foreach(char c in url.ToLower()) {
                if(c >= ' ' && c <= '}') name += c;
            }
            return name;
        }
        public string GetURLName(string Url) {
            return System.Text.RegularExpressions.Regex.Replace(
                Url,
                @"^([a-zA-Z]+:\/\/)?([^\/]+)\/.*?$",
                "$2"
            );
        }

    }
}