using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Log
    {
        public int id_log { get; set; }
        public TipoLog TipoLog { get; set; }
        public PermissoesUsuario PermissoesUsuario { get; set; }
        public DateTime data_hora { get; set; }
        public string observacao { get; set; }
        public string ip { get; set; }
        public string hora { get; set; }
        public string data_hora2 { get; set; }
    }
}
