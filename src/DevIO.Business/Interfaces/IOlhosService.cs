using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IOlhosService : IDisposable
    {
        Task<bool> Incluir(Olhos zona);
        Task<bool> Alterar(Olhos zona);
        Task Excluir(int id);

    }
}
