using FlowMetEFTest.Helpers.ViewModels;
using FlowMetEFTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMetEFTest.ViewModels.EntityViewModels
{
    class PatientEntityViewModel : StateAwareViewModel
    {
        public Patient Patient { get; set; }

        public PatientEntityViewModel()
        {
            Patient = new Patient();
            Patient.Identifier = DateTime.Now.ToString("G");
        }
    }
}
