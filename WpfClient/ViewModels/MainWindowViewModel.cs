using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.ViewModels
{
    public class MainWindowViewModel: ObservableRecipient
    {
        private ObservableRecipient selectedViewModel;

        public ObservableRecipient SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                if (value != null)
                {
                    selectedViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
