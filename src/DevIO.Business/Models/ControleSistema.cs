using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class ControleSistema : Entity
    {
        public int id_controle_sistema { get; set; }
        public int id_tipo_controle_sistema { get; set; }
        public TipoControleSistema TipoControleSistema { get; set; }
        public string cod_parametro { get; set; }
        public string val_parametro { get; set; }
        public string desc_controle { get; set; }
        public bool status { get; set; }
    }
}
