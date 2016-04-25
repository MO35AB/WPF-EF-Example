using System;
using System.Collections.Generic;

namespace FlowMetEFTest.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Study = new HashSet<Study>();
        }

        public int PatientID { get; set; }
        public byte? Age { get; set; }
        public double? Height { get; set; }
        public string Identifier { get; set; }
        public string Notes { get; set; }
        public bool? Sex { get; set; }
        public double? Weight { get; set; }

        public virtual ICollection<Study> Study { get; set; }
    }
}
