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
    public class PermissoesSistemaRepository : Repository<PermissoesSistema>, IPermissoesSistemaRepository
    {

        private readonly string _connectionString;
        public PermissoesSistemaRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<PermissoesSistema>> RetornarTodosPOC()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RETORNA_TODOS_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    var response = new List<PermissoesSistema>();
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

        public virtual async Task<List<PermissoesSistema>> ConsultarPOC(string descricao = "")
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PERMISSOES_SISTEMA_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOME", descricao.ToUpper().Trim()));
                    var response = new List<PermissoesSistema>();
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


        public async Task<PermissoesSistema> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RETORNA_DADOS_PERMISSOES_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    PermissoesSistema response = null;
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

        public async Task<PermissoesSistema> ObterPorNomePOC(string descricao)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RETORNA_DADOS_PERMISSOES_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOME", descricao.ToUpper().Trim()));
                    PermissoesSistema response = null;
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

        public async Task IncluirPOC(PermissoesSistema valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PERMISSOES_SISTEMA_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOME", valor.nome));
                    cmd.Parameters.Add(new SqlParameter("@SIGLA", valor.sigla));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO", valor.codigo));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task AlterarPOC(PermissoesSistema valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PERMISSOES_SISTEMA_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", valor.id_sistema));
                    cmd.Parameters.Add(new SqlParameter("@NOME", valor.nome));
                    cmd.Parameters.Add(new SqlParameter("@SIGLA", valor.sigla));
                    cmd.Parameters.Add(new SqlParameter("@CODIGO", valor.codigo));
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


        private PermissoesSistema MapToValue(SqlDataReader reader)
        {
            return new PermissoesSistema()
            {
                id_sistema = (int)reader["id_sistema"],                
                nome  = reader["nome"].ToString(),
                sigla = reader["sigla"].ToString(),
                codigo = reader["codigo"].ToString()
            };
        }

    }
}
