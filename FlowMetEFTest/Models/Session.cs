using System;
using System.Collections.Generic;

namespace FlowMetEFTest.Models
{
    public partial class Session
    {
        public Session()
        {
            Measurement = new HashSet<Measurement>();
        }

        public int SessionID { get; set; }
        public DateTime? EndDate { get; set; }
        public int MeasurementPositionID { get; set; }
        public DateTime? StartDate { get; set; }
        public int StudyID { get; set; }

        public virtual ICollection<Measurement> Measurement { get; set; }
        public virtual MeasurementPosition MeasurementPosition { get; set; }
        public virtual Study Study { get; set; }
    }
}
