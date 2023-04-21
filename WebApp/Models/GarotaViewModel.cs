using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class GarotaViewModel
    {
        [Key]
        public int id_garota { get; set; }

        public Guid Id { get; set; } // com entity tem que desabilitar      

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string nome { get; set; }
        public string cpf { get; set; }
        public string identidade { get; set; }
        public string email { get; set; }
        public SexoViewModel Sexo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Sexo")]
        public int id_sexo { get; set; }
        public bool status { get; set; }
        public string status2 { get; set; }

        [DisplayName("Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
       // [StringLength(8, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public DateTime dt_nascimento { get; set; }
        public string dt_nascimento2 { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string celular1 { get; set; }
        public string celular2 { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string descricao { get; set; }

        public decimal valor_aluguel { get; set; }
        public DateTime dt_cadastro { get; set; }
        public string dt_cadastro2 { get; set; }
        public decimal valor_destaque { get; set; }
        public int desconto { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string senha { get; set; }
        public string imagem_doc_frente { get; set; }
        public string imagem_doc_tras { get; set; }

        public string ip { get; set; }
        public int id_usuario { get; set; }

        //   public GarotaCategoriaViewModel GarotaCategoria { get; set; }

    }
}
