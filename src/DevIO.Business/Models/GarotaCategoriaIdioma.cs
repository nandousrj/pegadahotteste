using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class GarotaCategoriaIdioma : Entity
    {
        public int id_garota_categoria_idioma { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public Idioma Idioma { get; set; }
    }
}
