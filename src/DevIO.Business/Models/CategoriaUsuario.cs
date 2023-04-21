using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class CategoriaUsuario : Entity
    {
        public int id_categoria_usuario { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public bool Status { get; set; }
        public PermissoesUsuario PermissoesUsuario { get; set; }
        public Garota Garota { get; set; }
        public Categoria Categoria { get; set; }
    }
}
