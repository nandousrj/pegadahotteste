using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class AnuncioViewModel
    {
        [Key]
        public int id_anuncio { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string desc_anuncio { get; set; }
        public string link_site { get; set; }


        [DisplayName("Imagem do Anuncio")]
        public string imagem01_upload { get; set; }
        public string imagem01 { get; set; }

        [DisplayName("Ordem")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]       
        public int ordem { get; set; }
        public bool status { get; set; }

        public string observacao { get; set; }
    }
}
