using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IGarotaCategoriaAtendeRepository : IRepository<GarotaCategoriaAtende>
    {
        Task<List<GarotaCategoriaAtende>> RetornarGarotaCategoriaAtende(int id_garota_categoria);

    }
}

