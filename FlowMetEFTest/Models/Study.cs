using System;
using System.Collections.Generic;

namespace FlowMetEFTest.Models
{
    public partial class Study
    {
        public Study()
        {
            Session = new HashSet<Session>();
            StudyField = new HashSet<StudyField>();
        }

        public int StudyID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
        public int PatientID { get; set; }
        public DateTime StartDate { get; set; }

        public virtual ICollection<Session> Session { get; set; }
        public virtual ICollection<StudyField> StudyField { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
