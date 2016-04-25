using System;
using System.Collections.Generic;

namespace FlowMetEFTest.Models
{
    public partial class Measurement
    {
        public int MeasurementID { get; set; }
        public double? DiastolicMin { get; set; }
        public double FilteredFlow { get; set; }
        public DateTime GlobalTime { get; set; }
        public double? HeartRate { get; set; }
        public bool MeasurementType { get; set; }
        public double PositionInTime { get; set; }
        public double? RawSpeckleContrastSquared { get; set; }
        public int SessionID { get; set; }
        public double? SystolicMax { get; set; }

        public virtual Session Session { get; set; }
    }
}
