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
    public class VisualizacaoRepository : Repository<Visualizacao>, IVisualizacaoRepository
    {

        private readonly string _connectionString;
        public VisualizacaoRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }

             

        public async Task IncluirPOC(Visualizacao valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_VISUALIZACAO_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", valor.GarotaCategoria.id_garota_categoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.GarotaCategoria.Categoria.id_categoria));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task AlterarPOC(Visualizacao valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_VISUALIZACAO_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", valor.GarotaCategoria.id_garota_categoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.GarotaCategoria.Categoria.id_categoria));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }



        public async Task<int> AlterarSiteVisualizacaoGrupoPOC(int id_grupo)
        {
            int fileId;
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_SITE_GRUPO_VISUALIZACAO_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GRUPO", id_grupo));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }

        public async Task<int> AlterarSiteVisualizacaoPOC()
        {
            int fileId;
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_SITE_VISUALIZACAO_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                 //   cmd.Parameters.Add(new SqlParameter("@ID_NADA", 0));
                  //  cmd.Parameters.Add(new SqlParameter());
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }


        public async Task<int> RetornarTotalVisualizacaoSitePOC()
        {
            int fileId;
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_QTD_VISUALIZACAO_SITE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                   
                    await sql.OpenAsync();
                 
                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }

        public async Task<int> RetornarTotalVisualizacaoGarotasPOC()
        {
            int fileId;
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_QTD_VISUALIZACAO_GAROTAS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    await sql.OpenAsync();
                   
                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }

       

        public virtual async Task<List<Visualizacao>> RetornarTotalVisualizacaoSiteGrupoPOC()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_QTD_VISUALIZACAO_SITE_GRUPO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                  
                    var response = new List<Visualizacao>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }

        //public async Task ExlcuirPOC(int Id)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("NAO_TEM_EXCLUIR_AINDA", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@Id", Id));
        //            await sql.OpenAsync();
        //            await cmd.ExecuteNonQueryAsync();
        //            return;
        //        }
        //    }
        //}


        private Visualizacao MapToValue(SqlDataReader reader)
        {
            return new Visualizacao()
            {
                Grupo = new Grupo { id_grupo = (int)reader["id_grupo"], desc_grupo = reader["desc_grupo"].ToString() },
                quantidade = (int)reader["quantidade"]
            };
        }

    }
}
