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
    public class PermissoesUsuarioRepository : Repository<PermissoesUsuario>, IPermissoesUsuarioRepository
    {

        private readonly string _connectionString;
        public PermissoesUsuarioRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public async Task<int> VerificarLogin(string login)
        {
            int fileId;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VERIFICAR_LOGIN", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LOGIN", login));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -1;
                    }

                    return fileId;
                }
            }
        }

        public async Task<int> VerificaUsuarioSistema(int id_usuario, int id_sistema)
        {
            int fileId;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VERIFICA_USUARIO_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -1;
                    }

                    return fileId;
                }
            }
        }


        public async Task<PermissoesUsuario> ConsultarUsuario(string login)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CONSULTAR_USUARIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LOGIN", login));
                    PermissoesUsuario response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueConsultarUsuario(reader);
                        }
                    }

                    return response;
                }
            }
        }

        // é também o ConsultarUsuarioId
        public async Task<PermissoesUsuario> RetornarUsuarioId(int id_usuario, int id_sistema)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CONSULTAR_USUARIO_ID", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_usuario));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    PermissoesUsuario response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornarUsuarioId(reader);
                        }
                    }

                    return response;
                }
            }
        }


        //é também o RetornaTodosUsuariosSistema
        public virtual async Task<List<PermissoesUsuario>> RetornarTodos(int id_sistema)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RETORNA_TODOS_USUARIOS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    var response = new List<PermissoesUsuario>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornarTodos(reader));
                        }
                    }

                    return response;
                }
            }
        }


       // é também o RetornaUsuario
        public virtual async Task<List<PermissoesUsuario>> RetornarUsuarioSemTermo(int id_sistema)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RETORNA_USUARIO_SEM_TERMO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    var response = new List<PermissoesUsuario>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornarUsuarioSemTermo(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<PermissoesUsuario> RetornarUsuario(int id_usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RETORNA_USUARIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));                   
                    PermissoesUsuario response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornarUsuario(reader);
                        }
                    }

                    return response;
                }
            }
        }


        // é também o RetornaTodosUsuariosSistema
        public async Task<PermissoesUsuario> RetornarTodosSistema(int id_sistema)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RETORNA_TODOS_USUARIOS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    PermissoesUsuario response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornarTodosSistema(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public virtual async Task<List<PermissoesUsuario>> RetornarUsuarioAcessoPagina(int id_menu)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VERIFICA_USUARIO_ACESSO_PAGINA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_MENU", id_menu));
                    var response = new List<PermissoesUsuario>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornarUsuarioAcessoPagina(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<PermissoesUsuario> ValidarLogin(string login, string senha, int id_sistema)
        {
            //string chave = "";

            //using (SqlConnection sql = new SqlConnection(_connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("RETORNA_CHAVE", sql))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //     //   cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
            //     //   PermissoesUsuario response = null;
            //        await sql.OpenAsync();

            //        using (var reader = await cmd.ExecuteReaderAsync())
            //        {
            //            while (await reader.ReadAsync())
            //            {
            //                chave = reader["chave"].ToString();
            //            }
            //        }                                  
            //    }
            //}

            //Util util = new Util();
            //chave = util.Descriptografar("xvcriptAcesso");

            //using (SqlConnection sql = new SqlConnection(_connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("VERIFICAR_BASE_ACESSO", sql))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd.Parameters.Add(new SqlParameter("@LOGIN", id_sistema));
            //        cmd.Parameters.Add(new SqlParameter("@SENHA", FormsAuthentication.HashPasswordForStoringInConfigFile(usu.Senha + chave, "sha1")));
            //        cmd.Parameters.Add(new SqlParameter("@IP", id_sistema));
            //        PermissoesUsuario response = null;
            //        await sql.OpenAsync();

            //        using (var reader = await cmd.ExecuteReaderAsync())
            //        {
            //            while (await reader.ReadAsync())
            //            {
            //                response = MapToValueRetornarTodosSistema(reader);
            //            }
            //        }

            //        return response;
            //    }
            //}
            
            senha = Util.RetornaValorCriptografado(senha);          

            int qtdTentativa;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VERIFICAR_BASE_ACESSO_API", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LOGIN", login));
                    cmd.Parameters.Add(new SqlParameter("@SENHA_API", senha));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    cmd.Parameters.Add(new SqlParameter("@IP", ""));
                    await sql.OpenAsync();

                    qtdTentativa = Convert.ToInt32(await cmd.ExecuteScalarAsync());                  
                }
            }

            PermissoesUsuario response = null;
            if (qtdTentativa == 0)
            {                
                //TODO - capturar ip
                using (SqlConnection sql = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("LOGIN_USUARIO_API", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@LOGIN", login));
                        cmd.Parameters.Add(new SqlParameter("@SENHA_API", senha));
                        cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                        cmd.Parameters.Add(new SqlParameter("@IP", ""));
                        
                        await sql.OpenAsync();

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response = MapToValueValidarLogin(reader);
                            }
                        }

                        return response;
                    }
                }
            }
            else if (qtdTentativa > 0)
            {
                response = new PermissoesUsuario();
                response.quantidade_tentativa = qtdTentativa;
            }
            else
            {
                response = new PermissoesUsuario();
                response.quantidade_tentativa = -10;
            }

            return response;

        }

        //TODO - capturar ip
        // mesmo que o AutorizarLogin
        public async Task AtivarUsuario(int id_usuario_acao, int id_usuario, string ip, int id_sistema)
        {          
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ATIVAR_USUARIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO_ACAO", id_usuario_acao));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    cmd.Parameters.Add(new SqlParameter("@IP", ""));
                    await sql.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();
                   
                }
            }
        }


        public async Task<int> VerificarTrocaSenha(int id_usuario)
        {
            int fileId;           

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VERIFICAR_TROCA_SENHA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));    
                    await sql.OpenAsync();

                    fileId = 1;// Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -1;
                    }

                    return fileId;
                }
            }
        }

       // também o VerificaSistemaUsuario
        public async Task<int> VerificarUsuarioSistema(int id_usuario, int id_sistema)
        {
            int fileId;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("VERIFICA_USUARIO_SISTEMA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -10;
                    }

                    return fileId;
                }
            }
        }

        //TODO - fazer o processo IP
        // Também o CadastrarUsuario
        public async Task<int> IncluirPOC(PermissoesUsuario valor)
        {
            int fileId;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CADASTRAR_USUARIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", valor.id_usuario));
                    cmd.Parameters.Add(new SqlParameter("@NOME", valor.nome));
                    cmd.Parameters.Add(new SqlParameter("@LOGIN", valor.login));
                    cmd.Parameters.Add(new SqlParameter("@SENHA_API", valor.senha_api));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", valor.email));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE", valor.telefone));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR", valor.celular));
                    cmd.Parameters.Add(new SqlParameter("@ID_SETOR", valor.id_setor));
                    cmd.Parameters.Add(new SqlParameter("@MATRICULA", valor.matricula));
                    cmd.Parameters.Add(new SqlParameter("@EXPIRA_SENHA", valor.expira_senha));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", valor.id_sistema));
                    cmd.Parameters.Add(new SqlParameter("@ID_PERFIL", valor.id_perfil));
                    cmd.Parameters.Add(new SqlParameter("@RESPONSAVEL", valor.responsavel));
                    cmd.Parameters.Add(new SqlParameter("@LOGIN_ATUAL", valor.login_atual));
                    cmd.Parameters.Add(new SqlParameter("@IP", ""));
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO_ACAO", valor.id_usuario_acao));                    

                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -10;
                    }

                    return fileId;
                }
            }
        }

        //TODO - fazer o processo IP
        public async Task<int> AlterarPOC(PermissoesUsuario valor)
        {
            int fileId;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ALTERAR_USUARIO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", valor.id_usuario));
                    cmd.Parameters.Add(new SqlParameter("@NOME", valor.nome));
                    cmd.Parameters.Add(new SqlParameter("@LOGIN", valor.login));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL", valor.email));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE", valor.telefone));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR", valor.celular));
                    cmd.Parameters.Add(new SqlParameter("@ID_SETOR", valor.id_setor));
                    cmd.Parameters.Add(new SqlParameter("@MATRICULA", valor.matricula));
                    cmd.Parameters.Add(new SqlParameter("@RESPONSAVEL", valor.responsavel));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", valor.id_sistema));
                    cmd.Parameters.Add(new SqlParameter("@ID_PERFIL", valor.id_perfil));       
                    cmd.Parameters.Add(new SqlParameter("@IP", ""));
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO_ACAO", valor.id_usuario_acao));

                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -1;
                    }

                    return fileId;
                }
            }
        }

        // TODO - colocar ip
        public async Task<int> AlterarSenha(PermissoesUsuario valor)
        {
            int fileId;
            valor.expira_senha = DateTime.Now.AddMonths(3);

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ALTERAR_SENHA_API", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", valor.id_usuario));
                    cmd.Parameters.Add(new SqlParameter("@SENHA_ATUAL", valor.senha_atual));
                    cmd.Parameters.Add(new SqlParameter("@SENHA_API", valor.senha_api));
                    cmd.Parameters.Add(new SqlParameter("@EXPIRA_SENHA", valor.expira_senha));
                    cmd.Parameters.Add(new SqlParameter("@IP", ""));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", valor.id_sistema));

                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -10;
                    }

                    return fileId;
                }
            }
        }

        // TODO - colocar ip
        // mesmo que o ResetSenha
        public async Task<int> ResetarSenha(PermissoesUsuario valor)
        {
            int fileId;
            
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("RESETAR_SENHA_API", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LOGIN", valor.login));
                    cmd.Parameters.Add(new SqlParameter("@SENHA", valor.senha_atual));                    
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", valor.id_usuario));                   
                    cmd.Parameters.Add(new SqlParameter("@IP", ""));
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", valor.id_sistema));

                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (fileId.ToString() == null)
                    {
                        fileId = -10;
                    }

                    return fileId;
                }
            }
        }

        public async Task GravarAceiteTermo(int id_usuario, int id_sistema)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INCLUIR_ACEITE_TERMO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));                  
                    cmd.Parameters.Add(new SqlParameter("@ID_SISTEMA", id_sistema));
                    await sql.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        //todo lixo
        //public async Task<string> RetornarCripto(string valor)
        //{
        //    PermissoesUsuario response = null;
        //    string dado;
        //    bi

        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("RETORNA_CRIPTO", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@SENHA_API", valor));
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response = MapToValueCripto(reader);
        //                }
        //            }


        //            dado = response.senha.ToString();

        //            return dado;
        //        }
        //    }
        //}


        //    VerificaPermissao  
        //       TestaLogin          

        //TODO: RetornaMenu está no PerfilPermissoesMenu



        private PermissoesUsuario MapToValueConsultarUsuario(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {
                id_usuario = (int)reader["id_usuario"],
                nome = reader["nome"].ToString(),
                login = reader["login"].ToString(),
                telefone = reader["telefone"].ToString(),
                email = reader["email"].ToString(),
                matricula = reader["matricula"].ToString(),
                celular = reader["celular"].ToString(),
                expira_senha = (DateTime)reader["expira_senha"],
                status = (bool)reader["status"],
                ultimo_acesso = (DateTime)reader["ultimo_acesso"],
                id_instituicao = (int)reader["id_instituicao"],
                id_setor = (int)reader["id_setor"]
            };
        }

        private PermissoesUsuario MapToValueRetornarUsuarioId(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {
                id_usuario = (int)reader["id_usuario"],
                nome = reader["nome"].ToString(),
                login = reader["login"].ToString(),
                status = (bool)reader["status"],
                email = reader["email"].ToString(),
                telefone = reader["telefone"].ToString(),
                celular = reader["celular"].ToString(),
                Setor = new PermissoesSetor { id_setor = (int)reader["id_setor"], nome = reader["nm_setor"].ToString(), sala = reader["sala"].ToString(), andar = reader["andar"].ToString() },
                matricula = reader["matricula"].ToString(),
                id_instituicao = (int)reader["id_instituicao"],
                id_perfil = (int)reader["id_perfil"],
                responsavel = (bool)reader["responsavel"],
                status_termo = (bool)reader["status_termo"],
                quantidade_tentativa = (int)reader["erro_senha"],
                desc_sigla_instituicao = reader["sigla"].ToString(),
                desc_instituicao = reader["nm_instituicao"].ToString(),
                desc_setor = reader["nm_setor"].ToString(),
                desc_perfil = reader["nm_perfil"].ToString()
            };
        }

        //todo lixo
        //private PermissoesUsuario MapToValueCripto(SqlDataReader reader)
        //{
        //    return new PermissoesUsuario()
        //    {                
        //        senha = reader["senha"].ToString()
        //    };
        //}

        private PermissoesUsuario MapToValueRetornarTodos(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {
                id_usuario = (int)reader["id_usuario"],
                nome = reader["nome"].ToString()
            };
        }

        private PermissoesUsuario MapToValueRetornarUsuarioSemTermo(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {
                id_usuario = (int)reader["id_usuario"],
                login = reader["login"].ToString()
            };
        }


        private PermissoesUsuario MapToValueRetornarUsuario(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {
                login = reader["login"].ToString(),
                email = reader["email"].ToString(),
                telefone = reader["telefone"].ToString(),
                celular = reader["celular"].ToString(),
                nome = reader["nome"].ToString(),
                id_usuario = (int)reader["id_usuario"],
                matricula = reader["matricula"].ToString()
            };
        }

        private PermissoesUsuario MapToValueRetornarTodosSistema(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {

                id_usuario = (int)reader["id_usuario"],
                nome = reader["nome"].ToString(),
                login = reader["login"].ToString(),
                desc_sigla_instituicao = reader["sigla"].ToString(),
                desc_setor = reader["setor"].ToString(),
                quantidade_tentativa = (int)reader["erro_senha"],
                status = (bool)reader["status"],
                status_termo = (bool)reader["status_termo"]
            };
        }

        private PermissoesUsuario MapToValueRetornarUsuarioAcessoPagina(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {
                id_perfil = (int)reader["id_perfil"]
            };
        }

        private PermissoesUsuario MapToValueValidarLogin(SqlDataReader reader)
        {
            return new PermissoesUsuario()
            {

                id_usuario = (int)reader["id_usuario"],
                login = reader["login"].ToString(),
                email = reader["email"].ToString(),
                id_perfil = (int)reader["id_perfil"],
                expira_senha = (DateTime)reader["expira_senha"]
      //      expira_senha = (DateTime)reader["expira_senha"].ToString("dd/MM/yyyy")
       //         expira_senha = Convert.ToDateTime(reader["expira_senha"]).ToString("dd/MM/yyyy")
            //id_instituicao = (int)reader["id_instituicao"],
            //quantidade_tentativa = (int)reader["erro_senha"],
            //status = (bool)reader["status"],
            //status_termo = (bool)reader["status_termo"],
            //bloqueio = (bool)reader["bloqueio_acesso"]
        };
        }      

    }
}
