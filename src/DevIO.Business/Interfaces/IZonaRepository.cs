using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IZonaRepository : IRepository<Zona>
    {
        Task<List<Zona>> RetornarTodosPOC(int status);
        Task<List<Zona>> ConsultarPOC(string descricao);
        Task<Zona> ObterPorIdPOC(int id);
        Task<Zona> ObterPorNomePOC(string descricao);
        Task IncluirPOC(Zona valor);
        Task AlterarPOC(Zona valor);
        Task ExlcuirPOC(int Id);

    }
}

