using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IPermissoesSistemaRepository : IRepository<PermissoesSistema>
    {
        Task<List<PermissoesSistema>> RetornarTodosPOC();
        Task<List<PermissoesSistema>> ConsultarPOC(string descricao);
        Task<PermissoesSistema> ObterPorIdPOC(int id);
        Task<PermissoesSistema> ObterPorNomePOC(string descricao);
        Task IncluirPOC(PermissoesSistema valor);
        Task AlterarPOC(PermissoesSistema valor);
        Task ExlcuirPOC(int Id);

    }
}

