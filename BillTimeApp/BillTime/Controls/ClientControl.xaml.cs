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

        bool isNewEntry = true;

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

            isNewEntry = true;

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
                nameTextbox.Text = "";
                emailTextbox.Text = "";
                hourlyRateTextbox.Text = model.HourlyRate.ToString();
                preBillCheckbox.IsChecked = (model.PreBill > 0);
                hasCutOffCheckbox.IsChecked = (model.HasCutOff > 0);
                cutOffTextbox.Text = model.CutOff.ToString();
                minimumHoursTextbox.Text = "0.25";
                billingIncrementTextbox.Text = model.BillingIncrement.ToString();
                roundUpAfterXMinuteTextbox.Text = model.RoundUpAfterXMinutes.ToString();
            }
            else
            {
                nameTextbox.Text = "";
                emailTextbox.Text = "";
                hourlyRateTextbox.Text = "0";
                preBillCheckbox.IsChecked = true;
                hasCutOffCheckbox.IsChecked = false;
                cutOffTextbox.Text = "0";
                minimumHoursTextbox.Text = "0.25";
                billingIncrementTextbox.Text = "0.25";
                roundUpAfterXMinuteTextbox.Text = "0";
            }
        }

        private void submitForm_Click(object sender, RoutedEventArgs e)
        {
            if (isNewEntry == true)
            {
                InsertNewClient();
            }
            else
            {
                UpdateClientRecord();
            }

            ResetForm();
        }

        private void InsertNewClient()
        {
            string sql = "insert into Client (Name, HourlyRate, Email," +
                " PreBill, HasCutOff, CutOff, MinimumHours," +
                " BillingIncrement, RoundUpAfterXMinutes"
                + "values (@Name, @HourlyRate, @Email, @PreBill," +
                " @HasCutOff, @CutOff, @MinimumHours, @BillingIncrement," +
                " @RoundUpAfterXMinutes)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@Name", nameTextbox.Text },
                { },
            };

            SqliteDataAccess.SaveData(sql, parameters);
        }
        private void UpdateClientRecord()
        {

        }
        private void ResetForm()
        {
            clientStackPanel.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Visible;
            newButton.Visibility = Visibility.Visible;

            isNewEntry = true;
        }
    }
}
