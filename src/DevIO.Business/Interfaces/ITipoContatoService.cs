using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITipoContatoService : IDisposable
    {
        Task<bool> Incluir(TipoContato tipocontato);
        Task<bool> Alterar(TipoContato tipocontato);
        Task Excluir(int id);

    }
}
