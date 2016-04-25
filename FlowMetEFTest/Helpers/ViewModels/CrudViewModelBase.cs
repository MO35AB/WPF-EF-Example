using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMetEFTest.Helpers.ViewModels
{
    class CrudViewModelBase<T> : PropertyChangedBase
    {
        protected IList<T> tableData;

        protected virtual void ReadAllData()
        {
            
        }
    }
}
