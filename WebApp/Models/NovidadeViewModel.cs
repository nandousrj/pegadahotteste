using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class NovidadeViewModel
    {
        [Key]       

        public int id_novidade { get; set; }
        public string desc_novidade { get; set; }
   //     public CategoriaViewModel Categoria { get; set; }
        public GarotaCategoriaViewModel GarotaCategoria { get; set; }
        public int ordem { get; set; }
        public bool status { get; set; }

        public string imagem_01_upload { get; set; }
        public string imagem_01 { get; set; }

        [ScaffoldColumn(false)]
        public int idCategoria { get; set; }
        [ScaffoldColumn(false)]
        public string descCategoria { get; set; }
    }
}
