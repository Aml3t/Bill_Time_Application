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
    /// Interaction logic for WorkControl.xaml
    /// </summary>
    public partial class WorkControl : UserControl
    {
        ObservableCollection<ClientModel> clients = new ObservableCollection<ClientModel>();

        ObservableCollection<WorkModel> work = new ObservableCollection<WorkModel>();

        ObservableCollection<PaymentsModel> payments = new ObservableCollection<PaymentsModel>();

        public WorkControl()
        {
            InitializeComponent();

            WireDropDowns();

            InitializeClientList();

            ToggleFormFieldsDisplay(false);
        }

        private void WireDropDowns()
        {
            clientDropDown.ItemsSource = clients;
            clientDropDown.DisplayMemberPath = "Name";
            clientDropDown.SelectedValuePath = "Id";

            dateDropDown.ItemsSource = work;
            dateDropDown.DisplayMemberPath = "DateEntered";
            dateDropDown.SelectedValuePath = "Id";

            paymentDropDown.ItemsSource = payments;
            paymentDropDown.DisplayMemberPath = "Date";
            paymentDropDown.SelectedValuePath = "Id";
        }
        private void InitializeClientList()
        {
            string sql = "select * from Client order by Name";

            var clientList = SqliteDataAccess.LoadData<ClientModel>(sql, new Dictionary<string, object>());

            clientList.ForEach(x => clients.Add(x));
        }

        private void LoadDateDropDown()
        {
            string sql = "select * from Work Where ClientId = @ClientId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ClientId", clientDropDown.SelectedValue }
            };

            var records = SqliteDataAccess.LoadData<WorkModel>(sql, parameters);

            work.Clear();
            records.ForEach(x => work.Add(x));

        }

        private void ToggleFormFieldsDisplay(bool displayFields)
        {
            Visibility display = displayFields ? Visibility.Visible : Visibility.Collapsed;

            dateStackPanel.Visibility = display;
            hoursStackPanel.Visibility = display;
            titleStackPanel.Visibility = display;
            descriptionStackPanel.Visibility = display;
            paidStackPanel.Visibility = display;
            submitForm.Visibility = display;
        }

        private void clientDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dateStackPanel.Visibility = Visibility.Visible;
            
            LoadDateDropDown();

            LoadPaymentDropDown();
        }

        private void dateDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ToggleFormFieldsDisplay(true);

            WorkModel workEntry = (WorkModel)dateDropDown.SelectedItem;

            hoursTextBox.Text = workEntry.Hours.ToString();
            titleTextbox.Text = workEntry.Title;
            descriptionTextbox.Text = workEntry.Description;
            paidCheckbox.IsChecked = workEntry.Paid > 0;
        }

        private void LoadPaymentDropDown()
        {
            string sql = "select * from Payment Where ClientId = @ClientId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ClientId", clientDropDown.SelectedValue }
            };

            var records = SqliteDataAccess.LoadData<PaymentsModel>(sql, parameters);

            payments.Clear();
            records.ForEach(x => payments.Add(x));
        }
    }
}
