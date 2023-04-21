using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IVisualizacaoRepository : IRepository<Visualizacao>
    {
        //Task<List<Zona>> RetornarTodosPOC(int status);
        //Task<List<Zona>> ConsultarPOC(string descricao);
        //Task<Zona> ObterPorIdPOC(int id);
        //Task<Zona> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Visualizacao valor);
        Task AlterarPOC(Visualizacao valor);
        Task<int> AlterarSiteVisualizacaoGrupoPOC(int id_grupo);
        Task<int> AlterarSiteVisualizacaoPOC();
        Task<int> RetornarTotalVisualizacaoSitePOC();
        Task<int> RetornarTotalVisualizacaoGarotasPOC();        
        Task<List<Visualizacao>> RetornarTotalVisualizacaoSiteGrupoPOC();
        //Task ExlcuirPOC(int Id);

    }
}

