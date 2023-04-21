using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Pagamento
    {
        public int id_pagamento { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public Garota Garota { get; set; }
        public decimal valor_aluguel { get; set; }
        public decimal valor_destaque { get; set; }
        public int desconto { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public string valor_aluguel2 { get; set; }
        public string valor_destaque2 { get; set; }
        public string desconto2 { get; set; }
        public string total2 { get; set; }
    }
}
