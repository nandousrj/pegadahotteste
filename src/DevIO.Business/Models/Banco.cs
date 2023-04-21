using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Banco : Entity
    {
        public int id_banco { get; set; }
        public string desc_banco { get; set; }
        public string sigla { get; set; }
        public string cod_banco { get; set; }
        public string site { get; set; }
        public string observacao { get; set; }
    }
}
