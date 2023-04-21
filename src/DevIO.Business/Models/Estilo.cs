using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    [Table("AG_ESTILO")]
    public class Estilo : Entity
    {
        public Guid Id { get; set; } // com entity tem que desabilitar
        public int id_estilo { get; set; }
        public string desc_estilo { get; set; }
    }
}
