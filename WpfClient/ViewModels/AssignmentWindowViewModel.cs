using GVC31G_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NotebookDbApp.WpfClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfClient.ViewModels
{
    public class AssignmentWindowViewModel: ObservableRecipient
    {
        private string errorMsg;
        public RestCollection<Assignment> Assignments { get; set; }
        public AssignmentWindowViewModel()
        {
            ;
            if (!IsInDesignMode)
            {
                Assignments = new RestCollection<Assignment>("http://localhost:51716/", "assignment");
            }
            CreateAssignmentCommand = new RelayCommand(() =>
            {
                Assignments.Add(new Assignment()
                {
                    Description = selectedAssignment.Description,
                    dueDate = selectedAssignment.dueDate,
                    EmployeeId = selectedAssignment.EmployeeId

                });
            });
            UpdateAssignmentCommand = new RelayCommand(() =>
            {
                try
                {
                    Assignments.Update(selectedAssignment);
                }catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            });
            DeleteAssignmentCommand = new RelayCommand(() =>
            {
                Assignments.Delete(selectedAssignment.Id);
            },
            () =>
            {
                return selectedAssignment != null;
            });
            selectedAssignment = new Assignment();
        }
        private Assignment selectedAssignment;
        public Assignment SelectedAssignment
        {
            get { return selectedAssignment; }
            set
            {
                if (value != null)
                {
                    selectedAssignment = new Assignment()
                    {
                        Id = value.Id,
                        Description = value.Description,
                        dueDate = value.dueDate,
                        Employee = value.Employee,
                        EmployeeId = value.EmployeeId
                    };
                    OnPropertyChanged();
                    (DeleteAssignmentCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateAssignmentCommand { get; set; }
        public ICommand DeleteAssignmentCommand { get; set; }
        public ICommand UpdateAssignmentCommand { get; set; }
        public ICommand ClearFieldsCommand { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
