using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "anuncio")]
    public class AnuncioImagemViewModel
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
        public IFormFile imagem01_upload { get; set; }
        public string imagem01 { get; set; }

        [DisplayName("Ordem")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]       
        public int ordem { get; set; }
        public bool status { get; set; }

        public string observacao { get; set; }
    }
}
