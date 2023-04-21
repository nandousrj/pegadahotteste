using System.Collections.Generic;
using System.Linq;

namespace DevIO.Business.Models.Validations.Documentos
{
    public class LoginValidacao
    {
        public static bool Validar(string login)
        {
            bool erro = true;

            if (login.Trim().Length == 0)
                erro = false;
            else
            {
                if (!login.Contains("."))
                    erro = false;
                else
                {
                    string nome = login.Split('.')[0];
                    string sobrenome = login.Split('.')[1];

                    if ((nome.Trim().Length == 0) || (sobrenome.Trim().Length == 0))
                        erro = false;
                }
            }

            return erro;
        }
              
    }

  
}