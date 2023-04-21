using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class GarotaCategoriaUsuario
    {
        public int id_garota_categoria_usuario { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public bool status { get; set; }
        public PermissoesUsuario PermissoesUsuario { get; set; }
        public Garota Garota { get; set; }
        public Categoria Categoria { get; set; }
    }
}
