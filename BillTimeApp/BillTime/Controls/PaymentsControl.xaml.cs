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

        ObservableCollection<PaymentModel> payments = new ObservableCollection<PaymentModel>();
        

        public PaymentsControl()
        {
            InitializeComponent();

            InitializeClientList();

            WireUpClientDropDown();

            InitializePaymentsList();
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

            var paymentList = SqliteDataAccess.LoadData<PaymentModel>(sql, new Dictionary<string, object>());

            paymentList.ForEach(x => payments.Add(x));
        }



        private void SearchPaymentDateOfClient()
        {

        }

        private void SelectPaymentDate()
        {

        }


    }
}
