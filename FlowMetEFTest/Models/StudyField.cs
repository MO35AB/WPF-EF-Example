using System;
using System.Collections.Generic;

namespace FlowMetEFTest.Models
{
    public partial class StudyField
    {
        public int StudyFieldID { get; set; }
        public int FieldID { get; set; }
        public int StudyID { get; set; }
        public string Value { get; set; }

        public virtual Field Field { get; set; }
        public virtual Study Study { get; set; }
    }
}
