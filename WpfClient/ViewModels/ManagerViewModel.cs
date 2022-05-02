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
    internal class ManagerViewModel :ObservableRecipient
    {
        public RestCollection<Manager> Managers { get; set; }
        public ManagerViewModel()
        {
            if (!IsInDesignMode)
            {
                Managers = new RestCollection<Manager>("http://localhost:51716/", "manager");
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
