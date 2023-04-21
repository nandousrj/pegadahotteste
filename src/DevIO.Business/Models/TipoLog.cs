using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models 
{
    public class TipoLog : Entity
    {
        public int id_tipo_log { get; set; }
        public string descricao { get; set; }
       // public bool ind_interno { get; set; }
    }
}
