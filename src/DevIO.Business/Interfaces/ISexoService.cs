using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ISexoService : IDisposable
    {
        Task<bool> Incluir(Sexo zona);
        Task<bool> Alterar(Sexo zona);
        Task Excluir(int id);

    }
}
