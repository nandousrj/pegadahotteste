using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class GarotaCategoriaIdiomaViewModel
    {
        [Key]       

        public int id_garota_categoria_idioma { get; set; }
        public GarotaCategoriaViewModel GarotaCategoria { get; set; }
        public IdiomaViewModel Atende { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar
    }
}
