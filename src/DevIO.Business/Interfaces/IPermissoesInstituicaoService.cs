using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IPermissoesInstituicaoService : IDisposable
    {
        Task<bool> Incluir(PermissoesInstituicao permissoesInstituicao);
        Task<bool> Alterar(PermissoesInstituicao permissoesInstituicao);
        Task Excluir(int id);

    }
}
