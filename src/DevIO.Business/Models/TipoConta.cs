using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class TipoConta : Entity
    {
        public int id_tipo_conta { get; set; }
        public string desc_tipo_conta { get; set; }
        public string sigla { get; set; }
    }
}
