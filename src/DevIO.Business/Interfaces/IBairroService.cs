using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IBairroService : IDisposable
    {
        Task<bool> Incluir(Bairro bairro);
        Task<bool> Alterar(Bairro bairro);
        Task Excluir(int id);

    }
}
