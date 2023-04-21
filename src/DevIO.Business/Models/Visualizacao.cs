using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Visualizacao : Entity
    {
        public int id_visualizacao { get; set; }
        public GarotaCategoria GarotaCategoria { get; set; }
        public int quantidade { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
    //    public Categoria Categoria { get; set; }
        public string Ip { get; set; }
        public DateTime dt_visualizacao { get; set; }
        public Grupo Grupo { get; set; }
    }
}
