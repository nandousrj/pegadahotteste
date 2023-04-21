using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.ViewModels
{
    public class EstiloViewModel
    {
        [Key]
        public int id_estilo { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string desc_estilo { get; set; }
    }
}
