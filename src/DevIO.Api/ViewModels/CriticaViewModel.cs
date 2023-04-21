using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class CriticaViewModel
    {
        [Key]
        //public int id_tipo_critica { get; set; }

        //public Guid Id { get; set; } // com entity tem que desabilitar

        //[DisplayName("Descrição")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //[StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        //public string desc_tipo_critica { get; set; }


        //[DisplayName("Imagem do Tipo de Critica")]
        //public string imagem_upload { get; set; }
        //public string imagem { get; set; }

        //[DisplayName("Ordem")]
        //[Required(ErrorMessage = "O campo {0} é obrigatório")]       
        //public string ordem { get; set; }
        //public bool status { get; set; }


        public int id_critica { get; set; }
        public GarotaCategoriaViewModel GarotaCategoria { get; set; }
        public string nome { get; set; }
        public DateTime dt_critica { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public int nota { get; set; }
        public string comentario { get; set; }
        public bool ind_curtir { get; set; }
        public bool ind_ver { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public TipoCriticaViewModel TipoCritica { get; set; }

        public string cupom { get; set; }
        public DateTime dt_cupom_expira { get; set; }
        public bool ind_cupom_utilizado { get; set; }
       // public Usuario Usuario { get; set; }
        public string dt_cupom_expira2 { get; set; }
    }
}
