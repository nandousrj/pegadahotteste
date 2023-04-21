using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IZonaService : IDisposable
    {
        Task<bool> Incluir(Zona zona);
        Task<bool> Alterar(Zona zona);
        Task Excluir(int id);

    }
}
