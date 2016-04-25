using FlowMetEFTest.Helpers.ViewModels;
using FlowMetEFTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMetEFTest.ViewModels.EntityViewModels
{
    class StudyEntityViewModel : StateAwareViewModel
    {
        public Study Study { get; set; }

        public StudyEntityViewModel()
        {
            Study = new Study();
            Study.StartDate = DateTime.Now;
            Study.EndDate = DateTime.Now;
        }
    }
}
