using System;
using System.Collections.Generic;

namespace FlowMetEFTest.Models
{
    public partial class MeasurementPosition
    {
        public MeasurementPosition()
        {
            Session = new HashSet<Session>();
        }

        public int MeasurementPositionID { get; set; }
        public byte Digit { get; set; }
        public bool Location { get; set; }
        public bool Side { get; set; }

        public virtual ICollection<Session> Session { get; set; }
    }
}
