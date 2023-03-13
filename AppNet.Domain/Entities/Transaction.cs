using AppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Domain.Entities
{
    public class Transaction:BaseEntity
    {
        public override string ToString()
        {
            return "Toplam: " + this.Total + " - " + this.Comment + " İşlem Tarihi:" + this.TransactionDate;
        }
        public decimal Total { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comment { get; set; }
    }
}
