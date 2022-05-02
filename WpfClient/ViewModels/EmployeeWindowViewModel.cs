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
    public class EmployeeWindowViewModel: ObservableRecipient
    {
        public RestCollection<Employee> Employees { get; set; }
        public EmployeeWindowViewModel()
        {
            ;
            if (!IsInDesignMode)
            {
                ;
                Employees = new RestCollection<Employee>("http://localhost:51716/", "employee");
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
