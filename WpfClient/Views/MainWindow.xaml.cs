using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClient.Views;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_assignment_Click(object sender, RoutedEventArgs e)
        {
            AssignmentWindow aWindow = new AssignmentWindow();
            aWindow.Show();
        }

        private void btn_employee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeWindow eWindow = new EmployeeWindow();
            eWindow.Show();

        }

        private void btn_manager_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow mWindow = new ManagerWindow();
            mWindow.Show();
        }
    }
}
