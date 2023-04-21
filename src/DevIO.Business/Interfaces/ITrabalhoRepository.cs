using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ITrabalhoRepository : IRepository<Trabalho>
    {
        Task<List<Trabalho>> RetornarTodosPOC();
        Task<List<Trabalho>> ConsultarPOC(string descricao);
        Task<Trabalho> ObterPorIdPOC(int id);
        Task<Trabalho> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Trabalho valor);
        Task AlterarPOC(Trabalho valor);
        Task ExlcuirPOC(int Id);

    }
}
