using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class PermissoesPerfil : Entity
    {
        public int id_perfil { get; set; }
        public string nome { get; set; }
        public bool dtatus { get; set; }
        public DateTime data_cadastro { get; set; }
        public DateTime data_expiracao { get; set; }
      //  public List<PermissoesMenu> Menus { get; set; }
        public PermissoesSistema Sistema { get; set; }
    }
}
