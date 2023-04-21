namespace DevIO.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; } // chave de criptografia do token
        public int ExpiracaoHoras { get; set; }
        public int ExpiracaoMinutos { get; set; }
        public string Emissor { get; set; } // quem está emitindo, no caso a aplicãção
        public string ValidoEm { get; set; } // em quais urls estes tokens são válidos
    }
}