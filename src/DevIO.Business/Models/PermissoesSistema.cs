using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class PermissoesSistema : Entity
    {
        public int id_sistema { get; set; }
        public string nome { get; set; }
        public string sigla { get; set; }
        public string codigo { get; set; }
    }
}
