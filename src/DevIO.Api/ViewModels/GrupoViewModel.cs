using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class GrupoViewModel
    {
        [Key]
        public int id_grupo { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string desc_grupo { get; set; }

        public bool status { get; set; }

        [DisplayName("Imagem do Grupo")]
        public string imagem_01_upload { get; set; }
        public string imagem_01 { get; set; }
        public string observacao { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
