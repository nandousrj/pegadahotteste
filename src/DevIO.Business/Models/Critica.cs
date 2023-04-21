using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Critica
    {
        public int id_critica { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public string nome { get; set; }
        public DateTime dt_critica { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public int nota { get; set; }
        public string comentario { get; set; }
        public bool ind_curtir { get; set; }
        public bool ind_ver { get; set; }
        public Categoria Categoria { get; set; }
        public TipoCritica TipoCritica { get; set; }

        public string cupom { get; set; }
        public DateTime dt_cupom_expira { get; set; }
        public bool ind_cupom_utilizado  { get; set; }
        public Usuario Usuario { get; set; }
        public string dt_cupom_expira2 { get; set; }
    }
}
