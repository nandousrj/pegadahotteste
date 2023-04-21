using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class ControleSistemaViewModel
    {
        [Key]
        public int id_controle_sistema { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar


        public int id_tipo_controle_sistema { get; set; }
        public TipoControleSistemaViewModel TipoControleSistema { get; set; }

        public string cod_parametro { get; set; }
        public string val_parametro { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string desc_controle_sistema { get; set; }

        public bool status { get; set; }
    }
}
