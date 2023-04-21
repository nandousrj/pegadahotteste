using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class GarotaCategoriaIdiomaRepository : Repository<GarotaCategoriaIdioma>, IGarotaCategoriaIdiomaRepository
    {

        private readonly string _connectionString;
        public GarotaCategoriaIdiomaRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }

               
        public virtual async Task<List<GarotaCategoriaIdioma>> RetornarGarotaCategoriaIdioma(int id_garota_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_GAROTA_CATEGORIA_IDIOMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", id_garota_categoria));
                    var response = new List<GarotaCategoriaIdioma>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosGarotaCategoriaIdioma(reader));
                        }
                    }

                    return response;
                }
            }
        }
       
       

        private GarotaCategoriaIdioma MapToValueRetornaDadosGarotaCategoriaIdioma(SqlDataReader reader)
        {
            return new GarotaCategoriaIdioma()
            {
                id_garota_categoria_idioma = (int)reader["id_garota_categoria_idioma"],
                GarotaCategoria = new GarotaCategoria { id_garota_categoria = (int)reader["id_garota_categoria"] },
                Idioma = new Idioma { id_idioma = (int)reader["id_idioma"], desc_idioma = reader["desc_idioma"].ToString() }
            };
        }
        

    }
}
