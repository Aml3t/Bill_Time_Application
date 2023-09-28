using BillTime.Controls;
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

namespace BillTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            content.Content = new MainControl();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clientsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new ClientControl();
        }

        private void paymentMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new PaymentsControl();
        }

        private void workMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new WorkControl();
        }

        private void defaultsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new DefaultsControl();
        }

        private void aboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new AboutControl();
        }

        private void mainMenuItem_Click(object sender, RoutedEventArgs e)
        {
            content.Content = new MainControl();

        }
    }
}
