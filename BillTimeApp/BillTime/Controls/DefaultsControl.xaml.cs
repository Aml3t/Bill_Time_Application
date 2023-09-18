﻿using BillTimeLibrary.DataAccess;
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
    /// Interaction logic for DefaultsControl.xaml
    /// </summary>
    public partial class DefaultsControl : UserControl
    {
        public DefaultsControl()
        {
            InitializeComponent();
            LoadDefaultsFromDatabase();
            
        }

        private void LoadDefaultsFromDatabase()
        {
            string sql = "select * from Defaults";
            DefaultsModel model = SqliteDataAccess.LoadData<DefaultsModel>(sql, new Dictionary<string, object>()).FirstOrDefault();

            if (model != null)
            {
                hourlyRateTextBox.Text = model.HourlyRate.ToString();
                preBillCheckbox.IsChecked = (model.PreBill > 0); // Checking if the PreBill is true, meaning > 0. 
                hasCutoffCheckbox.IsChecked = (model.HasCutOff > 0);
                cutOffTextbox.Text = model.CutOff.ToString();
                minimumHoursTextbox.Text = model.MinimumHour.ToString();
                billingIncrementTextbox.Text = model.BillingIncrement.ToString();
                roundUpAfterXMinuteTextbox.Text = model.RoundUpAfterXMinutes.ToString();
            }
            else
            {
                hourlyRateTextBox.Text = "0";
                preBillCheckbox.IsChecked = true;
                hasCutoffCheckbox.IsChecked = false;
                cutOffTextbox.Text = "0";
                minimumHoursTextbox.Text = "0.25";
                billingIncrementTextbox.Text = "0.25";
                roundUpAfterXMinuteTextbox.Text = "0";
            }
        }
        private bool ValidateForm()
        {
            bool output = true;

            return output;
        }
        private void submitForm_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            SqliteDataAccess.SaveData(sql, new Dictionary<string, object>());
        }
    }
}
