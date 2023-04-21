using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IIdiomaRepository : IRepository<Idioma>
    {
        Task<List<Idioma>> RetornarTodosPOC(int status);
        Task<List<Idioma>> ConsultarPOC(string descricao);
        Task<Idioma> ObterPorIdPOC(int id);
        Task<Idioma> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Idioma valor);
        Task AlterarPOC(Idioma valor);
        Task ExlcuirPOC(int Id);

    }
}

