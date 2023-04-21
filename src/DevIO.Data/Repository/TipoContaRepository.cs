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
    public class TipoContaRepository : Repository<TipoConta>, ITipoContaRepository
    {

        private readonly string _connectionString;
        public TipoContaRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<TipoConta>> RetornarTodosPOC()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_TIPO_CONTA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    var response = new List<TipoConta>();
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

        public virtual async Task<List<TipoConta>> ConsultarPOC(string descricao = "")
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TIPO_CONTA_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    var response = new List<TipoConta>();
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


        public async Task<TipoConta> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_TIPO_CONTA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    TipoConta response = null;
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

        public async Task<TipoConta> ObterPorNomePOC(string descricao)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_TIPO_CONTA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    TipoConta response = null;
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

        public async Task IncluirPOC(TipoConta valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TIPO_CONTA_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESC_TIPO_CONTA", valor.desc_tipo_conta));
                    cmd.Parameters.Add(new SqlParameter("@SIGLA", valor.sigla));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task AlterarPOC(TipoConta valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TIPO_CONTA_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_TIPO_CONTA", valor.id_tipo_conta));
                    cmd.Parameters.Add(new SqlParameter("@DESC_TIPO_CONTA", valor.desc_tipo_conta));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.sigla));
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
                using (SqlCommand cmd = new SqlCommand("NAO_TEM_EXCLUIR_AINDA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        private TipoConta MapToValue(SqlDataReader reader)
        {
            return new TipoConta()
            {
                id_tipo_conta = (int)reader["id_tipo_Conta"],                
                desc_tipo_conta  = reader["desc_tipo_Conta"].ToString(),
                sigla = reader["sigla"].ToString()
            };
        }

    }
}
