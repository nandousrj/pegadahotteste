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
    public class TipoCriticaRepository : Repository<TipoCritica>, ITipoCriticaRepository
    {

        private readonly string _connectionString;
        public TipoCriticaRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<TipoCritica>> RetornarTodosPOC(int status)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_TIPO_CRITICA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@STATUS", status));
                    var response = new List<TipoCritica>();
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

        public virtual async Task<List<TipoCritica>> ConsultarPOC(string descricao = "")
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TIPO_CRITICA_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    var response = new List<TipoCritica>();
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


        public async Task<TipoCritica> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_TIPO_CRITICA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    TipoCritica response = null;
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

        public virtual async Task<List<TipoCritica>> RetornaDadosQtdTipoCategoriaPOC(int id_garota_categoria, int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_QTD_GAROTA_CATEGORIA_TIPO_CURTICAO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", id_garota_categoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", id_categoria));
                    var response = new List<TipoCritica>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosQtdTipoCategoria(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<TipoCritica> ObterPorNomePOC(string descricao)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_TIPO_CRITICA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    TipoCritica response = null;
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

        public async Task IncluirPOC(TipoCritica valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TIPO_CRITICA_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESC_TIPO_CRITICA", valor.desc_tipo_critica));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM", valor.imagem));
                    cmd.Parameters.Add(new SqlParameter("@ORDEM", valor.ordem));
                    await sql.OpenAsync();
                  //  await cmd.ExecuteNonQueryAsync();
                    var id = await cmd.ExecuteScalarAsync();
                    valor.id_tipo_critica = (int)id;
                    return ;
                }
            }
        }

        public async Task AlterarPOC(TipoCritica valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_TIPO_CRITICA_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_TipoCritica", valor.id_tipo_critica));
                    cmd.Parameters.Add(new SqlParameter("@DESC_TIPO_CRITICA", valor.desc_tipo_critica));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM", valor.imagem));
                    cmd.Parameters.Add(new SqlParameter("@ORDEM", valor.ordem));
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


        private TipoCritica MapToValue(SqlDataReader reader)
        {
            return new TipoCritica()
            {
                id_tipo_critica = (int)reader["id_tipo_critica"],
                desc_tipo_critica = reader["desc_tipo_critica"].ToString(),
                status = (bool)reader["status"],
                imagem = reader["imagem"].ToString(),
                ordem = (int)reader["ordem"]
            };
        }

        private TipoCritica MapToValueRetornaDadosQtdTipoCategoria(SqlDataReader reader)
        {
            return new TipoCritica()
            {
                id_tipo_critica = (int)reader["id_tipo_critica"],
                desc_tipo_critica = reader["desc_tipo_critica"].ToString(),               
                imagem = reader["imagem"].ToString(),
                qtd_tipo2 = reader["qtd_tipo_critica"].ToString()
            };
        }

    }
}
