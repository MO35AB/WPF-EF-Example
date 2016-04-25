using System;
using System.Collections.Generic;

namespace FlowMetEFTest.Models
{
    public partial class Field
    {
        public Field()
        {
            StudyField = new HashSet<StudyField>();
        }

        public int FieldID { get; set; }
        public string Label { get; set; }

        public virtual ICollection<StudyField> StudyField { get; set; }
    }
}
