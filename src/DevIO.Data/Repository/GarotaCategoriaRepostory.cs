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
    public class GarotaCategoriaRepository : Repository<GarotaCategoria>, IGarotaCategoriaRepository
    {

        private readonly string _connectionString;
        public GarotaCategoriaRepository(MeuDbContext context, IConfiguration configuration) : base(context) { _connectionString = configuration.GetConnectionString("defaultConnection"); }


        public virtual async Task<List<GarotaCategoria>> ConsultarPOC(string nome, string apelido, string cpf)
        {

            if (nome == null) { nome = ""; };
            if (apelido == null) { apelido = ""; };
            if (cpf == null) { cpf = ""; };

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_CATEGORIA_CONSULTAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NOME", nome));
                    cmd.Parameters.Add(new SqlParameter("@APELIDO", apelido));
                    cmd.Parameters.Add(new SqlParameter("@CPF", cpf));
                    var response = new List<GarotaCategoria>();
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

        public async Task<GarotaCategoria> ObterPorIdPOC(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", Id));
                    GarotaCategoria response = null;
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


        public async Task<GarotaCategoria> RetornarDadosGarotaCategoriaUsuario(int id_categoria, string desc_zona, int destaque)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {                
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_PRINCIPAL", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@DESC_ZONA", desc_zona));
                    cmd.Parameters.Add(new SqlParameter("@IND_DESTAQUE", destaque));
                    GarotaCategoria response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornaDadosGarotaCategoriaUsuario(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosGarotaCategoriaUsuarioPerfil(int id_usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_USUARIO_PERFIL", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosGarotaCategoriaMenor(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosCategoriaUsuarioPerfil(int id_usuario)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_CATEGORIA_USUARIO_PERFIL", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_USUARIO", id_usuario));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosGarotaCategoriaMenor(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosGarotaCategoriaLogada(int id_garota)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_LOGADA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", id_garota));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosGarotaCategoriaMenor(reader));
                        }
                    }

                    return response;
                }
            }
        }


        public virtual async Task<List<GarotaCategoria>> RetornarDadosPrincipal(int id_categoria, string desc_zona, int ind_destaque, string apelido, int id_estilo)
        {
            if (desc_zona == null) { desc_zona = ""; };
            if (apelido == null) { apelido = ""; };
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_PRINCIPAL", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@DESC_ZONA", desc_zona));
                    cmd.Parameters.Add(new SqlParameter("@IND_DESTAQUE", ind_destaque));
                    cmd.Parameters.Add(new SqlParameter("@APELIDO", apelido));
                    cmd.Parameters.Add(new SqlParameter("@ID_ESTILO", id_estilo));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosPrincipal(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosGrupo(int id_grupo)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_GRUPO", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_grupo));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosGrupo(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosVisualizadas(int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_RANKING_VISUALIZADAS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_categoria));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosVisualizados(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosVisualizadasTodos(int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_RANKING_VISUALIZADAS_TODOS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_categoria));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosVisualizados(reader));
                        }
                    }

                    return response;
                }
            }
        }


        public virtual async Task<List<GarotaCategoria>> RetornarDadosCurtidas(int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_RANKING_CURTIDAS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_categoria));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosCurtidas(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosCurtidasTodos(int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_RANKING_CURTIDAS_TODOS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", id_categoria));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosCurtidas(reader));
                        }
                    }

                    return response;
                }
            }
        }
        
        //todo:
        public virtual async Task<List<GarotaCategoria>> RetornarDadosCurtidasVisualizadasTotal(int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_VISUALIZADAS_CURTIDAS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_categoria));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosCurtidas(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public virtual async Task<List<GarotaCategoria>> RetornarDadosCurtidasVisualizadasTotalTodos(int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_VISUALIZADAS_CURTIDAS_TODOS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", id_categoria));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaDadosCurtidas(reader));
                        }
                    }

                    return response;
                }
            }
        }


        public async Task<GarotaCategoria> RetornarDadosDetalhe(int id_garota_categoria, int id_categoria)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {               
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_DETALHE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", id_garota_categoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", id_categoria));
                    GarotaCategoria response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValueRetornaDadosDetalhe(reader);
                        }
                    }

                    return response;
                }
            }
        }


        public virtual async Task<List<GarotaCategoria>> RelatorioAluguel()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RELATORIO_ALUGUEL", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                  
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRelatorioAluguel(reader));
                        }
                    }

                    return response;
                }
            }
        }


        public virtual async Task<List<GarotaCategoria>> ConsultarGarotaPorId(int id_garota)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_CATEGORIA_CONSULTAR_ID_GAROTA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", id_garota));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueConsultarGarotaPorId(reader));
                        }
                    }

                    return response;
                }
            }
        }


        public virtual async Task<List<GarotaCategoria>> RetornarTodosPOC(int status)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_GAROTA_CATEGORIA", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@STATUS", status));
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValueRetornaTodos(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<int> AtualizarPromocaoMesGratis()
        {
            int fileId;            

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_CATEGORIA_PROMOCAO_MES_GRATIS_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }


        //public virtual async Task<List<GarotaCategoriaAtende>> RetornarGarotaCategoriaAtende(int id_garota_categoria)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_GAROTA_CATEGORIA_ATENDE", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", id_garota_categoria));
        //            var response = new List<GarotaCategoriaAtende>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToValueRetornaDadosGarotaCategoriaAtende(reader));
        //                }
        //            }                    

        //            return response;
        //        }
        //    }
        //}

        //public virtual async Task<List<GarotaCategoriaIdioma>> RetornarGarotaCategoriaIdioma(int id_garota_categoria)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("AG_RETORNA_TODOS_GAROTA_CATEGORIA_IDIOMA", sql))
        //        {
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", id_garota_categoria));
        //            var response = new List<GarotaCategoriaIdioma>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToValueRetornaDadosGarotaCategoriaIdioma(reader));
        //                }
        //            }

        //            return response;
        //        }
        //    }
        //}

        public virtual async Task<List<GarotaCategoria>> RetornaCupomAtivo()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_RETORNA_DADOS_GAROTA_CATEGORIA_CUPOM", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<GarotaCategoria>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValuRetornaCupomAtivo(reader));
                        }
                    }

                    return response;
                }
            }
        }


        public async Task IncluirPOC(GarotaCategoria valor)
        {          
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_CATEGORIA_INCLUIR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", valor.id_garota));
                    cmd.Parameters.Add(new SqlParameter("@APELIDO", valor.apelido));
                    cmd.Parameters.Add(new SqlParameter("@IDADE", valor.idade));
                    cmd.Parameters.Add(new SqlParameter("@ID_SEXO", valor.id_sexo));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@IND_DESTAQUE", valor.ind_destaque));
                    cmd.Parameters.Add(new SqlParameter("@ALTURA", valor.telefone1));
                    cmd.Parameters.Add(new SqlParameter("@MANEQUIM", valor.manequim));
                    cmd.Parameters.Add(new SqlParameter("@ID_ZONA", valor.id_zona));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE1", valor.telefone1));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE2", valor.telefone2));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR1", valor.celular1));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR2", valor.celular2));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR3", valor.celular3));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR4", valor.celular4));
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", valor.descricao));
                    cmd.Parameters.Add(new SqlParameter("@OBSERVACAO", valor.observacao));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_01", valor.imagem_01));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_02", valor.imagem_02));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_03", valor.imagem_03));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_04", valor.imagem_04));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_05", valor.imagem_05));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_06", valor.imagem_06));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_07", valor.imagem_07));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_08", valor.imagem_08));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_09", valor.imagem_09));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_10", valor.imagem_10));
                    cmd.Parameters.Add(new SqlParameter("@ID_ESTILO", valor.id_estilo));
                    cmd.Parameters.Add(new SqlParameter("@ID_GRUPO", valor.id_grupo));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_ALUGUEL", valor.valor_aluguel));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_DESTAQUE", valor.valor_destaque));
                    cmd.Parameters.Add(new SqlParameter("@DESCONTO", valor.desconto));
                    cmd.Parameters.Add(new SqlParameter("@ID_BAIRRO", valor.id_bairro));
                    cmd.Parameters.Add(new SqlParameter("@ATENDIMENTO", valor.atendimento));
                    cmd.Parameters.Add(new SqlParameter("@IND_PROMOCAO_MES_GRATIS", valor.ind_promocao_mes_gratis));
                    cmd.Parameters.Add(new SqlParameter("@ID_OLHOS", valor.id_olhos));
                    cmd.Parameters.Add(new SqlParameter("@ID_TIPO_ANUNCIO", valor.id_tipo_anuncio));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_01", valor.link_video_01));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_02", valor.link_video_02));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_03", valor.link_video_03));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_04", valor.link_video_04));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_05", valor.link_video_05));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_01", valor.link_site_01));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_02", valor.link_site_02));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_03", valor.link_site_03));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_04", valor.link_site_04));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_05", valor.link_site_05));
                    cmd.Parameters.Add(new SqlParameter("@IND_PRIVE", valor.ind_prive));
                    cmd.Parameters.Add(new SqlParameter("@DESC_ANAL", valor.desc_anal));
                    cmd.Parameters.Add(new SqlParameter("@DESC_VIAGEM", valor.desc_viagem));
                    cmd.Parameters.Add(new SqlParameter("@DESC_ORAL_SEM_CAMISINHA", valor.desc_oral_sem_camisinha));
                    cmd.Parameters.Add(new SqlParameter("@DESC_DUPLA_PENETRACAO", valor.desc_dupla_penetracao));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_BANNER", valor.imagem_banner));
                    cmd.Parameters.Add(new SqlParameter("@IND_CARTAO", valor.ind_cartao));
                    cmd.Parameters.Add(new SqlParameter("@IND_FIM_DE_SEMANA", valor.ind_fim_de_semana));
                    cmd.Parameters.Add(new SqlParameter("@IND_CUPOM", valor.ind_cupom));
                    cmd.Parameters.Add(new SqlParameter("@PCT_CUPOM", valor.pct_cupom));

                    await sql.OpenAsync();
                    //  await cmd.ExecuteNonQueryAsync();
                    var id = await cmd.ExecuteScalarAsync();
                    valor.id_garota = (int)id;
                    return;
                }
            }
        }

        public async Task AlterarPOC(GarotaCategoria valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_GAROTA_CATEGORIA_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", valor.id_garota_categoria));                    
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA", valor.id_garota));
                    cmd.Parameters.Add(new SqlParameter("@APELIDO", valor.apelido));
                    cmd.Parameters.Add(new SqlParameter("@IDADE", valor.idade));
                    cmd.Parameters.Add(new SqlParameter("@ID_SEXO", valor.id_sexo));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", valor.id_categoria));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", valor.status));
                    cmd.Parameters.Add(new SqlParameter("@IND_DESTAQUE", valor.ind_destaque));
                    cmd.Parameters.Add(new SqlParameter("@ALTURA", valor.telefone1));
                    cmd.Parameters.Add(new SqlParameter("@MANEQUIM", valor.manequim));
                    cmd.Parameters.Add(new SqlParameter("@ID_ZONA", valor.id_zona));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE1", valor.telefone1));
                    cmd.Parameters.Add(new SqlParameter("@TELEFONE2", valor.telefone2));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR1", valor.celular1));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR2", valor.celular2));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR3", valor.celular3));
                    cmd.Parameters.Add(new SqlParameter("@CELULAR4", valor.celular4));
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO", valor.descricao));
                    cmd.Parameters.Add(new SqlParameter("@OBSERVACAO", valor.observacao));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_01", valor.imagem_01));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_02", valor.imagem_02));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_03", valor.imagem_03));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_04", valor.imagem_04));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_05", valor.imagem_05));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_06", valor.imagem_06));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_07", valor.imagem_07));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_08", valor.imagem_08));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_09", valor.imagem_09));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_10", valor.imagem_10));
                    cmd.Parameters.Add(new SqlParameter("@ID_ESTILO", valor.id_estilo));
                    cmd.Parameters.Add(new SqlParameter("@ID_GRUPO", valor.id_grupo));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_ALUGUEL", valor.valor_aluguel));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_DESTAQUE", valor.valor_destaque));
                    cmd.Parameters.Add(new SqlParameter("@DESCONTO", valor.desconto));
                    cmd.Parameters.Add(new SqlParameter("@ID_BAIRRO", valor.id_bairro));
                    cmd.Parameters.Add(new SqlParameter("@ATENDIMENTO", valor.atendimento));
                    cmd.Parameters.Add(new SqlParameter("@IND_PROMOCAO_MES_GRATIS", valor.ind_promocao_mes_gratis));
                    cmd.Parameters.Add(new SqlParameter("@ID_OLHOS", valor.id_olhos));
                    cmd.Parameters.Add(new SqlParameter("@ID_TIPO_ANUNCIO", valor.id_tipo_anuncio));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_01", valor.link_video_01));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_02", valor.link_video_02));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_03", valor.link_video_03));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_04", valor.link_video_04));
                    cmd.Parameters.Add(new SqlParameter("@LINK_VIDEO_05", valor.link_video_05));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_01", valor.link_site_01));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_02", valor.link_site_02));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_03", valor.link_site_03));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_04", valor.link_site_04));
                    cmd.Parameters.Add(new SqlParameter("@LINK_SITE_05", valor.link_site_05));
                    cmd.Parameters.Add(new SqlParameter("@IND_PRIVE", valor.ind_prive));
                    cmd.Parameters.Add(new SqlParameter("@DESC_ANAL", valor.desc_anal));
                    cmd.Parameters.Add(new SqlParameter("@DESC_VIAGEM", valor.desc_viagem));
                    cmd.Parameters.Add(new SqlParameter("@DESC_ORAL_SEM_CAMISINHA", valor.desc_oral_sem_camisinha));
                    cmd.Parameters.Add(new SqlParameter("@DESC_DUPLA_PENETRACAO", valor.desc_dupla_penetracao));
                    cmd.Parameters.Add(new SqlParameter("@IMAGEM_BANNER", valor.imagem_banner));
                    cmd.Parameters.Add(new SqlParameter("@IND_CARTAO", valor.ind_cartao));
                    cmd.Parameters.Add(new SqlParameter("@IND_FIM_DE_SEMANA", valor.ind_fim_de_semana));
                    cmd.Parameters.Add(new SqlParameter("@IND_CUPOM", valor.ind_cupom));
                    cmd.Parameters.Add(new SqlParameter("@PCT_CUPOM", valor.pct_cupom));
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

        public async Task<int> AtualizarVisualizacaoGarotaCategoria(int id_garota_categoria, int id_categoria)
        {
            int fileId;

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AG_VISUALIZACAO_ALTERAR", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID_GAROTA_CATEGORIA", id_garota_categoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA", id_categoria));
                    await sql.OpenAsync();

                    fileId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return fileId;
                }
            }
        }


        private GarotaCategoria MapToValueConsultar(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria= (int)reader["id_garota"],
                Garota = new Garota
                {
                    id_garota = (int)reader["id_garota"],
                    nome = reader["nome"].ToString(),
                    cpf = reader["cpf"].ToString(),
                    status = (bool)reader["status_garota"]
                },
                id_garota = (int)reader["id_garota"],
                apelido = reader["apelido"].ToString(),
                status = (bool)reader["status"],
                Categoria = new Categoria { desc_categoria = reader["desc_categoria"].ToString() },
                ind_promocao_mes_gratis = (bool)reader["ind_promocao_mes_gratis"]

            };
        }

        private GarotaCategoria MapToValueRetornaDados(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                Garota = new Garota { id_garota = (int)reader["id_garota"], cpf = reader["cpf"].ToString(), status = (bool)reader["status"], dt_nascimento = (DateTime)reader["dt_nascimento"], dt_nascimento2 = reader["dt_nascimento"].ToString()},
                id_sexo = (int)reader["id_sexo"],
                apelido = reader["apelido"].ToString(),
                idade = (int)reader["idade"],
                Categoria = new Categoria { id_categoria = (int)reader["id_categoria"], desc_categoria = reader["desc_categoria"].ToString() },
                id_categoria = (int)reader["id_categoria"],
               // Estilo = new Estilo { id_estilo = (int)reader["id_estilo"], desc_estilo = reader["desc_estilo"].ToString() },
                id_estilo = (int)reader["id_estilo"],
                status = (bool)reader["status"],
                ind_destaque = (bool)reader["ind_destaque"],
                altura = reader["altura"].ToString(),
                manequim = reader["manequim"].ToString(),                
                id_zona = (int)reader["id_zona"],
                telefone1 = reader["telefone1"].ToString(),
                telefone2 = reader["telefone2"].ToString(),
                celular1 = reader["celular1"].ToString(),
                celular2 = reader["celular2"].ToString(),
                celular3 = reader["celular3"].ToString(),
                celular4 = reader["celular4"].ToString(),
                descricao = reader["descricao"].ToString(),
                observacao = reader["observacao"].ToString(),
                imagem_01 = reader["imagem_01"].ToString(),
                imagem_02 = reader["imagem_02"].ToString(),
                imagem_03 = reader["imagem_03"].ToString(),
                imagem_04 = reader["imagem_04"].ToString(),
                imagem_05 = reader["imagem_05"].ToString(),
                imagem_06 = reader["imagem_06"].ToString(),
                imagem_07 = reader["imagem_07"].ToString(),
                imagem_08 = reader["imagem_08"].ToString(),
                imagem_09 = reader["imagem_09"].ToString(),
                imagem_10 = reader["imagem_10"].ToString(),
                id_grupo = (int)reader["id_grupo"],
                valor_aluguel = (decimal)reader["valor_aluguel"],
                valor_destaque = (decimal)reader["valor_destaque"],
                desconto = (int)reader["desconto"],
                total = (decimal)reader["total"],
                id_bairro = (int)reader["id_bairro"],
                atendimento = reader["atendimento"].ToString(),
                ind_promocao_mes_gratis = (bool)reader["ind_promocao_mes_gratis"],
                id_olhos = (int)reader["id_olhos"],
                id_tipo_anuncio = (int)reader["id_tipo_anuncio"],
                link_video_01 = reader["link_video_01"].ToString(),
                link_video_02 = reader["link_video_02"].ToString(),
                link_video_03 = reader["link_video_03"].ToString(),
                link_video_04 = reader["link_video_04"].ToString(),
                link_video_05 = reader["link_video_05"].ToString(),
                link_site_01 = reader["link_site_01"].ToString(),
                link_site_02 = reader["link_site_02"].ToString(),
                link_site_03 = reader["link_site_03"].ToString(),
                link_site_04 = reader["link_site_04"].ToString(),
                link_site_05 = reader["link_site_05"].ToString(),
                ind_prive = (bool)reader["ind_prive"],
                desc_anal = reader["desc_anal"].ToString(),
                desc_viagem = reader["desc_viagem"].ToString(),
                desc_oral_sem_camisinha = reader["desc_oral_sem_camisinha"].ToString(),
                desc_dupla_penetracao = reader["desc_dupla_penetracao"].ToString(),
                imagem_banner = reader["imagem_banner"].ToString(),
                ind_cartao = (bool)reader["ind_cartao"],
                ind_fim_de_semana = (bool)reader["ind_fim_de_semana"],
                ind_cupom = (bool)reader["ind_cupom"],
                pct_cupom = (int)reader["pct_cupom"]
            };
        }


        private GarotaCategoria MapToValueRetornaDadosGarotaCategoriaUsuario(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                imagem_01 = reader["imagem_01"].ToString(),
                atendimento = reader["atendimento"].ToString(),
                Bairro = new Bairro { desc_bairro = reader["desc_bairro"].ToString() }
            };
        }

        private GarotaCategoria MapToValueRetornaDadosGarotaCategoriaMenor(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString()
            };
        }

        private GarotaCategoria MapToValueRetornaDadosPrincipal(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                imagem_01 = reader["imagem_01"].ToString(),
                atendimento = reader["atendimento"].ToString(),
                Bairro = new Bairro { desc_bairro = reader["desc_bairro"].ToString() },
                link_site_01 = reader["link_site_01"].ToString(),
                link_video_01 = reader["link_video_01"].ToString()
            };
        }

        private GarotaCategoria MapToValueRetornaDadosGrupo(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                imagem_01 = reader["imagem_01"].ToString(),
                atendimento = reader["atendimento"].ToString(),
                Bairro = new Bairro { desc_bairro = reader["desc_bairro"].ToString() }
            };
        }

        private GarotaCategoria MapToValueRetornaDadosVisualizados(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                imagem_01 = reader["imagem_01"].ToString(),
                atendimento = reader["atendimento"].ToString(),
                Bairro = new Bairro { desc_bairro = reader["desc_bairro"].ToString() },
                qtd_visualizacao = (int)reader["qtd_visualizacao"]
            };
        }

        private GarotaCategoria MapToValueRetornaDadosCurtidas(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                imagem_01 = reader["imagem_01"].ToString(),
                atendimento = reader["atendimento"].ToString(),
                Bairro = new Bairro { desc_bairro = reader["desc_bairro"].ToString() },
                quantidade2 = reader["qtd_critica"].ToString()
            };
        }

        
        private GarotaCategoria MapToValueRetornaDadosDetalhe(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                Garota = new Garota { id_garota = (int)reader["id_garota"], nome = reader["nome"].ToString(), status = (bool)reader["status_garota"]/*, dt_nascimento = (DateTime)reader["dt_nascimento"], dt_nascimento2 = reader["dt_nascimento"].ToString() */},
                //id_sexo = (int)reader["id_sexo"]//,
                id_garota = (int)reader["id_garota"],
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                idade = (int)reader["idade"],
                Categoria = new Categoria { id_categoria = (int)reader["id_categoria"]/*, desc_categoria = reader["desc_categoria"].ToString()*/ },
                id_categoria = (int)reader["id_categoria"],
                // //-- Estilo = new Estilo { id_estilo = (int)reader["id_estilo"], desc_estilo = reader["desc_estilo"].ToString() },
                // //--    id_estilo = (int)reader["id_estilo"],
                status = (bool)reader["status"],
                //   ind_destaque = (bool)reader["ind_destaque"]//,
                altura = reader["altura"].ToString(),
                manequim = reader["manequim"].ToString(),
                telefone1 = reader["telefone1"].ToString(),
                telefone2 = reader["telefone2"].ToString(),
                celular1 = reader["celular1"].ToString(),
                celular2 = reader["celular2"].ToString(),
                celular3 = reader["celular3"].ToString(),
                celular4 = reader["celular4"].ToString(),
                descricao = reader["descricao"].ToString(),
                observacao = reader["observacao"].ToString(),
                imagem_01 = reader["imagem_01"].ToString(),
                imagem_02 = reader["imagem_02"].ToString(),
                imagem_03 = reader["imagem_03"].ToString(),
                imagem_04 = reader["imagem_04"].ToString(),
                imagem_05 = reader["imagem_05"].ToString(),
                imagem_06 = reader["imagem_06"].ToString(),
                imagem_07 = reader["imagem_07"].ToString(),
                imagem_08 = reader["imagem_08"].ToString(),
                imagem_09 = reader["imagem_09"].ToString(),
                imagem_10 = reader["imagem_10"].ToString(),
                Bairro = new Bairro { desc_bairro = reader["desc_bairro"].ToString() },
                Zona = new Zona { desc_zona = reader["desc_zona"].ToString() },
                TipoAnuncio = new TipoAnuncio { id_tipo_anuncio = (int)reader["id_tipo_anuncio"], desc_tipo_anuncio = reader["desc_tipo_anuncio"].ToString() },
                Olhos = new Olhos { id_olhos = (int)reader["id_olhos"], desc_olhos = reader["desc_olhos"].ToString() },
                Sexo = new Sexo { desc_sexo = reader["desc_sexo"].ToString() },
                qtd_visualizacao = (int)reader["qtd_visualizacao"],
                qtd_curticao = (int)reader["qtd_curticao"],
                //  Critica = new Critica { ind_curtir = (bool)reader["ind_curtir"], desc_olhos = reader["desc_olhos"].ToString() },
                //valor_aluguel = (decimal)reader["valor_aluguel"],
                //valor_destaque = (decimal)reader["valor_destaque"],
                //    desconto = (int)reader["desconto"],
                //    total = (decimal)reader["total"],
                //    id_bairro = (int)reader["id_bairro"],
                atendimento = reader["atendimento"].ToString(),
                // ind_promocao_mes_gratis = (bool)reader["ind_promocao_mes_gratis"],
                link_video_01 = reader["link_video_01"].ToString(),
                link_video_02 = reader["link_video_02"].ToString(),
                link_video_03 = reader["link_video_03"].ToString(),
                link_video_04 = reader["link_video_04"].ToString(),
                link_video_05 = reader["link_video_05"].ToString(),
                link_site_01 = reader["link_site_01"].ToString(),
                link_site_02 = reader["link_site_02"].ToString(),
                link_site_03 = reader["link_site_03"].ToString(),
                link_site_04 = reader["link_site_04"].ToString(),
                link_site_05 = reader["link_site_05"].ToString(),
                ind_prive = (bool)reader["ind_prive"],
                desc_anal = reader["desc_anal"].ToString(),
                desc_viagem = reader["desc_viagem"].ToString(),
                desc_oral_sem_camisinha = reader["desc_oral_sem_camisinha"].ToString(),
                desc_dupla_penetracao = reader["desc_dupla_penetracao"].ToString(),
                imagem_banner = reader["imagem_banner"].ToString(),
                ind_cartao = (bool)reader["ind_cartao"],
                ind_fim_de_semana = (bool)reader["ind_fim_de_semana"],
                ind_cupom = (bool)reader["ind_cupom"],
                pct_cupom = (int)reader["pct_cupom"]
            };
        }

        private GarotaCategoria MapToValueRelatorioAluguel(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                telefone1 = reader["telefone1"].ToString(),
                celular1 = reader["celular1"].ToString(),
                valor_aluguel = (decimal)reader["valor_aluguel"],
                valor_destaque = (decimal)reader["valor_destaque"],
                desconto = (int)reader["desconto"],
                total = (int)reader["total"],
                Garota = new Garota { nome = reader["nome"].ToString() },
                Categoria = new Categoria { desc_categoria = reader["desc_categoria"].ToString() },
                Estilo = new Estilo { desc_estilo = reader["desc_estilo"].ToString() },
                quantidade2 = reader["qtd_critica"].ToString()
            };
        }


        private GarotaCategoria MapToValueConsultarGarotaPorId(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),
                status = (bool)reader["status"],
                Garota = new Garota { nome = reader["nome"].ToString(), cpf = reader["cpf"].ToString(), status = (bool)reader["status"] },
                Categoria = new Categoria { desc_categoria = reader["desc_categoria"].ToString() }
            };
        }

        private GarotaCategoria MapToValueRetornaTodos(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString(),             
                Garota = new Garota { id_garota = (int)reader["id_garota"], nome = reader["nome"].ToString() }
               
            };
        }

        private GarotaCategoriaAtende MapToValueRetornaDadosGarotaCategoriaAtende(SqlDataReader reader)
        {
            return new GarotaCategoriaAtende()
            {
                id_garota_categoria_atende = (int)reader["id_garota_categoria_atende"],
                GarotaCategoria = new GarotaCategoria { id_garota_categoria = (int)reader["id_garota_categoria"] },
                Atende = new Atende  { id_atende = (int)reader["id_atende"], desc_atende = reader["desc_atende"].ToString() }
            };
        }

        private GarotaCategoriaIdioma MapToValueRetornaDadosGarotaCategoriaIdioma(SqlDataReader reader)
        {
            return new GarotaCategoriaIdioma()
            {
                id_garota_categoria_idioma = (int)reader["id_garota_categoria_idioma"],
                GarotaCategoria = new GarotaCategoria { id_garota_categoria = (int)reader["id_garota_categoria"] },
                Idioma = new Idioma { id_idioma = (int)reader["id_idioma"], desc_idioma = reader["desc_idioma"].ToString() }
            };
        }
        private GarotaCategoria MapToValuRetornaCupomAtivo(SqlDataReader reader)
        {
            return new GarotaCategoria()
            {
                id_garota_categoria = (int)reader["id_garota_categoria"],
                apelido = reader["apelido"].ToString() + " - " + reader["pct_cupom"].ToString()  + "%"
            };
        }















   

    }
}
