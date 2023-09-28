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
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        ObservableCollection<ClientModel> clients = new ObservableCollection<ClientModel>();
        public MainControl()
        {
            InitializeComponent();

            InitializeClientList();

            WireClientDropDown();


        }

        private void WireClientDropDown()
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

        private void clientDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void submitForm_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            ClearFormData();
        }

        private void ClearFormData()
        {
            hoursTextbox.Text = "";
            titleTextbox.Text = "";
            descriptionTextbox.Text = "";
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
    }
}
