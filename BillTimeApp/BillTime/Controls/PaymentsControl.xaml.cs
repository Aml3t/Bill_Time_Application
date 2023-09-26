using BillTimeLibrary.DataAccess;
using BillTimeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PaymentsControl.xaml
    /// </summary>
    public partial class PaymentsControl : UserControl
    {
        ObservableCollection<ClientModel> clients = new ObservableCollection<ClientModel>();

        bool isNewEntry = true;

        ObservableCollection<PaymentsModel> payments = new ObservableCollection<PaymentsModel>();
        

        public PaymentsControl()
        {
            InitializeComponent();

            InitializeClientList();

            WireUpClientDropDown();

            ToggleFormFieldsDisplay(false);

            selectionStackPanel.Visibility = Visibility.Collapsed;


            //InitializePaymentsList();
        }

        private void WireUpClientDropDown()
        {
            clientDropDown.ItemsSource = clients;
            clientDropDown.DisplayMemberPath = "Name";
            clientDropDown.SelectedValuePath = "Id";
        }

        private void InitializeClientList()
        {
            string sql = "select * from Client order by Name";

            var clientList = SqliteDataAccess.LoadData<ClientModel>(sql, new Dictionary<string, object>());

            clientList.ForEach(x => clients.Add(x));
        }

        private void InitializePaymentsList()
        {
            int clientId = ((ClientModel)(clientDropDown.SelectedItem)).Id;

            string sql = "select * from Payment Where ClientId = @clientId";

            var paymentList = SqliteDataAccess.LoadData<PaymentsModel>(sql, new Dictionary<string, object>());

            paymentList.ForEach(x => payments.Add(x));
        }

        private void ToggleFormFieldsDisplay(bool displayFields)
        {
            Visibility display = displayFields ? Visibility.Visible : Visibility.Collapsed;

            amountStackPanel.Visibility = display;
            hoursStackPanel.Visibility = display;
            buttonStackPanel.Visibility = display;
        }
        private void SearchPaymentDateOfClient()
        {

        }

        private void SelectPaymentDate()
        {

        }


        private void clientDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionStackPanel.Visibility = Visibility.Visible;
        }

        private void newButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleFormFieldsDisplay(true);
            dateStackPanel.Visibility = Visibility.Collapsed;
            orTextBlock.Visibility = Visibility.Collapsed; 
        }

        private void submitForm_Click(object sender, RoutedEventArgs e)
        {
            if (isNewEntry == true)
            {
                InsertNewPayment();
            }
            else
            {
                UpdatePaymentRecord();
            }

            ResetForm();
        }

        private (bool isValid, PaymentsModel model) ValidateForm()
        {
            bool isValid = true;
            PaymentsModel model = new PaymentsModel();

            try
            {
                model.Amount = double.Parse(amountTextBox.Text);
                model.Hours = double.Parse(hoursTextBox.Text);
                model.Date = DateTime.Parse(dateDropDown.Text);

                //model.Name = nameTextbox.Text;
                //model.Email = emailTextbox.Text;
                //model.PreBill = (bool)preBillCheckbox.IsChecked ? 1 : 0;
                //model.HasCutOff = (bool)hasCutOffCheckbox.IsChecked ? 1 : 0;
                //model.HourlyRate = double.Parse(hourlyRateTextbox.Text);
                //model.CutOff = int.Parse(cutOffTextbox.Text);
                //model.MinimumHours = double.Parse(minimumHoursTextbox.Text);
                //model.BillingIncrement = double.Parse(billingIncrementTextbox.Text);
                //model.RoundUpAfterXMinutes = int.Parse(roundUpAfterXMinuteTextbox.Text);
            }
            catch
            {
                isValid = false;
            }

            return (isValid, model);
        }

        private void InsertNewPayment()
        {
            throw new NotImplementedException();
        }

        private void UpdatePaymentRecord()
        {
            throw new NotImplementedException();
        }

        private void ResetForm()
        {
            dateStackPanel.Visibility = Visibility.Visible;
            orTextBlock.Visibility = Visibility.Visible;
            newButton.Visibility = Visibility.Visible;

            isNewEntry = true;

            ClearFormData();

            ToggleFormFieldsDisplay(false);
        }

        private void ClearFormData()
        {
            amountTextBox.Text = "";
            hoursTextBox.Text = "";
        }


        private void clearForm_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
