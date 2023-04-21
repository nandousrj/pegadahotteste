using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IBancoRepository : IRepository<Banco>
    {
        Task<List<Banco>> RetornarTodosPOC();
        Task<List<Banco>> ConsultarPOC(string descricao);
        Task<Banco> ObterPorIdPOC(int id);
        Task<Banco> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Banco valor);
        Task AlterarPOC(Banco valor);
        Task ExlcuirPOC(int Id);

    }
}

