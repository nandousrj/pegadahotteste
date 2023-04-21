using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IAtendeRepository : IRepository<Atende>
    {
        Task<List<Atende>> RetornarTodosPOC(int status);
        Task<List<Atende>> ConsultarPOC(string descricao);
        Task<Atende> ObterPorIdPOC(int id);
        Task<Atende> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Atende valor);
        Task AlterarPOC(Atende valor);
        Task ExlcuirPOC(int Id);

    }
}

