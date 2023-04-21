using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class GarotaCategoriaAtende : Entity
    {
        public int id_garota_categoria_atende { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public Atende Atende { get; set; }
    }
}
