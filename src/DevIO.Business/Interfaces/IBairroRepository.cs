using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IBairroRepository : IRepository<Bairro>
    {
        Task<List<Bairro>> RetornarTodosPOC(int id_zona);
        Task<List<Bairro>> ConsultarPOC(string descricao);
        Task<Bairro> ObterPorIdPOC(int id);
        Task<Bairro> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Bairro valor);
        Task AlterarPOC(Bairro valor);
        Task ExlcuirPOC(int Id);

    }
}

