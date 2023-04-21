using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class TipoContato : Entity
    {
        public int id_tipo_contato { get; set; }
        public string desc_tipo_contato { get; set; }
    }
}
