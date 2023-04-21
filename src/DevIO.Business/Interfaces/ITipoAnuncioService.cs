using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoAnuncioService : IDisposable
    {
        Task<bool> Incluir(TipoAnuncio zona);
        Task<bool> Alterar(TipoAnuncio zona);
        Task Excluir(int id);

    }
}
