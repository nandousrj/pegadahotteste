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
    public class ParametrosRepository : Repository<Parametros>, IParametrosRepository
    {

        private readonly string _connectionString;
        public ParametrosRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }
                    

        public async Task IncluirPOC(Parametros valor)
        {
            string senhaCript = Util.RetornaValorCriptografado(valor.senha_email_empresa);

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_PARAMETROS_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.Categoria.id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_ALUGUEL", valor.valor_aluguel));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_DESTAQUE", valor.valor_destaque));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_CASAL", valor.valor_casal));
                    cmd.Parameters.Add(new SqlParameter("@NOME_EMPRESA", valor.nome_empresa));
                    cmd.Parameters.Add(new SqlParameter("@APELIDO_EMPRESA", valor.apelido_empresa));
                    cmd.Parameters.Add(new SqlParameter("@CNPJ_EMPRESA", valor.cnpj_empresa));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL_EMPRESA", valor.email_empresa));
                    cmd.Parameters.Add(new SqlParameter("@SENHA_EMAIL_EMPRESA", senhaCript));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE_EMPRESA1", valor.telefone_empresa1));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE_EMPRESA2", valor.telefone_empresa2));
                    cmd.Parameters.Add(new SqlParameter("@IND_PROMOCAO_MES_GRATIS", valor.ind_promocao_mes_gratis));
                    cmd.Parameters.Add(new SqlParameter("@IND_MENSAGEM_CURTIR", valor.ind_mensagem_curtir));
                    cmd.Parameters.Add(new SqlParameter("@MENSAGEM_CURTIR", valor.mensagem_curtir));
                    cmd.Parameters.Add(new SqlParameter("@IND_MENSAGEM_VISUALIZAR", valor.ind_mensagem_visualizar));
                    cmd.Parameters.Add(new SqlParameter("@MENSAGEM_VISUALIZAR", valor.mensagem_visualizar));
                    cmd.Parameters.Add(new SqlParameter("@IND_CURTIR_DIA", valor.ind_curtir_dia));
                    cmd.Parameters.Add(new SqlParameter("@IND_PROMOCAO_CUPOM", valor.ind_promocao_cupom));
                    cmd.Parameters.Add(new SqlParameter("@MENSAGEM_PROMOCAO_CUPOM", valor.mensagem_promocao_cupom));
                    cmd.Parameters.Add(new SqlParameter("@OUTRO_EMAIL", valor.outro_email));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task AlterarPOC(Parametros valor)
        {
            string senhaCript = Util.RetornaValorCriptografado(valor.senha_email_empresa);

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_PARAMETROS_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_PARAMETROS", valor.id_parametros));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.Categoria.id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_ALUGUEL", valor.valor_aluguel));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_DESTAQUE", valor.valor_destaque));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_CASAL", valor.valor_casal));
                    cmd.Parameters.Add(new SqlParameter("@NOME_EMPRESA", valor.nome_empresa));
                    cmd.Parameters.Add(new SqlParameter("@APELIDO_EMPRESA", valor.apelido_empresa));
                    cmd.Parameters.Add(new SqlParameter("@CNPJ_EMPRESA", valor.cnpj_empresa));
                    cmd.Parameters.Add(new SqlParameter("@EMAIL_EMPRESA", valor.email_empresa));
                    cmd.Parameters.Add(new SqlParameter("@SENHA_EMAIL_EMPRESA", senhaCript));
                    cmd.Parameters.Add(new SqlParameter("@ID_BANCO_01", valor.Banco01.id_banco));
                    cmd.Parameters.Add(new SqlParameter("@AGENCIA_01", valor.agencia_01));
                    cmd.Parameters.Add(new SqlParameter("@CONTA_01", valor.conta_01));
                    cmd.Parameters.Add(new SqlParameter("@ID_TIPO_CONTA_01", valor.TipoConta01.id_tipo_conta));
                    cmd.Parameters.Add(new SqlParameter("@ID_BANCO_02", valor.Banco02.id_banco));
                    cmd.Parameters.Add(new SqlParameter("@AGENCIA_02", valor.agencia_02));
                    cmd.Parameters.Add(new SqlParameter("@CONTA_02", valor.conta_02));
                    cmd.Parameters.Add(new SqlParameter("@ID_TIPO_CONTA_02", valor.TipoConta02.id_tipo_conta));
                    cmd.Parameters.Add(new SqlParameter("@IND_PROMOCAO_MES_GRATIS", valor.ind_promocao_mes_gratis));
                    cmd.Parameters.Add(new SqlParameter("@IND_MENSAGEM_CURTIR", valor.ind_mensagem_curtir));
                    cmd.Parameters.Add(new SqlParameter("@MENSAGEM_CURTIR", valor.mensagem_curtir));
                    cmd.Parameters.Add(new SqlParameter("@IND_MENSAGEM_VISUALIZAR", valor.ind_mensagem_visualizar));
                    cmd.Parameters.Add(new SqlParameter("@MENSAGEM_VISUALIZAR", valor.mensagem_visualizar));
                    cmd.Parameters.Add(new SqlParameter("@IND_CURTIR_DIA", valor.ind_curtir_dia));
                    cmd.Parameters.Add(new SqlParameter("@IND_PROMOCAO_CUPOM", valor.ind_promocao_cupom));
                    cmd.Parameters.Add(new SqlParameter("@MENSAGEM_PROMOCAO_CUPOM", valor.mensagem_promocao_cupom));
                    cmd.Parameters.Add(new SqlParameter("@OUTRO_EMAIL", valor.outro_email));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task<Parametros> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_PARAMETROS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    Parametros response = null;
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

        public async Task<Parametros> ObterPorIdExternoPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_PARAMETROS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));
                    Parametros response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueExterno(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<Parametros>> ConsultarPOC(string categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_PARAMETROS_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", categoria));
                    var response = new List<Parametros>();
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


        private Parametros MapToValue(SqlDataReader reader)
        {
            string senhaDesCript = "";
            senhaDesCript = Util.RetornaValorDescriptografado(reader["senha_email_empresa"].ToString());

            return new Parametros()
            {

                Categoria = new Categoria { id_categoria = (int)reader["id_categoria"] },
                valor_aluguel = reader["valor_aluguel"].ToString(),
                valor_destaque = reader["valor_destaque"].ToString(),
                valor_casal = reader["valor_casal"].ToString(),
                nome_empresa = reader["nome_empresa"].ToString(),
                apelido_empresa = reader["apelido_empresa"].ToString(),
                cnpj_empresa = reader["cnpj_empresa"].ToString(),
                email_empresa = reader["email_empresa"].ToString(),
                senha_email_empresa = senhaDesCript,
                Banco01 = new Banco { id_banco = (int)reader["id_banco_01"], desc_banco = reader["desc_banco_01"].ToString(), cod_banco = reader["cod_banco_01"].ToString() },
                agencia_01 = reader["agencia_01"].ToString(),
                conta_01 = reader["conta_01"].ToString(),
                TipoConta01 = new TipoConta { id_tipo_conta = (int)reader["id_tipo_conta_01"], desc_tipo_conta = reader["desc_tipo_conta_01"].ToString(), sigla = reader["sigla_tipo_conta_01"].ToString() },
                Banco02 = new Banco { id_banco = (int)reader["id_banco_02"], desc_banco =  reader["desc_banco_02"].ToString(), cod_banco = reader["cod_banco_02"].ToString() },
                agencia_02 = reader["agencia_02"].ToString(),
                conta_02 = reader["conta_02"].ToString(),
                TipoConta02 = new TipoConta { id_tipo_conta = (int)reader["id_tipo_conta_02"], desc_tipo_conta = reader["desc_tipo_conta_02"].ToString(), sigla = reader["sigla_tipo_conta_02"].ToString() },  
                telefone_empresa1 = reader["telefone_empresa1"].ToString(),
                telefone_empresa2 = reader["telefone_empresa2"].ToString(),
                ind_promocao_mes_gratis = (bool)reader["ind_promocao_mes_gratis"],
                ind_mensagem_curtir = (bool)reader["ind_mensagem_curtir"],
                mensagem_curtir = reader["mensagem_curtir"].ToString(),
                ind_mensagem_visualizar = (bool)reader["ind_mensagem_visualizar"],
                mensagem_visualizar = reader["mensagem_visualizar"].ToString(),
                ind_curtir_dia = (bool)reader["ind_curtir_dia"],
                ind_promocao_cupom = (bool)reader["ind_promocao_cupom"],
                mensagem_promocao_cupom = reader["mensagem_promocao_cupom"].ToString(),
                outro_email = reader["outro_email"].ToString()
            };
        }

        private Parametros MapToValueExterno(SqlDataReader reader)
        {
            string senhaDesCript = "";
            senhaDesCript = Util.RetornaValorDescriptografado(reader["senha_email_empresa"].ToString());

            return new Parametros()
            {

               Categoria = new Categoria { id_categoria = (int)reader["id_categoria"] },
                valor_aluguel = reader["valor_aluguel"].ToString(),
                valor_destaque = reader["valor_destaque"].ToString(),
                valor_casal = reader["valor_casal"].ToString(),
                nome_empresa = reader["nome_empresa"].ToString(),
                apelido_empresa = reader["apelido_empresa"].ToString(),
                cnpj_empresa = reader["cnpj_empresa"].ToString(),
                email_empresa = reader["email_empresa"].ToString(),
                Banco01 = new Banco { id_banco = (int)reader["id_banco_01"], desc_banco = reader["desc_banco_01"].ToString(), cod_banco = reader["cod_banco_01"].ToString() },
                agencia_01 = reader["agencia_01"].ToString(),
                conta_01 = reader["conta_01"].ToString(),
                TipoConta01 = new TipoConta { id_tipo_conta = (int)reader["id_tipo_conta_01"], desc_tipo_conta = reader["desc_tipo_conta_01"].ToString(), sigla = reader["sigla_tipo_conta_01"].ToString() },
                Banco02 = new Banco { id_banco = (int)reader["id_banco_02"], desc_banco = reader["desc_banco_02"].ToString(), cod_banco = reader["cod_banco_02"].ToString() },
                agencia_02 = reader["agencia_02"].ToString(),
                conta_02 = reader["conta_02"].ToString(),
                TipoConta02 = new TipoConta { id_tipo_conta = (int)reader["id_tipo_conta_02"], desc_tipo_conta = reader["desc_tipo_conta_02"].ToString(), sigla = reader["sigla_tipo_conta_02"].ToString() },
                telefone_empresa1 = reader["telefone_empresa1"].ToString(),
                telefone_empresa2 = reader["telefone_empresa2"].ToString(),
                ind_promocao_mes_gratis = (bool)reader["ind_promocao_mes_gratis"],
                ind_mensagem_curtir = (bool)reader["ind_mensagem_curtir"],
                mensagem_curtir = reader["mensagem_curtir"].ToString(),
                ind_mensagem_visualizar = (bool)reader["ind_mensagem_visualizar"],
                mensagem_visualizar = reader["mensagem_visualizar"].ToString(),
                ind_curtir_dia = (bool)reader["ind_curtir_dia"],
                ind_promocao_cupom = (bool)reader["ind_promocao_cupom"],
                mensagem_promocao_cupom = reader["mensagem_promocao_cupom"].ToString(),
                outro_email = reader["outro_email"].ToString()
            };
        }

        private Parametros MapToValueConsultar(SqlDataReader reader)
        {
            string senhaDesCript = "";
            senhaDesCript = Util.RetornaValorDescriptografado(reader["senha_email_empresa"].ToString());

            return new Parametros()
            {
                Categoria = new Categoria { id_categoria = (int)reader["id_categoria"] , desc_categoria = reader["valor_aluguel"].ToString() }
            };
        }

    }
}
