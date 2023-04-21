using AutoMapper;
using DevIO.Api.Extensions;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/PegadaHotExterna")]   
  //  [Route("api/PegadaHotExterna")]
    public class PegadaHotExternaController : MainController
    {
        private readonly IEstiloRepository _estiloRepository;
        private readonly IEstiloService _estiloService;
        private readonly IGarotaCategoriaRepository _garotacategoriaRepository;
        private readonly IGarotaCategoriaService _garotacategoriaService;
        private readonly IGarotaCategoriaAtendeRepository _garotacategoriaatendeRepository;
        private readonly IGarotaCategoriaIdiomaRepository _garotacategoriaidiomaRepository;
        private readonly IZonaRepository _zonaRepository;
        private readonly IZonaService _zonaService;
        private readonly INovidadeRepository _novidadeRepository;
        private readonly INovidadeService _novidadeService;
        private readonly ITipoCriticaRepository _tipocriticaRepository;
        private readonly IVisualizacaoRepository _visualizacaoRepository;
        private readonly IVisualizacaoService _visualizacaoService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;

        public PegadaHotExternaController(IEstiloRepository estiloRepository,
                                     IEstiloService estiloService,
                                     IGarotaCategoriaRepository garotacategoriaRepository,
                                     IGarotaCategoriaService garotacategoriaService,
                                     IGarotaCategoriaAtendeRepository garotacategoriaatendeRepository,
                                     IGarotaCategoriaIdiomaRepository garotacategoriaidiomaRepository,
                                     IZonaRepository zonaRepository,
                                     IZonaService zonaService,
                                     INovidadeRepository novidadeRepository,
                                     INovidadeService novidadeService,
                                     ITipoCriticaRepository tipocriticaRepository,
                                     IVisualizacaoRepository visualizacaoRepository,
                                     IVisualizacaoService visualizacaoService,
                                     IControleSistemaRepository controlesistemaRepository,
                                     INotificador notificador,
                                     IMapper mapper
                                     ) : base(notificador)
        {
            _estiloRepository = estiloRepository;
            _estiloService = estiloService;
            _garotacategoriaRepository = garotacategoriaRepository;
            _garotacategoriaService = garotacategoriaService;
            _garotacategoriaatendeRepository = garotacategoriaatendeRepository;
            _garotacategoriaidiomaRepository = garotacategoriaidiomaRepository;
            _zonaRepository = zonaRepository;
            _zonaService = zonaService;
            _novidadeRepository = novidadeRepository;
            _novidadeService = novidadeService;
            _tipocriticaRepository = tipocriticaRepository;
            _visualizacaoRepository = visualizacaoRepository;
            _visualizacaoService = visualizacaoService;
            _controlesistemaRepository = controlesistemaRepository;

            _mapper = mapper;
        }


        //PegadoraPrincipalMaster(cabeçalho, anuncios, rodapé)
        //Consulta Criticas
        //Cupom
        //Curtir
        //SuaConta

        //TODO: pegar o contador ao clicar no site --ok
        //TODO: Fazer o RetornaDadosParametro --ok
        //TODO: Montar o processo de critica --ok
        //TODO: os curtis da pegadora --ok
        //TODO: colocar as imagens nas pastas --ok
        //TODO: fazer controller dos anuncios da principal(criado o interface, service, repository, controller e validation), --ok
        //TODO: fazer controller dos anuncios  externo --ok
        //TODO: nas controllers que insere ou altera imagens, colocar o controlesistema, com o parametro para o local das imagens
        // são Anuncios(ok), TipoCritica(ok),  grupos(ok), novidade(ok), Garota, GarotaCategoria,
        //TODO: colocar na garotacategoria horário de atendimento(Segunda à Sexta de 13h às 22hrs.), o segundo ou primeiro numero do celular será o zap, fixo. Ter o cadastro com ddd

        //// Ranking
        // [AllowAnonymous]
        // [HttpGet("CarregaGarotaCurtidasVisualizadasTotal")]
        // public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotal(int id_categoria)
        // {
        //     return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasVisualizadasTotal(id_categoria));
        // }

        //// Ranking
        // [AllowAnonymous]
        // [HttpGet("CarregaGarotasRankingVisualizacoes")]
        // public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoes(int id_categoria)
        // {
        //     return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosVisualizadas(id_categoria));
        // }

        //// Ranking
        // [AllowAnonymous]
        // [HttpGet("CarregaGarotasRankingCurticoes")]
        // public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoes(int id_categoria)
        // {
        //     return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidas(id_categoria));
        // }



        //// RankingTodos
        // [AllowAnonymous]
        // [HttpGet("CarregaGarotaCurtidasVisualizadasTotalTodos")]
        // public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotalTodos(int id_categoria)
        // {
        //     return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasVisualizadasTotalTodos(id_categoria));
        // }

        //// RankingTodos
        // [AllowAnonymous]
        // [HttpGet("CarregaGarotasRankingVisualizacoesTodos")]
        // private async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoesTodos(int id_categoria)
        // {
        //     return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosVisualizadasTodos(id_categoria));
        // }

        // //RankingTodos
        // [AllowAnonymous]
        // [HttpGet("CarregaGarotasRankingCurticoesTodos")]
        // public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoesTodos(int id_categoria)
        // {
        //     return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasTodos(id_categoria));
        // }


        ////RankingTodos
        //[AllowAnonymous]
        //[HttpGet("RetornarTotalVisualizacaoSite")]
        //public async Task<int> RetornarTotalVisualizacaoSite()
        //{
        //    return await _visualizacaoRepository.RetornarTotalVisualizacaoSitePOC();
        //}

        ////RankingTodos
        //[AllowAnonymous]
        //[HttpGet("RetornarTotalVisualizacaoGarotas")]
        //public async Task<int> RetornarTotalVisualizacaoGarotas()
        //{
        //    return await _visualizacaoRepository.RetornarTotalVisualizacaoGarotasPOC();
        //}

        ////RankingTodos
        //[AllowAnonymous]
        //[HttpGet("RetornarTotalVisualizacaoSiteGrupo")]
        //public async Task<IEnumerable<VisualizacaoViewModel>> RetornarTotalVisualizacaoSiteGrupo()
        //{
        //    return _mapper.Map<IEnumerable<VisualizacaoViewModel>>(await _visualizacaoRepository.RetornarTotalVisualizacaoSiteGrupoPOC());
        //}


        ////index       
        //[HttpGet("RetornarTodosEstilos")]
        //public async Task<IEnumerable<EstiloViewModel>> ObterTodos()
        //{
        //    var estilo = _mapper.Map<IEnumerable<EstiloViewModel>>(await _estiloRepository.RetornarTodosPOC());

        //    return estilo;
        //}

        ////index
        //[AllowAnonymous]
        //[HttpGet("RetornarMenuZona/{status:int}")]
        //public async Task<IEnumerable<ZonaViewModel>> ObterTodos(int status)
        //{
        //    var dados = _mapper.Map<IEnumerable<ZonaViewModel>>(await _zonaRepository.RetornarTodosPOC(status));

        //    return dados;
        //}

        ////index
        //[AllowAnonymous]
        //[HttpGet("RetornarDadosPrincipal")]
        //public async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosPrincipal(int id_categoria, string desc_zona, int ind_destaque, string apelido, int id_estilo = 0)
        //{
        //    var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosPrincipal(id_categoria, desc_zona, ind_destaque, apelido, id_estilo));
        //    var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
        //    var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

        //    string caminhoImagem = Util.caminhoFoto.ToString();

        //    // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoImagem + imgPrefixo.ToString(), imgPrefixo + "_" + Util.TiraAcentos(arquivo.FileName)); //"wwwroot/app/demo-webapi/src/assets"
        //    foreach (GarotaCategoriaViewModel i in dados)
        //    {
        //        i.imagem_01 = "wwwroot/" + caminhoPastaImagemBanco.val_parametro.ToString() + caminhoImagemBanco.val_parametro.ToString() + i.imagem_01;
        //    }
        //    return dados;
        //}

        ////index
        //[AllowAnonymous]
        //[HttpGet("RetornarNovidades")]
        //public async Task<IEnumerable<NovidadeViewModel>> RetornarDadosAtivos(int id_categoria)
        //{
        //    var dados = _mapper.Map<IEnumerable<NovidadeViewModel>>(await _novidadeRepository.ObterDadosAtivos(id_categoria));

        //    return dados;
        //}



        ////pegadora -- concatenar
        //[AllowAnonymous]
        //[HttpGet("RetornarGarotaCategoriaAtendeConcat")]
        //private async Task<IEnumerable<GarotaCategoriaAtendeViewModel>> RetornarGarotaCategoriaAtende(int id_garota_categoria)
        //{
        //    return _mapper.Map<IEnumerable<GarotaCategoriaAtendeViewModel>>(await _garotacategoriaatendeRepository.RetornarGarotaCategoriaAtende(id_garota_categoria));
        //}

        ////pegadora --concatenar
        //[AllowAnonymous]
        //[HttpGet("RetornarGarotaCategoriaIdiomaConcat")]
        //public async Task<IEnumerable<GarotaCategoriaIdiomaViewModel>> RetornarGarotaCategoriaIdiomae(int id_garota_categoria)
        //{
        //    return _mapper.Map<IEnumerable<GarotaCategoriaIdiomaViewModel>>(await _garotacategoriaidiomaRepository.RetornarGarotaCategoriaIdioma(id_garota_categoria));
        //}

        //pegadora 
        [AllowAnonymous]
        [HttpGet("RetornarTipoCritica")]
        public async Task<IEnumerable<TipoCriticaViewModel>> RetornarTipoCririca(int status)
        {
            return _mapper.Map<IEnumerable<TipoCriticaViewModel>>(await _tipocriticaRepository.RetornarTodosPOC(status));
        }


        ////pegadora
        //[AllowAnonymous]
        //[HttpGet("RetornarGarotaCategoriaDetalhe")]
        //public async Task<GarotaCategoriaViewModel> RetornarDadosDetalhe(int id_garota_categoria, int id_categoria)
        //{
        //    return _mapper.Map<GarotaCategoriaViewModel>(await _garotacategoriaRepository.RetornarDadosDetalhe(id_garota_categoria, id_categoria));
        //}




        ////pegadora
        //[AllowAnonymous]
        //[HttpGet("AtualizarVisualizacaoGarotaCategoria")]
        //public async Task<int> AtualizarVisualizacaoGarotaCategoria(int id_garota_categoria, int id_categoria)
        //{
        //    return await _garotacategoriaRepository.AtualizarVisualizacaoGarotaCategoria(id_garota_categoria, id_categoria);
        //}




        ////GrupoPegadora
        //[AllowAnonymous]
        //[HttpGet("RetornaDadosGarotaCategoriaGrupo")]
        //public async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosGrupo(int id_grupo)
        //{
        //    return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosGrupo(id_grupo));
        //}

        ////GrupoPegadora
        //[AllowAnonymous]
        //[HttpGet("AlterarSiteVisualizacaoGrupo")]
        //public async Task<int> AlterarSiteVisualizacaoGrupo(int id_grupo)
        //{
        //    return await _visualizacaoRepository.AlterarSiteVisualizacaoGrupoPOC(id_grupo);
        //}

    }
}
