using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IVisualizacaoService : IDisposable
    {
        Task<bool> Incluir(Visualizacao visualizacao);
        Task<bool> Alterar(Visualizacao visualizacao);
        Task<bool> AlterarSiteVisualizacaoGrupo(int id_grupo);
        //  Task Excluir(int id);

    }
}
