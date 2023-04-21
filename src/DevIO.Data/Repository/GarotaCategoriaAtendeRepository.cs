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
    public class GarotaCategoriaAtendeRepository : Repository<GarotaCategoriaAtende>, IGarotaCategoriaAtendeRepository
    {

        private readonly string _connectionString;
        public GarotaCategoriaAtendeRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<GarotaCategoriaAtende>> RetornarGarotaCategoriaAtende(int id_garota_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_GAROTA_CATEGORIA_ATENDE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", id_garota_categoria));
                    var response = new List<GarotaCategoriaAtende>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosGarotaCategoriaAtende(reader));
                        }
                    }                    

                    return response;
                }
            }
        }
               

       
        private GarotaCategoriaAtende MapToValueRetornaDadosGarotaCategoriaAtende(SqlDataReader reader)
        {
            return new GarotaCategoriaAtende()
            {
                id_garota_categoria_atende = (int)reader["id_garota_categoria_atende"],
                GarotaCategoria = new GarotaCategoria { id_garota_categoria = (int)reader["id_garota_categoria"] },
                Atende = new Atende  { id_atende = (int)reader["id_atende"], desc_atende = reader["desc_atende"].ToString() }
            };
        }     
        

    }
}
