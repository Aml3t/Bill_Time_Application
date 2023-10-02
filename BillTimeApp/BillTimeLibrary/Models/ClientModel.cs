using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillTimeLibrary.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double HourlyRate { get; set; }
        public int PreBill { get; set; }
        public int HasCutOff { get; set; }
        public int CutOff { get; set; }
        public double MinimumHours { get; set; }

        // Charges the client every x minutes
        public double BillingIncrement { get; set; }
        public int RoundUpAfterXMinutes { get; set; }


    }
}
