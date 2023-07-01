using AppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Domain.Entities
{
    public class Case:BaseEntity
    {
        public decimal Price { get; set; }

        public DateTime ProcessDate { get; set; } = DateTime.Now;

        public string Explanation { get; set; }

    }
}
