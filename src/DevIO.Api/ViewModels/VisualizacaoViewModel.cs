using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class VisualizacaoViewModel
    {
        [Key]
        public int id_visualizacao { get; set; }
        public GarotaCategoriaViewModel GarotaCategoria { get; set; }
        public int quantidade { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
    //    public CategoriaViewModel Categoria { get; set; }
        public string Ip { get; set; }
        public DateTime dt_visualizacao { get; set; }
        public GrupoViewModel Grupo { get; set; }
    }
}
