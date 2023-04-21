using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Parametros : Entity
    {
        public int id_parametros { get; set; }
        public Categoria Categoria { get; set; }
        public string valor_aluguel { get; set; }
        public string valor_destaque { get; set; }
        public string valor_casal { get; set; }
        public string nome_empresa { get; set; }
        public string apelido_empresa { get; set; }
        public string cnpj_empresa { get; set; }
        public string email_empresa { get; set; }
        public string senha_email_empresa { get; set; }
        public string telefone_empresa1 { get; set; }
        public string telefone_empresa2 { get; set; }

        public Banco Banco01 { get; set; }
        public string agencia_01 { get; set; }
        public string conta_01 { get; set; }
        public TipoConta TipoConta01 { get; set; }
        public Banco Banco02 { get; set; }
        public string agencia_02 { get; set; }
        public string conta_02 { get; set; }
        public TipoConta TipoConta02 { get; set; }
        public bool ind_promocao_mes_gratis { get; set; }
        public bool ind_mensagem_curtir { get; set; }
        public string mensagem_curtir { get; set; }
        public bool ind_mensagem_visualizar { get; set; }
        public string mensagem_visualizar { get; set; }
        public bool ind_curtir_dia { get; set; }
        public bool ind_promocao_cupom { get; set; }
        public string mensagem_promocao_cupom { get; set; }
        public string outro_email { get; set; }
    }
}
