using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Anuncio : Entity
    {
        public int id_anuncio { get; set; }
        public string desc_anuncio { get; set; }
        public string link_site { get; set; }
        public int ordem { get; set; }
        public bool status { get; set; }
        public string imagem01 { get; set; }
        public string observacao { get; set; }
    }
}
