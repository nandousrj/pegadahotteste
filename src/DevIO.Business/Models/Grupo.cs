using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Grupo : Entity
    {
        public int id_grupo { get; set; }
        public string desc_grupo { get; set; }
        public bool status { get; set; }
        public string imagem_01 { get; set; }
        public string observacao { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
