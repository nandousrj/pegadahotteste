using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class PermissoesSetor : Entity
    {
        public int id_setor { get; set; }
        public string nome { get; set; }
        public string andar { get; set; }
        public string sala { get; set; }
        public PermissoesInstituicao Instituicao { get; set; }
        public PermissoesInstituicao PermissoesInstituicao { get; set; }
    }
}
