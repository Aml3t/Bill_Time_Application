using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillTimeLibrary.Models
{
    public class WorkModel
    {
        public int ClientId { get; set; }
        public double Hours { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public bool Paid { get; set; }
        public int PaymentId { get; set; }
    }
}
