using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class GarotaCategoriaVisualizacoes
    {
        public int id_garota_categoria_visualizacoes { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public DateTime dt_visualizacao { get; set; }
        public string dt_visualizacao2 { get; set; }
    }
}
