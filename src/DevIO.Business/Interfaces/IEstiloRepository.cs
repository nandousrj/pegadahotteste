using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IEstiloRepository : IRepository<Estilo>
    {
        Task<List<Estilo>> RetornarTodosPOC();
        Task<List<Estilo>> ConsultarPOC(string descricao);
        Task<Estilo> ObterPorIdPOC(int id);
        Task<Estilo> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Estilo valor);
        Task AlterarPOC(Estilo valor);
        Task ExlcuirPOC(int Id);

    }
}
