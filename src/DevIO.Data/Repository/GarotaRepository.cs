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
    public class GarotaRepository : Repository<Garota>, IGarotaRepository
    {

        private readonly string _connectionString;
        public GarotaRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<Garota>> RetornarTodosPOC(int status)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_GAROTA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@STATUS", status));
                    var response = new List<Garota>();
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

        public virtual async Task<List<Garota>> ConsultarPOC(string nome)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOME", nome));
                    var response = new List<Garota>();
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


        public async Task<Garota> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", Id));
                    Garota response = null;
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

        //public async Task<Garota> ObterPorNomePOC(string descricao)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@DESCRICAO", descricao.ToUpper().Trim()));
        //            Garota response = null;
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response = MapToValueRetornaDados(reader);
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}      

        public async Task<Garota> RetornarDadosGarotaSenha(string email, string senha)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {

                string senhaCript = Util.RetornaValorCriptografado(senha);
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_SENHA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                    cmd.Parameters.Add(new SqlParameter("@SENHA", senhaCript));
                    Garota response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornaDadosMenores(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Garota> RetornarDadosGarotaNascimentoSenha(string nascimento, string senha)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {

                string senhaCript = Util.RetornaValorCriptografado(senha);
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_NASCIMENTO_SENHA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NASCIMENTO", nascimento));
                    cmd.Parameters.Add(new SqlParameter("@SENHA", senhaCript));
                    Garota response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornaDadosMenores(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Garota> VerificarCPFGarotaExistente(string cpf)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_VERIFICA_CPF_GAROTA_EXISTENTE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    Garota response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornaDadosMenores(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Garota> RetornarEsqueceuSenha(string cpf, string email)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_GAROTA_ESQUECEU_SENHA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                    Garota response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueEsqueceuSenha(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Garota> RetornarEsqueceuSenhaNascimento(string nascimento, string email)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_GAROTA_ESQUECEU_SENHA_NASCIMENTO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NASCIMENTO", nascimento));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                    Garota response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueEsqueceuSenha(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Garota> RetornarEsqueceuEmail(string cpf)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_GAROTA_ESQUECEU_EMAIL", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));                    
                    Garota response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueEsqueceuEmail(reader);
                        }
                    }

                    return response;
                }
            }
        }

       
        public async Task IncluirPOC(Garota valor)
        {
            string senhaCript = Util.RetornaValorCriptografado(valor.senha);

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOME", valor.nome));
                    cmd.Parameters.Add(new SqlParameter("@CPF", valor.cpf));
                    cmd.Parameters.Add(new SqlParameter("@IDENTIDADE", valor.identidade));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", valor.email));
                    cmd.Parameters.Add(new SqlParameter("@ID_SEXO", valor.id_sexo));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@DT_NASCIMENTO", valor.dt_nascimento));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE1", valor.telefone1));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE2", valor.telefone2));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR1", valor.celular1));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR2", valor.celular2));
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", valor.descricao));
                    cmd.Parameters.Add(new SqlParameter("@SENHA", senhaCript));
                    cmd.Parameters.Add(new SqlParameter("@IP", valor.ip));
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", valor.id_usuario));

                    await sql.OpenAsync();
                    //  await cmd.ExecuteNonQueryAsync();
                    var id = await cmd.ExecuteScalarAsync();
                    valor.id_garota = (int)id;
                    return;
                }
            }
        }

        public async Task AlterarPOC(Garota valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", valor.id_garota));
                    cmd.Parameters.Add(new SqlParameter("@NOME", valor.nome));
                    cmd.Parameters.Add(new SqlParameter("@CPF", valor.cpf));
                    cmd.Parameters.Add(new SqlParameter("@IDENTIDADE", valor.identidade));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", valor.email));
                    cmd.Parameters.Add(new SqlParameter("@ID_SEXO", valor.id_sexo));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@DT_NASCIMENTO", valor.dt_nascimento));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE1", valor.telefone1));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE2", valor.telefone2));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR1", valor.celular1));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR2", valor.celular2));
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", valor.descricao));
                   // cmd.Parameters.Add(new SqlParameter("@SENHA", senhaCript));
                    cmd.Parameters.Add(new SqlParameter("@IP", valor.ip));
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", valor.id_usuario));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task<int> ResetarSenha(int id_garota)
        {
            int fileId;
            string senhaCript = Util.RetornaValorCriptografado("1234abcd");

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_RESETAR_SENHA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", id_garota));
                    cmd.Parameters.Add(new SqlParameter("@SENHA", senhaCript));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }

        public async Task<int> AlterarSenha(int id_garota, string senha)
        {
            int fileId;
            string senhaCript = Util.RetornaValorCriptografado(senha);

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_ALTERAR_SENHA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", id_garota));
                    cmd.Parameters.Add(new SqlParameter("@SENHA", senhaCript));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }

        public async Task<int> AlterarEmail(int id_garota, string email)
        {
            int fileId;           

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_ALTERAR_EMAIL", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", id_garota));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
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


        private Garota MapToValueConsultar(SqlDataReader reader)
        {
            return new Garota()
            {
                id_garota = (int)reader["id_garota"],
                nome = reader["desc_garota"].ToString(),
                status = (bool)reader["status"]
            };
        }

        private Garota MapToValueRetornaDados(SqlDataReader reader)
        {
            return new Garota()
            {
                id_garota = (int)reader["id_garota"],
                nome = reader["nome"].ToString(),
                cpf = reader["cpf"].ToString(),
                identidade = reader["identidade"].ToString(),
                id_sexo = (int)reader["id_sexo"],
                status = (bool)reader["status"],
                dt_nascimento = (DateTime)reader["dt_nascimento"],
                dt_nascimento2 = reader["dt_nascimento"].ToString(),
                telefone1 = reader["telefone1"].ToString(),
                telefone2 = reader["telefone2"].ToString(),
                celular1 = reader["celular1"].ToString(),
                celular2 = reader["celular2"].ToString(),
                descricao = reader["descricao"].ToString(),
                imagem_doc_frente = reader["imagem_doc_frente"].ToString(),
                imagem_doc_tras = reader["imagem_doc_tras"].ToString()
            };
        }

        private Garota MapToValueRetornaDadosMenores(SqlDataReader reader)
        {
            return new Garota()
            {
                id_garota = (int)reader["id_garota"],
                nome = reader["nome"].ToString()
            };
        }

        private Garota MapToValueEsqueceuSenha(SqlDataReader reader)
        {
            return new Garota()
            {
                id_garota = (int)reader["id_garota"],
                nome = reader["nome"].ToString(),
                senha = Util.RetornaValorDescriptografado(reader["senha"].ToString())
            };
        }

        private Garota MapToValueEsqueceuEmail(SqlDataReader reader)
        {
            return new Garota()
            {
                id_garota = (int)reader["id_garota"],
                email = reader["email"].ToString()
            };
        }       

    }
}
