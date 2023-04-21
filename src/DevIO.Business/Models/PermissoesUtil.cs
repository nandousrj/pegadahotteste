using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class PermissoesUtil
    {
        public string acesso;
        public string ip;
        public int id_usuario_acao;

        private byte[] chave = { };
        private byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };
    }
}
