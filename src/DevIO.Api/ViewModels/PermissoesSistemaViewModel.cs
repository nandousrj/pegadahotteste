﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class PermissoesSistemaViewModel
    {
        [Key]
        public int id_sistema { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string nome { get; set; }

        [DisplayName("Sigla")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string sigla { get; set; }

        [DisplayName("Código")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(3, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength =1)]
        public string codigo { get; set; }
    }
}
