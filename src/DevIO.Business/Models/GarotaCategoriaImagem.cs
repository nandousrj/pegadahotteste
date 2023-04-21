using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class GarotaCategoriaImagem
    {
        public int id_garota_categoria_imagem { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public string imagem { get; set; }
        public int ordem { get; set; }
        public bool ind_capa { get; set; }
    }
}
