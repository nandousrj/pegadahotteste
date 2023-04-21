using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Garota : Entity
    {
        public int id_garota { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string identidade { get; set; }
        public string email { get; set; }
        public Sexo Sexo { get; set; }       
        public int id_sexo { get; set; }
        public bool status { get; set; }
        public string status2 { get; set; }
        public DateTime dt_nascimento { get; set; }
        public string dt_nascimento2 { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string celular1 { get; set; }
        public string celular2 { get; set; }
        public string descricao { get; set; }

        public decimal valor_aluguel { get; set; }
        public DateTime dt_cadastro { get; set; }
        public string dt_cadastro2 { get; set; }
        public decimal valor_destaque { get; set; }
        public int desconto { get; set; }
        public string senha { get; set; }
        public string imagem_doc_frente { get; set; }
        public string imagem_doc_tras { get; set; }
        public string ip { get; set; }
        public int id_usuario { get; set; }

        //   public GarotaCategoria GarotaCategoria { get; set; }
    }
}
