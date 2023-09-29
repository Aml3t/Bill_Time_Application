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

        bool isTimerRunning = false;
        DateTime startTime;


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
            InsertNewWorkItem();

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

        private (bool isValid, WorkModel model) ValidateForm()
        {
            bool isValid = true;
            WorkModel model = new WorkModel();

            try
            {
                model.Hours = double.Parse(hoursTextbox.Text);
                model.Title = titleTextbox.Text;
                model.Description = descriptionTextbox.Text;
                model.ClientId = (int)clientDropDown.SelectedValue;
            }
            catch
            {
                isValid = false;
            }

            return (isValid, model);
        }

        private void InsertNewWorkItem()
        {
            string sql = "INSERT INTO Work (ClientId, Hours, Title, Description) "
            + "VALUES (@ClientId, @Hours, @Title, @Description)";

            var form = ValidateForm();

            if (form.isValid == false)
            {
                MessageBox.Show("Invalid work form. Please check data and try again.");
                return;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@ClientId", form.model.ClientId },
                {"@Hours", form.model.Hours },
                {"@Title", form.model.Title },
                { "@Description", form.model.Description },
            };

            SqliteDataAccess.SaveData(sql, parameters);

            MessageBox.Show("Success");

        }

        private void operateTime_Click(object sender, RoutedEventArgs e)
        {

            if (clientDropDown.SelectedItem != null)
            {
                if (isTimerRunning == true)
                {
                    //TimeSpan result = DateTime.Now - startTime;

                    double minutes = DateTime.Now.Subtract(startTime).TotalMinutes;

                    CalculateHours(minutes);

                    isTimerRunning = false;
                    operateTime.Content = "Start Timer";
                    operateTime.Background = Brushes.Green;
                    //MessageBox.Show(result.TotalMinutes.ToString());
                }
                else
                {
                    startTime = DateTime.Now;
                    isTimerRunning = true;
                    operateTime.Content = "Stop Timer";
                    operateTime.Background = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("Please select a client first.");
            }

        }

        private void CalculateHours(double minutes)
        {
            throw new NotImplementedException();
        }
    }
}
