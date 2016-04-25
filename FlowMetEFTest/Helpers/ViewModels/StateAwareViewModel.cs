using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMetEFTest.Helpers.ViewModels
{
    class StateAwareViewModel : PropertyChangedBase
    {
        private bool isNew = true;
        public bool IsNew
        {
            get { return isNew; }
            set
            {
                isNew = value;
                NotifyOfPropertyChange(() => IsNew);
            }
        }

        private bool isDeleted;
        public bool IsDeleted
        {
            get { return isDeleted; }
            set
            {
                isDeleted = value;
                NotifyOfPropertyChange(() => IsDeleted);
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }
    }
}
