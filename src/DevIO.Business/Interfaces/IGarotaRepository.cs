using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IGarotaRepository : IRepository<Garota>
    {
        Task<List<Garota>> RetornarTodosPOC(int ststus);
        Task<List<Garota>> ConsultarPOC(string nome);
        Task<Garota> ObterPorIdPOC(int id);
        Task<Garota> RetornarDadosGarotaSenha(string email, string senha);
        Task<Garota> RetornarDadosGarotaNascimentoSenha(string nascimento, string senha);
        Task<Garota> VerificarCPFGarotaExistente(string cpf);
        Task<Garota> RetornarEsqueceuSenha(string cpf, string email);
        Task<Garota> RetornarEsqueceuSenhaNascimento(string nascimento, string email);
        Task<Garota> RetornarEsqueceuEmail(string cpf);       
        Task IncluirPOC(Garota valor);
        Task AlterarPOC(Garota valor);
        Task<int> ResetarSenha(int id_garota);
        Task<int> AlterarSenha(int id_garota, string senha);
        Task<int> AlterarEmail(int id_garota, string email);        
        
        //Task ExlcuirPOC(int Id);

    }
}

