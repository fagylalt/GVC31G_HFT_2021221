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
    internal class ManagerViewModel :ObservableRecipient
    {
        private string errorMsg;
        public RestCollection<Manager> Managers { get; set; }
        public ManagerViewModel()
        {
            if (!IsInDesignMode)
            {
                Managers = new RestCollection<Manager>("http://localhost:51716/", "manager", "hub");
                CreateManagerCommand = new RelayCommand(() =>
                {
                    Managers.Add(new Manager()
                    {
                        Name = selectedManager.Name,
                        DepartmentName = selectedManager.DepartmentName,
                        Employees = new List<Employee>()

                    });
                });
                UpdateManagerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Managers.Update(selectedManager);
                    }
                    catch (Exception ex)
                    {
                        errorMsg = ex.Message;
                    }
                });
                DeleteManagerCommand = new RelayCommand(() =>
                {
                    Managers.Delete(selectedManager.Id);
                },
                () =>
                {
                    return selectedManager != null;
                });
                selectedManager = new Manager();
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
        private Manager selectedManager;
        public Manager SelectedManager
        {
            get { return selectedManager; }
            set
            {
                if (value != null)
                {
                    selectedManager = new Manager()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        DepartmentName = value.DepartmentName,
                        Employees = value.Employees
                    };
                    OnPropertyChanged();
                    (DeleteManagerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public ICommand CreateManagerCommand { get; set; }
        public ICommand UpdateManagerCommand { get; set; }

        public ICommand DeleteManagerCommand { get; set; }
    }
}
