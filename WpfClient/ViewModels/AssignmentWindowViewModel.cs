using GVC31G_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using NotebookDbApp.WpfClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient.ViewModels
{
    public class AssignmentWindowViewModel: ObservableRecipient
    {
        public RestCollection<Assignment> Assignments { get; set; }
        public AssignmentWindowViewModel()
        {
            ;
            if (!IsInDesignMode)
            {
                Assignments = new RestCollection<Assignment>("http://localhost:51716/", "assignment");
            }
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
                        EmployeeId = value.EmployeeId
                    };
                    OnPropertyChanged();
                    //(DeleteActorCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
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
