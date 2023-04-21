using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IGarotaCategoriaService : IDisposable
    {
        Task<bool> Incluir(GarotaCategoria garotacategoria);
        Task<bool> Alterar(GarotaCategoria garotacategoria);
       

    }
}
