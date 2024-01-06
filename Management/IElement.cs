using School_Project.Tabs.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Management {
    public interface IElement<T> where T : class, IElement<T>, new() {

        string ToKey();
        void FromStrings(string[] props);
        T Clone();
        ElementView<T> GetView();
        FileManager<T> GetManager();
        string GetName(bool plural = false);
        int GetScore();
        AddPage<T> GetAddPage();

    }
}
