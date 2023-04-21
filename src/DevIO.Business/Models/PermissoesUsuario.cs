using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class PermissoesUsuario : Entity
    {
        public int id_usuario { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string senha_api { get; set; }
        public string senha_atual { get; set; }
        public string login_atual { get; set; }
        public bool status { get; set; }
        public bool status_termo { get; set; }
        public DateTime ultimo_acesso { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
        public DateTime expira_senha { get; set; }
        public bool forca_nova_senha { get; set; }
        public PermissoesSetor Setor { get; set; }
        public PermissoesPerfil Perfil { get; set; }
        public int id_instituicao { get; set; }
        public int id_setor { get; set; } 
        public string matricula { get; set; }
        public bool responsavel { get; set; }
        public bool bloqueio { get; set; }
        public List<PermissoesSistema> Sistemas { get; set; }
        public int id_sistema { get; set; } 
        public List<PermissoesPerfil> Perfis { get; set; }
        public int id_perfil { get; set; } 
        public int id_permissao { get; set; } 
        public string ip { get; set; } 
        public int quantidade_tentativa { get; set; }
        public string desc_sigla_instituicao { get; set; }
        public string desc_instituicao { get; set; }
        public string desc_setor { get; set; }
        public string desc_perfil { get; set; }

        public int util_value { get; set; }
        public string menu_value { get; set; }
        public int perfil_value { get; set; }

        public int id_usuario_acao { get; set; }
    }
}
