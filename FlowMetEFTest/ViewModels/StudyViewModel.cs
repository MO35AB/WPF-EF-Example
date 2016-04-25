using Caliburn.Micro;
using FlowMetEFTest.EventMessages;
using FlowMetEFTest.Models;
using FlowMetEFTest.ViewModels.EntityViewModels;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlowMetEFTest.ViewModels
{
    class StudyViewModel : Screen
    {

        #region Field Defintions
        private FlowMetContext flowmetContext;
        IEventAggregator eventAggregator;
        private Dictionary<string, int> selectedPatients;
        #endregion

        #region Property Definitions
        public ObservableCollection<StudyEntityViewModel> AllStudies { get; set; }

        private List<string> patientIdentifiers;
        public List<string> PatientIdentifiers
        {
            get { return patientIdentifiers; }
            set
            {
                patientIdentifiers = value;
                NotifyOfPropertyChange(() => PatientIdentifiers);
            }
        }

        private string selectedPatient;
        public string SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                NotifyOfPropertyChange(() => SelectedPatient);
            }
        }
        #endregion

        public StudyViewModel(FlowMetContext flowmetContext, IEventAggregator eventAggregator)
        {
            this.flowmetContext = flowmetContext;
            this.eventAggregator = eventAggregator;
            PatientIdentifiers = new List<string>();
        }

        public StudyViewModel(FlowMetContext flowmetContext, IEventAggregator eventAggregator,
            Dictionary<string, int> selectedPatients)
        {
            this.flowmetContext = flowmetContext;
            this.eventAggregator = eventAggregator;
            this.selectedPatients = selectedPatients;

            PatientIdentifiers = this.selectedPatients.Keys.ToList();
            SelectedPatient = PatientIdentifiers[0];

            AllStudies = new ObservableCollection<StudyEntityViewModel>();
            InitializeStudyEntityViewModels(this.selectedPatients[SelectedPatient]);
        }

        public async void InitializeStudyEntityViewModels(int patientId)
        {
            var allStudyEntities = await (from study in flowmetContext.Study
                                          where study.PatientID == patientId
                                          select study).ToListAsync();
            Application.Current.Dispatcher.Invoke(() =>
            {
                AllStudies.Clear();
                foreach (var entity in allStudyEntities)
                {
                    AllStudies.Add(new StudyEntityViewModel { Study = entity, IsNew = false });
                }
            });
        }

        public void PatientSelectionChanged()
        {
            InitializeStudyEntityViewModels(selectedPatients[SelectedPatient]);
        }

        public void ReturnToPatientsView()
        {
            eventAggregator.Publish(new EventMessageNoLoad("ActivatePatientsView"), action =>
            {
                Task.Factory.StartNew(action);
            });
        }
    }
}
