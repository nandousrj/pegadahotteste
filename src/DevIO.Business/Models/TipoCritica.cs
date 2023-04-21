using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class TipoCritica : Entity
    {
        public int id_tipo_critica { get; set; }
        public string desc_tipo_critica { get; set; }
        public string imagem { get; set; }
        public int ordem { get; set; }
        public bool status { get; set; }
        public string qtd_tipo2 { get; set; }
    }
}
