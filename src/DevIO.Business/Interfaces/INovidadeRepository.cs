using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface INovidadeRepository : IRepository<Novidade>
    {
       // Task<List<Novidade>> RetornarTodosPOC(int status);
        Task<List<Novidade>> ConsultarPOC();
        Task<Novidade> ObterPorIdPOC(int id);
        Task<Novidade> ObterPorNomePOC(string descricao);
        Task<List<Novidade>> ObterDadosAtivos(int id_categoria);
        Task IncluirPOC(Novidade valor);
        Task AlterarPOC(Novidade valor);
        Task ExlcuirPOC(int Id);

    }
}

