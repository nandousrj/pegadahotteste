using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class GarotaCategoriaAtendeViewModel
    {
        [Key]       

        public int id_garota_categoria_atende { get; set; }
        public GarotaCategoriaViewModel GarotaCategoria { get; set; }
        public AtendeViewModel Atende { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar
    }
}
