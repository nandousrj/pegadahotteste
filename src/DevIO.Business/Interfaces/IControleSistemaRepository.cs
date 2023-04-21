using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IControleSistemaRepository : IRepository<ControleSistema>
    {
        Task<List<ControleSistema>> RetornarTodosPOC(int status);
        Task<List<ControleSistema>> ConsultarPOC(string descricao);
        Task<ControleSistema> ObterPorIdPOC(int id);
        Task<ControleSistema> ObterPorNomePOC(string descricao);
        Task IncluirPOC(ControleSistema valor);
        Task AlterarPOC(ControleSistema valor);
        Task ExlcuirPOC(int Id);

    }
}

