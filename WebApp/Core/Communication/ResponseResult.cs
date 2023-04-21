using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Core.Communication
{
    public class ResponseResult
    {
        // tem que inicializar abaixo, senão dá erro na MainController, na ResponsePossuiErros(leva os valores zerados e nulos)
        public ResponseResult()
        {
            Errors = new ResponseErrorMessages();
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
    }

    public class ResponseErrorMessages
    {
        // tem que inicializar abaixo, senão dá erro na MainController, na ResponsePossuiErros(leva os valores zerados e nulos)
        public ResponseErrorMessages()
        {
            Mensagens = new List<string>();
        }

        public List<string> Mensagens { get; set; }
    }
}