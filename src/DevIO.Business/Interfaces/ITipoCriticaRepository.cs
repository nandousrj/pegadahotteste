using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface ITipoCriticaRepository : IRepository<TipoCritica>
    {
        Task<List<TipoCritica>> RetornarTodosPOC(int status);
        Task<List<TipoCritica>> ConsultarPOC(string descricao);
        Task<TipoCritica> ObterPorIdPOC(int id);
        Task<List<TipoCritica>> RetornaDadosQtdTipoCategoriaPOC(int id_garota_categoria, int id_categoria);
        Task<TipoCritica> ObterPorNomePOC(string descricao);
        Task IncluirPOC(TipoCritica valor);
        Task AlterarPOC(TipoCritica valor);
        Task ExlcuirPOC(int Id);

    }
}

