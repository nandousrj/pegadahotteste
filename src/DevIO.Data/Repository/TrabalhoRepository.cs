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
    public class TrabalhoRepository : Repository<Trabalho>, ITrabalhoRepository
    {

        private readonly string _connectionString;
        public TrabalhoRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<Trabalho>> RetornarTodosPOC()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_Trabalho", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    var response = new List<Trabalho>();
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

        public virtual async Task<List<Trabalho>> ConsultarPOC(string descricao = "")
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TRABALHO_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    var response = new List<Trabalho>();
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


        public async Task<Trabalho> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_TRABALHO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    Trabalho response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Trabalho> ObterPorNomePOC(string descricao)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_TRABALHO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    Trabalho response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task IncluirPOC(Trabalho valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TRABALHO_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESC_TRABALHO", valor.desc_trabalho));                   
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task AlterarPOC(Trabalho valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TRABALHO_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_TRABALHO", valor.id_trabalho));
                    cmd.Parameters.Add(new SqlParameter("@DESC_TRABALHO", valor.desc_trabalho));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }



        public async Task ExlcuirPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteValue", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        private Trabalho MapToValue(SqlDataReader reader)
        {
            return new Trabalho()
            {
                id_trabalho = (int)reader["id_trabalho"],                
                desc_trabalho = reader["desc_trabalho"].ToString()
            };
        }

    }
}
