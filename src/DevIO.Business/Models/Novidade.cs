using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Novidade : Entity
    {
        public int id_novidade { get; set; }
        public string desc_novidade { get; set; }
   //     public Categoria Categoria { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public int ordem { get; set; }
        public bool status { get; set; }
        public string imagem_01 { get; set; }
       
    }
}
