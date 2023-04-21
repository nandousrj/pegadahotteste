using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IGarotaCategoriaIdiomaRepository : IRepository<GarotaCategoriaIdioma>
    {
        Task<List<GarotaCategoriaIdioma>> RetornarGarotaCategoriaIdioma(int id_garota_categoria);

    }

}


