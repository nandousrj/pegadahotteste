using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IPermissoesInstituicaoRepository : IRepository<PermissoesInstituicao>
    {
        Task<List<PermissoesInstituicao>> RetornarTodosPOC();
        Task<List<PermissoesInstituicao>> ConsultarPOC(string descricao);
        Task<PermissoesInstituicao> ObterPorIdPOC(int id);
        Task<PermissoesInstituicao> ObterPorNomePOC(string descricao);
        Task IncluirPOC(PermissoesInstituicao valor);
        Task AlterarPOC(PermissoesInstituicao valor);
        Task ExlcuirPOC(int Id);

    }
}

