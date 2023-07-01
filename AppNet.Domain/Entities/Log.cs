using AppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Domain.Entities
{
    public class Log:BaseEntity
    {
        public Log()
        {

            this.LogTime = DateTime.Now;
        }
        public string LogMessage { get; set; }
        public DateTime LogTime { get; set; }
    }
}
