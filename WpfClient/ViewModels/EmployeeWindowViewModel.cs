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
    public class EmployeeWindowViewModel: ObservableRecipient
    {
        private string errorMsg;
        public RestCollection<Employee> Employees { get; set; }
        public RestCollection<Manager> Managers { get; set; }
        public int SelectedManager; 
        public EmployeeWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Employees = new RestCollection<Employee>("http://localhost:51716/", "employee", "hub");
            }
            CreateEmployeeCommand = new RelayCommand(() =>
            {
                Employees.Add(new Employee()
                {
                    Name = selectedEmployee.Name,
                    ManagerId = SelectedEmployee.ManagerId,
                    CurrentTask = selectedEmployee.CurrentTask

                });
            });
            UpdateEmployeeCommand = new RelayCommand(() =>
            {
                try
                {
                    Employees.Update(selectedEmployee);
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                }
            });
            DeleteEmployeeCommand = new RelayCommand(() =>
            {
                Employees.Delete(selectedEmployee.Id);
            },
            () =>
            {
                return selectedEmployee != null;
            });
            selectedEmployee = new Employee();
        }
        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                if (value != null)
                {
                    selectedEmployee = new Employee()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        ManagerId = value.ManagerId
                    };
                    OnPropertyChanged();
                    (DeleteEmployeeCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
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
