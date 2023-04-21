using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Contato
    {
        public int id_contato { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public TipoContato TipoContato { get; set; }
        public string mensagem { get; set; }
        public string ip { get; set; }
        public DateTime data { get; set; }
        public Categoria Categoria { get; set; }

        public string data2 { get; set; }
    }
}
