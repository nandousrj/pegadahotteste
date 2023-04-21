using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Email
    {
        public string emailRemetente { get; set; }
        public string emailDestinatario { get; set; }
        public string assunto { get; set; }
        public string corpo { get; set; }
        public string senha { get; set; }
        public string documento { get; set; }

    }
}
