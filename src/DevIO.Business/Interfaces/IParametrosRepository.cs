using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IParametrosRepository : IRepository<Parametros>  
    {
        Task IncluirPOC(Parametros valor);
        Task AlterarPOC(Parametros valor);

        Task<Parametros> ObterPorIdPOC(int id);
        Task<Parametros> ObterPorIdExternoPOC(int id);
        Task<List<Parametros>> ConsultarPOC(string descricao);
        
        
        Task ExlcuirPOC(int Id);

    }
}
