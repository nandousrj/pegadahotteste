using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class TipoCriticaViewModel
    {
        [Key]
        public int id_tipo_critica { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string desc_tipo_critica { get; set; }


        [DisplayName("Imagem do Tipo de Critica")]
        public string imagem_upload { get; set; }
        public string imagem { get; set; }

        [DisplayName("Ordem")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]       
        public string ordem { get; set; }
        public bool status { get; set; }

        public string qtd_tipo2 { get; set; }
    }
}
