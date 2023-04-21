using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IIdiomaService : IDisposable
    {
        Task<bool> Incluir(Idioma zona);
        Task<bool> Alterar(Idioma zona);
        Task Excluir(int id);

    }
}
