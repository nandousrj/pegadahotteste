using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class PermissoesMenu
    {
        public int id_menu { get; set; }
        public string descricao { get; set; }
        public string caminho { get; set; }
        public string titulo { get; set; }
        public int ordem { get; set; }
        public int id_pai { get; set; }
        public string desc_pai { get; set; }
        public PermissoesSistema Sistema { get; set; }
        public PermissoesSistema PermissoesSistema { get; set; }
    }
}
