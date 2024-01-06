using School_Project.Information;
using System;
using System.Collections.Generic;
using System.Text;

namespace School_Project.Controls {
    public class SubjectSelector : InformationSelector<Subject> {

        public SubjectSelector() : base((element) => { return element.Name; }) {

        }

    }
}
