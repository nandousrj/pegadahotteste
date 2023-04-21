using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        Task<List<Grupo>> RetornarTodosPOC(int status);
        Task<List<Grupo>> ConsultarPOC(string descricao);
        Task<Grupo> ObterPorIdPOC(int id);
        Task<Grupo> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Grupo valor);
        Task AlterarPOC(Grupo valor);
        Task ExlcuirPOC(int Id);

    }
}

