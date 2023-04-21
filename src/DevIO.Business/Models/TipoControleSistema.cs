using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class TipoControleSistema : Entity
    {
        public int id_tipo_controle_sistema { get; set; }
        public string desc_tipo_controle { get; set; }    
    }
}
