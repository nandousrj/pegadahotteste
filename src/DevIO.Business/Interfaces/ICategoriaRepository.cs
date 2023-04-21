using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<List<Categoria>> RetornarTodosPOC();
        Task<List<Categoria>> ConsultarPOC(string descricao);
        Task<Categoria> ObterPorIdPOC(int id);
        Task<Categoria> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Categoria valor);
        Task AlterarPOC(Categoria valor);
        Task ExlcuirPOC(int Id);

    }
}
