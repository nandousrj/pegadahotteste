using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Interfaces
{
    public interface IGarotaService : IDisposable
    {
        Task<bool> Incluir(Garota garota);
        Task<bool> Alterar(Garota garota);
       

    }
}
