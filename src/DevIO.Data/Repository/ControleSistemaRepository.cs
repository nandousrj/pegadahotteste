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
    public class ControleSistemaRepository : Repository<ControleSistema>, IControleSistemaRepository
    {

        private readonly string _connectionString;
        public ControleSistemaRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<ControleSistema>> RetornarTodosPOC(int status)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_CONTROLE_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@STATUS", status));
                    var response = new List<ControleSistema>();
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

        public virtual async Task<List<ControleSistema>> ConsultarPOC(string descricao = "")
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_CONTROLE_SISTEMA_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    var response = new List<ControleSistema>();
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


        public async Task<ControleSistema> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_CONTROLE_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    ControleSistema response = null;
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

        public async Task<ControleSistema> ObterPorNomePOC(string descricao)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_CONTROLE_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    ControleSistema response = null;
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

        public async Task IncluirPOC(ControleSistema valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_CONTROLE_SISTEMA_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESC_CONTROLE", valor.desc_controle));
                    cmd.Parameters.Add(new SqlParameter("@ID_TIPO_CONTROLE_SISTEMA", valor.id_tipo_controle_sistema));
                    cmd.Parameters.Add(new SqlParameter("@COD_PARAMETRO", valor.cod_parametro));
                    cmd.Parameters.Add(new SqlParameter("@VAL_PARAMETRO", valor.val_parametro));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task AlterarPOC(ControleSistema valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_CONTROLE_SISTEMA_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_CONTROLE_SISTEMA", valor.id_controle_sistema));
                    cmd.Parameters.Add(new SqlParameter("@ID_TIPO_CONTROLE_SISTEMA", valor.id_tipo_controle_sistema));
                    cmd.Parameters.Add(new SqlParameter("@DESC_CONTROLE", valor.desc_controle));
                    cmd.Parameters.Add(new SqlParameter("@COD_PARAMETRO", valor.cod_parametro));
                    cmd.Parameters.Add(new SqlParameter("@VAL_PARAMETRO", valor.val_parametro));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
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


        private ControleSistema MapToValue(SqlDataReader reader)
        {
            return new ControleSistema()
            {
                id_controle_sistema = (int)reader["id_controle_sistema"],
                id_tipo_controle_sistema = (int)reader["id_tipo_controle_sistema"],
                desc_controle = reader["desc_controle"].ToString(),
                cod_parametro = reader["cod_parametro"].ToString(),
                val_parametro = reader["val_parametro"].ToString(),
                status = (bool)reader["status"],
                TipoControleSistema = new TipoControleSistema { id_tipo_controle_sistema = (int)reader["id_tipo_controle_sistema"], desc_tipo_controle = reader["desc_tipo_controle"].ToString() }
            };
        }

    }
}
