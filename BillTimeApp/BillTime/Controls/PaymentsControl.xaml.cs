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

        ObservableCollection<PaymentsModel> payments = new ObservableCollection<PaymentsModel>();

        bool isNewEntry = true;

        public PaymentsControl()
        {
            InitializeComponent();

            InitializeClientList();

            WireUpDropDowns();

            ToggleFormFieldsDisplay(false);

            selectionStackPanel.Visibility = Visibility.Collapsed;

        }

        private void WireUpDropDowns()
        {
            clientDropDown.ItemsSource = clients;
            clientDropDown.DisplayMemberPath = "Name";
            clientDropDown.SelectedValuePath = "Id";

            dateDropDown.ItemsSource = payments;
            dateDropDown.DisplayMemberPath = "DisplayValue";
            dateDropDown.SelectedValuePath = "Id";

        }

        private void InitializeClientList()
        {
            string sql = "select * from Client order by Name";

            var clientList = SqliteDataAccess.LoadData<ClientModel>(sql, new Dictionary<string, object>());

            clientList.ForEach(x => clients.Add(x));
        }


        private void LoadDateDropDown()
        {
            //int clientId = ((ClientModel)(clientDropDown.SelectedItem)).Id;

            string sql = "select * from Payment Where ClientId = @ClientId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ClientId", clientDropDown.SelectedValue }
            };

            var records = SqliteDataAccess.LoadData<PaymentsModel>(sql, parameters);

            payments.Clear();
            records.ForEach(x => payments.Add(x));
        }

        private void ToggleFormFieldsDisplay(bool displayFields)
        {
            Visibility display = displayFields ? Visibility.Visible : Visibility.Collapsed;

            amountStackPanel.Visibility = display;
            hoursStackPanel.Visibility = display;
            buttonStackPanel.Visibility = display;
        }

        private void clientDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionStackPanel.Visibility = Visibility.Visible;

            LoadDateDropDown();
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
                //model.Date = DateTime.Parse(dateDropDown.Text);
            }
            catch
            {
                isValid = false;
            }

            return (isValid, model);
        }

        private void InsertNewPayment()
        {
            string sql = "INSERT INTO Payment (ClientId, Hours, Amount) "
            + "VALUES (@ClientId, @Hours, @Amount)";

            var form = ValidateForm();

            if (form.isValid == false)
            {
                MessageBox.Show("Invalid form. Please check data and try again.");
                return;
            }

            form.model.ClientId = (int)clientDropDown.SelectedValue;

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ClientId", form.model.ClientId },
                {"@Hours", form.model.Hours },
                {"@Amount", form.model.Amount }
            };

            SqliteDataAccess.SaveData(sql, parameters);

            payments.Add(form.model);

            MessageBox.Show("Success");

        }

        private void UpdatePaymentRecord()
        {
            string sql = "UPDATE Payment set Hours = @Hours, Amount = @Amount where Id = @Id";

            var form = ValidateForm();

            if (form.isValid == false)
            {
                MessageBox.Show("Invalid input. Please check again.");
                return;
            }

            Dictionary<string, object> parameter = new Dictionary<string, object>
            {
                { "@Id", dateDropDown.SelectedValue },
                { "@Hours", form.model.Hours },
                { "@Amount", form.model.Amount }
            };

            SqliteDataAccess.SaveData(sql, parameter);

            PaymentsModel currentPayment = (PaymentsModel)(dateDropDown.SelectedItem);

            currentPayment.Amount = form.model.Amount;
            currentPayment.Hours = form.model.Hours;

            MessageBox.Show("Client successfully updated");

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
            ResetForm();
        }

        private void dateDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            orTextBlock.Visibility = Visibility.Collapsed;
            newButton.Visibility = Visibility.Collapsed;

            PaymentsModel payment = (PaymentsModel)dateDropDown.SelectedItem;

            amountTextBox.Text = payment.Amount.ToString();
            hoursTextBox.Text = payment.Hours.ToString();

            isNewEntry = false;

            ToggleFormFieldsDisplay(true);

        }
    }
}
