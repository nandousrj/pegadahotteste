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
    public class NovidadeRepository : Repository<Novidade>, INovidadeRepository
    {

        private readonly string _connectionString;
        public NovidadeRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        //public virtual async Task<List<Novidade>> RetornarTodosPOC(int status)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_NOVIDADE", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@STATUS", status));
        //            var response = new List<Novidade>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToValue(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}

        public virtual async Task<List<Novidade>> ConsultarPOC()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_NOVIDADE_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Novidade>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueConsultar(reader));
                        }
                    }

                    return response;
                }
            }
        }


        public async Task<Novidade> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_NOVIDADE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    Novidade response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornaDados(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Novidade> ObterPorNomePOC(string descricao)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_NOVIDADE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
                    Novidade response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornaDados(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<List<Novidade>> ObterDadosAtivos(int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_NOVIDADE_ATIVOS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_categoria));
                    var response = new List<Novidade>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosAtivos(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task IncluirPOC(Novidade valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_NOVIDADE_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESC_NOVIDADE", valor.desc_novidade));
                    cmd.Parameters.Add(new SqlParameter("@ORDEM", valor.ordem));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.GarotaCategoria.Categoria.id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", valor.GarotaCategoria.id_garota_categoria));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_01", valor.imagem_01));

                    await sql.OpenAsync();
                    //  await cmd.ExecuteNonQueryAsync();
                    var id = await cmd.ExecuteScalarAsync();
                    valor.id_novidade = (int)id;
                    return;
                }
            }
        }

        public async Task AlterarPOC(Novidade valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_NOVIDADE_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_Novidade", valor.id_novidade));
                    cmd.Parameters.Add(new SqlParameter("@DESC_NOVIDADE", valor.desc_novidade));
                    cmd.Parameters.Add(new SqlParameter("@ORDEM", valor.ordem));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.GarotaCategoria.Categoria.id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", valor.GarotaCategoria.id_garota_categoria));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_01", valor.imagem_01));
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


        private Novidade MapToValueConsultar(SqlDataReader reader)
        {
            return new Novidade()
            {
                id_novidade = (int)reader["id_novidade"],
                desc_novidade = reader["desc_novidade"].ToString(),
                status = (bool)reader["status"],
                imagem_01 = reader["imagem_01"].ToString(),
                ordem = (int)reader["ordem"],
                GarotaCategoria = new GarotaCategoria { Categoria = new Categoria { desc_categoria = reader["desc_categoria"].ToString() }, apelido = reader["apelido"].ToString() },
            };
        }

        private Novidade MapToValueRetornaDados(SqlDataReader reader)
        {
            return new Novidade()
            {
                id_novidade = (int)reader["id_novidade"],
                desc_novidade = reader["desc_novidade"].ToString(),
                status = (bool)reader["status"],
                imagem_01 = reader["imagem_01"].ToString(),
                ordem = (int)reader["ordem"],
                GarotaCategoria = new GarotaCategoria { Categoria = new Categoria { id_categoria = (int)reader["id_categoria"] }, id_garota_categoria = (int)reader["id_garota_categoria"] },
            };
        }

        private Novidade MapToValueRetornaDadosAtivos(SqlDataReader reader)
        {
            return new Novidade()
            {
                id_novidade = (int)reader["id_novidade"],
                desc_novidade = reader["desc_novidade"].ToString(),
                ordem = (int)reader["ordem"],
                status = (bool)reader["status"],
                GarotaCategoria = new GarotaCategoria { Categoria = new Categoria { id_categoria = (int)reader["id_categoria"] }, id_garota_categoria = (int)reader["id_garota_categoria"], apelido = reader["apelido"].ToString(), imagem_01 = reader["imagem_01"].ToString() },
                imagem_01 = reader["imagem_novidade"].ToString()
            };
        }



    }
}
