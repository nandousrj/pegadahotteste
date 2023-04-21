using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class BairroViewModel
    {
        [Key]
        public int id_bairro { get; set; }

        public Guid Id { get; set; } 

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string desc_bairro { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool status { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Zona")]
        public int id_zona { get; set; }

        public ZonaViewModel Zona { get; set; }

        public IEnumerable<ZonaViewModel> Zonas { get; set; }

        [ScaffoldColumn(false)]
        public string descricao_zona { get; set; }
    }
}
