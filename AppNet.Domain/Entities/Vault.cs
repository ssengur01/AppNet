using AppNET.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.Domain.Entities
{
    public class Vault:BaseEntity
    {
        public Transaction? Gelir { get; set; }

        public Transaction? Gider { get; set; }
    }
}
