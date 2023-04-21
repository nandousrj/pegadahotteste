using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string apelido { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
        public string senha { get; set; }
        public DateTime dt_nascimento { get; set; }
        public string uf { get; set; }
        public string cidade { get; set; }
        public bool status { get; set; }
        public string dt_nascimento2 { get; set; }
    }
}
