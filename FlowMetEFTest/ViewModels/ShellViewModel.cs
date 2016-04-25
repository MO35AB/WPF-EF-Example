using Caliburn.Micro;
using FlowMetEFTest.EventMessages;
using FlowMetEFTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowMetEFTest.ViewModels
{
    class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<EventMessageNoLoad>,
        IHandle<EventMessageSingleLoad<Dictionary<string, int>>>
    {
        #region Field Definitions
        private FlowMetContext flowmetContext;
        private IEventAggregator eventAggregator;
        #endregion Field Definitions

        #region Property Definitions
        private PatientViewModel patientView;
        public PatientViewModel PatientView
        {
            get { return patientView; }
            set
            {
                patientView = value;
                NotifyOfPropertyChange(() => PatientView);
            }
        }
        #endregion Property Definitions


        public ShellViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.Subscribe(this);

            flowmetContext = new FlowMetContext();
            PatientView = new PatientViewModel(flowmetContext, this.eventAggregator);

            ActivateItem(PatientView);
        }

        public void SaveDBChanges()
        {
            var savedStatus = flowmetContext.SaveChanges();
        }

        public void ApplicationClosing()
        {
            flowmetContext.Dispose();
        }

        public void Handle(EventMessageNoLoad message)
        {
            switch (message.Identifier)
            {
                case "ActivatePatientsView":
                    ActivateItem(PatientView);
                    break;
            }
        }

        public void Handle(EventMessageSingleLoad<Dictionary<string, int>> message)
        {
            switch (message.Identifier)
            {
                case "ActivateStudies":
                    ActivateItem(new StudyViewModel(flowmetContext, eventAggregator, message.Value));
                    break;
            }
        }
    }
}
