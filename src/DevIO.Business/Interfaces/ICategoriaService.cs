using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ICategoriaService : IDisposable
    {
        Task<bool> Incluir(Categoria categoria);
        Task<bool> Alterar(Categoria categoria);
        Task Excluir(int id);

    }
}
