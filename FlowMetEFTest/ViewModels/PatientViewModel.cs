using Caliburn.Micro;
using FlowMetEFTest.Models;
using FlowMetEFTest.ViewModels.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowMetEFTest.Helpers;
using Microsoft.Data.Entity;
using FlowMetEFTest.EventMessages;

namespace FlowMetEFTest.ViewModels
{
    class PatientViewModel : Screen
    {

        #region Field Definitions
        private FlowMetContext flowmetContext;
        private List<PatientEntityViewModel> selectedItems;
        private List<PatientEntityViewModel> patientsToRemove;
        private IEventAggregator eventAggregator;
        #endregion

        #region Property Definitions
        public ObservableCollection<PatientEntityViewModel> AllPatients { get; set; }

        private PatientEntityViewModel selectedPatient;
        public PatientEntityViewModel SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                NotifyOfPropertyChange(() => SelectedPatient);
            }
        }
        #endregion Property Definitions

        public PatientViewModel(FlowMetContext flowmetContext, IEventAggregator eventAggregator)
        {
            this.flowmetContext = flowmetContext;
            this.eventAggregator = eventAggregator;

            AllPatients = new ObservableCollection<PatientEntityViewModel>();
            selectedItems = new List<PatientEntityViewModel>();
            patientsToRemove = new List<PatientEntityViewModel>();

            LoadListOfPatients();
        }


        public void LoadListOfPatients()
        {
            InitializePatientEntityViewModels();

            if (AllPatients.Count > 0)
            {
                SelectedPatient = AllPatients[0];
            }
        }

        public async void InitializePatientEntityViewModels()
        {
            var allPatientEntities = await (from patients in flowmetContext.Patient.Include(patient => patient.Study)
                                            select patients).ToListAsync();

            AllPatients.Clear();
            foreach (var entity in allPatientEntities)
            {
                AllPatients.Add(new PatientEntityViewModel { Patient = entity, IsNew = false });
            }
        }

        public void PatientShowSelectedStudies()
        {
            var selectedIds = (from patient in AllPatients
                               where patient.IsSelected
                               select new { ID = patient.Patient.PatientID, Identifier = patient.Patient.Identifier }).ToList();

            Dictionary<string, int> finalCombos = new Dictionary<string, int>();
            foreach (var combo in selectedIds)
            {
                finalCombos.Add(combo.Identifier, combo.ID);
            }

            eventAggregator.Publish(new EventMessageSingleLoad<Dictionary<string, int>>("ActivateStudies", finalCombos), action =>
            {
                Task.Factory.StartNew(action);
            });
        }

        public void PatientRemoveSelected()
        {
            patientsToRemove = AllPatients.Where(patient => patient.IsSelected).ToList();

            if (patientsToRemove.Count > 0)
            {
                foreach (var patient in patientsToRemove)
                {
                    AllPatients.Remove(patient);
                    patient.IsSelected = false;
                    patient.IsDeleted = true;
                }
            }
        }

        public void AbandonPatientUpdates()
        {
            // Abandon all changes
            flowmetContext = new FlowMetContext();
            InitializePatientEntityViewModels();

            patientsToRemove.Clear();
        }

        public void PatientConfirmUpdates()
        {
            if (true)
            {
                SavePatientUpdates();
            }
            else
            {
                AbandonPatientUpdates();
            }
        }

        public void SavePatientUpdates()
        {
            // Handle Updates and Inserts
            foreach (var patient in AllPatients)
            {
                if (patient.IsNew)
                {
                    patient.IsNew = false;
                    flowmetContext.Patient.Add(patient.Patient);
                }
                else
                {
                    flowmetContext.Patient.Update(patient.Patient);
                }
            }

            // Handle Removals
            foreach (var patientToRemove in patientsToRemove)
            {
                flowmetContext.Patient.Remove(patientToRemove.Patient);
            }

            // Empty the list of patients to remove
            patientsToRemove.Clear();

            // Save all changes
            flowmetContext.SaveChanges();

        }

    }
}
