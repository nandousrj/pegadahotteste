using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IAnuncioService : IDisposable
    {
        Task<bool> Incluir(Anuncio Anuncio);
        Task<bool> Alterar(Anuncio Anuncio);
        Task Excluir(int id);

    }
}
