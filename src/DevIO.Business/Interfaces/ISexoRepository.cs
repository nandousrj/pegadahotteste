using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface ISexoRepository : IRepository<Sexo>
    {
        Task<List<Sexo>> RetornarTodosPOC(int status);
        Task<List<Sexo>> ConsultarPOC(string descricao);
        Task<Sexo> ObterPorIdPOC(int id);
        Task<Sexo> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Sexo valor);
        Task AlterarPOC(Sexo valor);
        Task ExlcuirPOC(int Id);

    }
}

