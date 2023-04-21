//using SecureApp;
using Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Util
    {
        public static string caminhoImagem = "Imagens//";
        public static string caminhoFoto = caminhoImagem + "ImgFotos//Categoria_";
        public static string caminhoDocFoto = caminhoImagem + "ImgDocFotos//Categoria_";
        public static string caminhoFotoCurriculoGarota = caminhoImagem + "ImgFotosCurriculoGarota";
        public static string caminhoEmotions = caminhoImagem + "ImgSite//Emotions";
        public static string caminhoNovidade = caminhoImagem + "ImgNovidade";
        public static string caminhoGrupo = caminhoImagem + "ImgGrupo";
        public static string caminhoAnuncio = caminhoImagem + "ImgAnuncio";
        //  public static string caminhoFotoTerapeuta = "ImgFotos//TeraputaGeral//";
        //  public static string caminhoFotoMassagista = "ImgFotos//MassagistaGeral//";
        //  public static string caminhoFotoAgencia = "ImgFotos//AgenciaGeral//";

        public static int tamMaximoDoc = 61440; // 60 kb
        public static int tamMaximoParamNome = 50;
        //public static int tamanhamaximoimagens = 307200; // 300 kb * 1024
        public static int tamMaximoImagens = 5120000; // 5000 kb * 1024
        public static string idVisto = "";


        public string Acesso;
        public string Ip;
        public int IdUsuarioAcao;

        private byte[] chave = { };
        private byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };

        public static string RemoverAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }

        public static string RetornaIp()
        {
            string ip = string.Empty;
            IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            for (int i = 0; i < addressList.Length; i++) ip += addressList[i].ToString() + "\n";
            return ip;
        }

        /*      public static int VerificaPermissao(int idPerfil, int idMenu)
                {
                    try
                    {
                        List<Menu> lstMenu = new List<Menu>();
                        lstMenu.Add(new Menu(idMenu));

                        Perfil oPerfil = new Perfil(idPerfil);
                        oPerfil.LstMenu = lstMenu;
                        oPerfil.IdPerfil = oPerfil.PermissaoPerfil();

                        return oPerfil.IdPerfil;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }*/

        public static void EnviarEmail(string emailRemetente, string nomeRemetente, string emailDestino, string nomeDestino,
                                       string assunto, string corpo, string emailCopia)
        {

            MailAddress from = new MailAddress(emailRemetente, nomeRemetente);
            MailAddress to = new MailAddress(emailDestino, nomeDestino);

            MailMessage msg = new MailMessage(from, to);
            msg.IsBodyHtml = true;
            msg.Subject = assunto;
            msg.Body = corpo;
            msg.BodyEncoding = Encoding.UTF8;

            if (emailCopia != string.Empty)
            {
                string[] arrCopia = emailCopia.Split(';');

                for (int i = 0; i < arrCopia.Length; i++)
                {
                    if (arrCopia[i] != string.Empty)
                    {
                        string[] arrDadosEmail = arrCopia[i].Split('^');
                        msg.CC.Add(new MailAddress(arrDadosEmail[0], arrDadosEmail[1]));
                    }
                }
            }

            msg.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "10.0.65.17";
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = true;

            try
            {
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (msg != null)
                    msg.Dispose();
            }
        }

        public static void EnviarEmailExterno(string emailRemetente, string nomeRemetente, string emailDestino, string nomeDestino,
                                       string assunto, string corpo, string emailCopia)
        {
            SmtpClient cliente = new SmtpClient("smtp.datasus.gov", 25 /* TLS */);
            //cliente.EnableSsl = true;

            MailAddress remetente = new MailAddress(emailRemetente, nomeRemetente);
            MailAddress destinatario = new MailAddress(emailDestino, nomeDestino);

            MailMessage mensagem = new MailMessage(remetente, destinatario);

            mensagem.Body = corpo;
            mensagem.Subject = assunto;
            mensagem.ReplyTo = remetente;

            if (emailCopia != string.Empty)
            {
                string[] arrCopia = emailCopia.Split(';');

                for (int i = 0; i < arrCopia.Length; i++)
                {
                    if (arrCopia[i] != string.Empty)
                    {
                        string[] arrDadosEmail = arrCopia[i].Split('^');
                        mensagem.CC.Add(new MailAddress(arrDadosEmail[0], arrDadosEmail[1]));
                    }
                }
            }

            mensagem.IsBodyHtml = true;
            mensagem.Priority = MailPriority.High;

            NetworkCredential credenciais = new NetworkCredential("marcio.monteiro@nerj.rj.saude.gov.br", "msilva");

            cliente.Credentials = credenciais;

            try
            {
                cliente.Send(mensagem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EnviarMensagem(string Assunto, string Mensagem, string Destinatario)
        {
            try
            {
                //Montar a mensagem (quem manda/quem recebe)
                MailMessage msg = new MailMessage(Destinatario, Destinatario);
                msg.Subject = Assunto;
                msg.Body = Mensagem;

                // Configurar o protocolo SMTP
                SmtpClient s = new SmtpClient("smtp.gmail.com", 587);
                s.EnableSsl = true; //criptografia
                s.Credentials = new NetworkCredential("cotiexemplo@gmail.com", "@coticoti@");
                s.Send(msg); // envio de email
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao enviar o Email: " + e.Message);
            }
        }

        public static int GerarNumeroAleatorioSemRepeticao(string reg, int min, int max)
        {
            char[] letras = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Random ra = new Random();
            int escolheLetra = new Random().Next(0, (letras.Length - 1));
            char la = letras[escolheLetra];

            foreach (char obj in reg)
            {
                if (obj == la)
                {
                    escolheLetra = ra.Next(0, (letras.Length - 1));
                    la = letras[escolheLetra];
                }
            }

            return Convert.ToInt16(la);
        }

        public static char GerarLetraAleatoriaSemRepeticao(string reg)
        {
            char[] letras = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'X', 'Y', 'Z' };
            Random ra = new Random();
            int escolheLetra = new Random().Next(0, (letras.Length - 1));
            char la = letras[escolheLetra];

            foreach (char obj in reg)
            {
                if (obj == la)
                {
                    escolheLetra = ra.Next(0, (letras.Length - 1));
                    la = letras[escolheLetra];
                }
            }

            return la;
        }

        public static DataTable ConvertTo<T>(IList<T> lst)
        {
            //create DataTable Structure
            DataTable tbl = CreateTable<T>();
            Type entType = typeof(T);

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            //get the list item and add into the list
            foreach (T item in lst)
            {
                DataRow row = tbl.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                tbl.Rows.Add(row);
            }

            return tbl;
        }

        public static DataTable CreateTable<T>()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new DataTable(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            foreach (PropertyDescriptor prop in properties)
            {
                //add property as column
                tbl.Columns.Add(prop.Name, prop.PropertyType);
            }
            return tbl;
        }

        public static string CapturaIp()
        {
            string ip = string.Empty;
            IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            for (int i = 0; i < addressList.Length; i++)
            {
                ip += addressList[i].ToString() + "\n";
            }

            return ip;
        }

        public static string Concatena(string valor, string nvValor)
        {
            if (valor != string.Empty)
                return valor + "<br/>" + nvValor;
            else
                return nvValor;
        }

        public static bool ValidaSenha(string senha)
        {
            byte[] ASCIIValues = Encoding.ASCII.GetBytes(senha);
            bool retorno = false;
            int alfa = 0;
            int outros = 0;

            foreach (byte b in ASCIIValues)
            {
                if (((b >= 32) && (b <= 64)) || ((b >= 91) && (b <= 96)) || ((b >= 123) && (b <= 126)))
                {
                    outros++;
                }
                else if (((b >= 65) && (b <= 90)) || ((b >= 97) && (b <= 122)))
                {
                    alfa++;
                }
            }

            if ((alfa >= 4) && (outros >= 4))
                retorno = true;

            return retorno;
        }

        public static bool ValidaLogin(string login)
        {
            try
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
            catch
            {
                return false;
            }
        }

        public static bool ValidaEmail(string email)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"

                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"

                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"

                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"

                        + @"[a-zA-Z]{2,}))$";

            Regex reStrict = new Regex(patternStrict);

            bool isStrictMatch = reStrict.IsMatch(email);

            return isStrictMatch;
        }


        public static int EnviaEmail(string emailRemetente, string emailDestinatario, string assunto, string corpo)
        {
            try
            {
                MailAddress from = new MailAddress(emailRemetente);
                MailAddress to = new MailAddress(emailDestinatario);
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = assunto;
                msg.Body = corpo;
                //att = new Attachment("Y:\\Inetpub\\Desenvolvimento\\EmailRC\\EmailRC\\Vale-transporte 2013.doc");
                //msg.Attachments.Add(att);
                msg.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                msg.Priority = MailPriority.High;
                //msg.Headers.Add("Disposition-Notification-To", "grupoavaliacao@nerj.rj.saude.gov.br");
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "exchange.datasus.gov";
                smtp.EnableSsl = true;
                NetworkCredential credenciais = new NetworkCredential("nerj\\informatica", "abcd.123");
                smtp.Credentials = credenciais;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                //smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                //smtp.UseDefaultCredentials = true;

                smtp.Send(msg);

                return 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static bool ValidarData(string data)
        {
            DateTime resultado = DateTime.MinValue;
            if (data.Length == 10)
            {
                if (DateTime.TryParse(data, out resultado))
                {
                    //  Response.Write("Data Válida.");
                    return true;
                }
                else
                {
                    //    Response.Write("Data Inválida.");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string AcertaCPF(string valor)
        {
            valor = valor.Replace(".", "");
            valor = valor.Replace("-", "");
            valor = valor.Replace("/", "");
            valor = valor.Replace(" ", "");
            if (valor.Length >= 11)
            {
                valor = valor.Substring(0, 3) + "." + valor.Substring(3, 3) + "." + valor.Substring(6, 3) + "-" + valor.Substring(9, 2);
            }
            else
            {
                valor = "";
            }

            return valor;
        }

        public static string AcertaCNPJ(string valor)
        {
            valor = valor.Replace(".", "");
            valor = valor.Replace("-", "");
            valor = valor.Replace("/", "");
            valor = valor.Replace(" ", "");

            if (valor.Length >= 14)
            {
                valor = valor.Substring(0, 2) + "." + valor.Substring(2, 3) + "." + valor.Substring(5, 3) + "/" + valor.Substring(8, 4) + "-" + valor.Substring(12, 2);
            }
            else
            {
                valor = "";
            }

            return valor;
        }



        public static bool ValidaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }


        public static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool ValidaPIS(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            return pis.EndsWith(resto.ToString());
        }


        public static string AcertaTelefone(string valor)
        {
            valor = valor.Replace("(", "");
            valor = valor.Replace(")", "");
            valor = valor.Replace("-", "");
            if (valor.Length >= 10)
            {
                valor = "(" + valor.Substring(0, 2) + ")" + valor.Substring(2, 4) + "-" + valor.Substring(6, 4);
            }
            else
            {
                valor = "";
            }

            return valor;
        }

        public static string AcertaCelular(string valor)
        {
            valor = valor.Replace("(", "");
            valor = valor.Replace(")", "");
            valor = valor.Replace("-", "");
            if (valor.Length >= 11)
            {
                valor = "(" + valor.Substring(0, 2) + ")" + valor.Substring(2, 5) + "-" + valor.Substring(7, 4);
            }
            else
            {
                valor = "";
            }

            return valor;
        }


        public static string RetornaValorCriptografado(string valor)
        {
            DTICrypto objCrypto = new DTICrypto();
            string senhaCript = objCrypto.Cifrar(valor, "agencia");
            valor = senhaCript;
            return valor;
        }

        public static string RetornaValorDescriptografado(string valor)
        {
            DTICrypto objCrypto = new DTICrypto();
            string senhaCript = objCrypto.Decifrar(valor, "agencia");
            valor = senhaCript;
            return valor;
        }



        public static bool VerificaValorInteiro(string valor)
        {
            //  bool retorno = Regex.IsMatch(valor, @"^\d{9}$");
            int valorInteiro;
            bool retorno = int.TryParse(valor, out valorInteiro);

            return retorno;
        }

        public static string RetornaApenasNumeros(string toNormalize)
        {
            string resultString = string.Empty;
            Regex regexObj = new Regex(@"[^\d]");
            resultString = regexObj.Replace(toNormalize, "");
            return resultString;
        }

        public static string TiraAcentos(string texto)
        {
            string ComAcentos = "!@#$%¨&*()-?:{}][ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç ";

            string SemAcentos = "_________________AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < ComAcentos.Length; i++)
                if (ComAcentos[i].ToString() != " ")
                {
                    texto = texto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();
                }
                else { texto = texto.Replace(ComAcentos[i].ToString(), ""); }


            return texto;
        }



        public string Descriptografar(string chaveCriptografia)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs; byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                input = new byte[Acesso.Length];
                input = Convert.FromBase64String(Acesso.Replace(" ", "+"));

                chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Criptografar(string chaveCriptografia)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs;
            byte[] input;
            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                input = Encoding.UTF8.GetBytes(Acesso);
                chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));
                cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      


    }
}
