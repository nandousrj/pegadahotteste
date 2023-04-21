using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class TipoPagamento : Entity
    {
        public int id_tipo_pagamento { get; set; }
        public string desc_tipo_pagamento { get; set; }
        public bool status { get; set; }

    }
}
