using BillTimeLibrary.DataAccess;
using BillTimeLibrary.Models;
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

namespace BillTime.Controls
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControl
    {
        List<ClientModel> clients;

        public ClientControl()
        {
            InitializeComponent();

            InitializeClientList();

            WireUpClientDropDown();
        }

        private void WireUpClientDropDown()
        {
            clientDropDown.ItemsSource = clients;
            clientDropDown.DisplayMemberPath = "Name";
            clientDropDown.SelectedValuePath = "Id";
        }

        private void InitializeClientList()
        {
            string sql = "select * from Client";

            clients = SqliteDataAccess.LoadData<ClientModel>(sql, new Dictionary<string, object>());
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            //// Could be done like this
            //clientStackPanel.IsEnabled = false;
            //editButton.IsEnabled = false;
            clientStackPanel.Visibility = Visibility.Collapsed;
            editButton.Visibility = Visibility.Collapsed;
            LoadDefaults();

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //// Could be done like this
            //clientStackPanel.IsEnabled = false;
            //newButton.IsEnabled = false;
            clientStackPanel.Visibility = Visibility.Collapsed;
            newButton.Visibility = Visibility.Collapsed;

        }

        private void LoadDefaults()
        {
            string sql = "select * from Defaults";

            DefaultsModel model = SqliteDataAccess.LoadData<DefaultsModel>(sql, new Dictionary<string, object>()).FirstOrDefault();

            if (model != null)
            {
                hourlyRateTextbox.Text = model.HourlyRate.ToString();
                preBillCheckbox.IsChecked = (model.PreBill > 0);
                hasCutOffCheckbox.IsChecked = (model.HasCutOff > 0);
                cutOffTextbox.Text = model.CutOff.ToString();
                minimumHoursTextbox.Text = "0.25";
                billingIncrementTextbox.Text = model.BillingIncrement.ToString();
                roundUpAfterXMinuteTextbox.Text = model.RoundUpAfterXMinutes.ToString();
            }
            
        }

    }
}
