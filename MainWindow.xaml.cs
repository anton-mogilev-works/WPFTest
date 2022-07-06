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
using System.ServiceProcess;
using System.Collections.ObjectModel;

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ServiceController> Services;
        public MainWindow()
        {            
            InitializeComponent();            
            Services = new ObservableCollection<ServiceController>(ServiceController.GetServices());
            ServicesGrid.DataContext = Services;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (Services.Count > 0)
            //{
            //    foreach (ServiceController service in Services)
            //    {
            //        TextBlock.Text += service.Status + "\r\n";
            //    }
            //}
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = ServicesGrid.Items.IndexOf(ServicesGrid.CurrentItem);
                
                if (Services[index].Status == ServiceControllerStatus.Running)
                {
                    MessageBox.Show("Service " + Services[index].DisplayName + " already running");
                }
                else
                {
                    try
                    {
                        Services[index].Start();
                        Services[index].WaitForStatus(ServiceControllerStatus.Running);
                    }
                    catch(Exception startException)
                    {
                        if(startException.GetType() == typeof(InvalidOperationException))
                        {
                            MessageBox.Show(startException.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Unexpected error");
                        }
                        
                    }
                    
                }
                //MessageBox.Show(Services[index].Status.ToString());
            }
            catch(Exception ex)
            {
                if(ex.GetType() == typeof(ArgumentOutOfRangeException))
                {
                    MessageBox.Show("Service not selected");
                }
            }
            
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

            int index = ServicesGrid.Items.IndexOf(ServicesGrid.CurrentItem);

        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
