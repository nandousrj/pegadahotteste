using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoCriticaService : IDisposable
    {
        Task<bool> Incluir(TipoCritica TipoCritica);
        Task<bool> Alterar(TipoCritica TipoCritica);
        Task Excluir(int id);

    }
}
