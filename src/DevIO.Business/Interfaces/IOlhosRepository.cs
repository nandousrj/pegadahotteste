using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IOlhosRepository : IRepository<Olhos>
    {
        Task<List<Olhos>> RetornarTodosPOC(int status);
        Task<List<Olhos>> ConsultarPOC(string descricao);
        Task<Olhos> ObterPorIdPOC(int id);
        Task<Olhos> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Olhos valor);
        Task AlterarPOC(Olhos valor);
        Task ExlcuirPOC(int Id);

    }
}

