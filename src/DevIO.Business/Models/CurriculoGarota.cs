using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class CurriculoGarota
    {
        public int id_curriculo_garota { get; set; }
        public Categoria Categoria { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public DateTime dt_nascimento { get; set; }
        public string apelido { get; set; }
        public string site { get; set; }
        public string observacao { get; set; }
        public string imagem { get; set; }

        public string dt_inclusao_2 { get; set; }
        public string dt_nascimento2 { get; set; }
    }
}
