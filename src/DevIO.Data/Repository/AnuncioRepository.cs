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
    public class AnuncioRepository : Repository<Anuncio>, IAnuncioRepository
    {

        private readonly string _connectionString;
        public AnuncioRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<Anuncio>> RetornarTodosPOC(int status)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_ANUNCIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@STATUS", status));
                    var response = new List<Anuncio>();
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


        public virtual async Task<List<Anuncio>> RetornarTodosAtivosPOC()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_ANUNCIO_ATIVOS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    var response = new List<Anuncio>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornarTodosAtivosPOC(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task IncluirPOC(Anuncio valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_ANUNCIO_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESC_ANUNCIO", valor.desc_anuncio));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE", valor.link_site));
                    cmd.Parameters.Add(new SqlParameter("@ORDEM", valor.ordem));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_01", valor.imagem01));
                    cmd.Parameters.Add(new SqlParameter("@OBSERVACAO", valor.observacao));
                    await sql.OpenAsync();
                    //  await cmd.ExecuteNonQueryAsync();
                    var id = await cmd.ExecuteScalarAsync();
                    valor.id_anuncio = (int)id;
                    return;
                }
            }
        }

        public virtual async Task<List<Anuncio>> ConsultarPOC(string descricao = "")
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_ANUNCIO_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    var response = new List<Anuncio>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueConsultarPOC(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task AlterarPOC(Anuncio valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_ANUNCIO_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_ANUNCIO", valor.id_anuncio));
                    cmd.Parameters.Add(new SqlParameter("@DESC_ANUNCIO", valor.desc_anuncio));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE", valor.link_site));
                    cmd.Parameters.Add(new SqlParameter("@ORDEM", valor.ordem));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_01", valor.imagem01));
                    cmd.Parameters.Add(new SqlParameter("@OBSERVACAO", valor.observacao));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task<Anuncio> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_ANUNCIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    Anuncio response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornarTodosAtivosPOC(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public async Task<Anuncio> ObterPorNomePOC(string descricao)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_ANUNCIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    Anuncio response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornarTodosAtivosPOC(reader);
                        }
                    }

                    return response;
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


        private Anuncio MapToValue(SqlDataReader reader)
        {
            return new Anuncio()
            {
                id_anuncio = (int)reader["id_anuncio"],
                desc_anuncio = reader["desc_anuncio"].ToString()
            };
        }


        private Anuncio MapToValueRetornarTodosAtivosPOC(SqlDataReader reader)
        {
            return new Anuncio()
            {
                id_anuncio = (int)reader["id_anuncio"],
                desc_anuncio = reader["desc_anuncio"].ToString(),
                link_site = reader["link_site"].ToString(),
                ordem = (int)reader["ordem"],
                status = (bool)reader["status"],
                imagem01 = reader["imagem_01"].ToString(),
                observacao = reader["observacao"].ToString()
            };
        }

        private Anuncio MapToValueConsultarPOC(SqlDataReader reader)
        {
            return new Anuncio()
            {
                id_anuncio = (int)reader["id_anuncio"],
                desc_anuncio = reader["desc_anuncio"].ToString(),               
                ordem = (int)reader["ordem"],
                status = (bool)reader["status"]               
            };
        }
       

    }
}
