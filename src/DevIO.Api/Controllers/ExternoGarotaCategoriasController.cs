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
using System.Drawing;
using FileSizeFromBase64.NET;

namespace DevIO.Api.Controllers
{
    //  [Route("api/PrincipalSite")]
    //  [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/externogarotacategorias")]
    public class ExternoGarotaCategoriasController : MainController
    {

        private readonly IGarotaCategoriaRepository _garotacategoriaRepository;
        private readonly IGarotaCategoriaService _garotacategoriaService;
        private readonly IGarotaCategoriaAtendeRepository _garotacategoriaatendeRepository;
        private readonly IGarotaCategoriaIdiomaRepository _garotacategoriaidiomaRepository;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly ITipoCriticaRepository _tipocriticaRepository;
        private readonly ITipoCriticaService _tipocriticaService;
        private readonly IMapper _mapper;


        public ExternoGarotaCategoriasController(IGarotaCategoriaRepository garotacategoriaRepository,
                                     IGarotaCategoriaService garotacategoriaService,
                                     IGarotaCategoriaAtendeRepository garotacategoriaatendeRepository,
                                     IGarotaCategoriaIdiomaRepository garotacategoriaidiomaRepository,
                                     IControleSistemaRepository controlesistemaRepository,
                                     ITipoCriticaRepository tipocriticaRepository,
                                     ITipoCriticaService tipocriticaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _garotacategoriaRepository = garotacategoriaRepository;
            _garotacategoriaService = garotacategoriaService;
            _garotacategoriaatendeRepository = garotacategoriaatendeRepository;
            _garotacategoriaidiomaRepository = garotacategoriaidiomaRepository;
            _controlesistemaRepository = controlesistemaRepository;
            _tipocriticaRepository = tipocriticaRepository;
            _tipocriticaService = tipocriticaService;
            _mapper = mapper;
        }


        //index 
        [AllowAnonymous]
        [HttpGet("RetornarDadosPrincipal")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosPrincipal(int id_categoria, string desc_zona, int ind_destaque, string apelido, int id_estilo = 0)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosPrincipal(id_categoria, desc_zona, ind_destaque, apelido, id_estilo));
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            // var path = Path.Combine(Directory.GetCurrentDirectory(),  caminhoImagem + imgPrefixo.ToString(), imgPrefixo + "_" + Util.TiraAcentos(arquivo.FileName)); //"wwwroot/app/demo-webapi/src/assets"
            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
                //i.id_cripto = Util.RetornaValorCriptografado(i.id_garota_categoria.ToString());
            }
            return dados;
        }


        //pegadora 
        [AllowAnonymous]
        [HttpGet("RetornarGarotaCategoriaDetalhe")]
        public async Task<GarotaCategoriaViewModel> RetornarDadosDetalhe(int id_garota_categoria, int id_categoria)
        {
           // int id_garota_categoria = Convert.ToInt32(Util.RetornaValorDescriptografado(id_cripto));
            int valor = await _garotacategoriaRepository.AtualizarVisualizacaoGarotaCategoria(id_garota_categoria, id_categoria);
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));
            var caminhoImagemBancoEmotions = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_EMOTIONS"));
            var dados = _mapper.Map<GarotaCategoriaViewModel>(await _garotacategoriaRepository.RetornarDadosDetalhe(id_garota_categoria, id_categoria));
            var dadosAtende = _mapper.Map<IEnumerable<GarotaCategoriaAtendeViewModel>>(await _garotacategoriaatendeRepository.RetornarGarotaCategoriaAtende(id_garota_categoria));
            var dadosIdioma = _mapper.Map<IEnumerable<GarotaCategoriaIdiomaViewModel>>(await _garotacategoriaidiomaRepository.RetornarGarotaCategoriaIdioma(id_garota_categoria));
            var listaTipoCritica = _mapper.Map<IEnumerable<TipoCriticaViewModel>>(await _tipocriticaRepository.RetornaDadosQtdTipoCategoriaPOC(id_garota_categoria, id_categoria));

            if (!(string.IsNullOrEmpty(dados.imagem_01)))
            {
                dados.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_01;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_02)))
            {
                dados.imagem_02 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_02;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_03)))
            {
                dados.imagem_03 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_03;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_04)))
            {
                dados.imagem_04 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_04;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_05)))
            {
                dados.imagem_05 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_05;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_06)))
            {
                dados.imagem_06 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_06;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_07)))
            {
                dados.imagem_07 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_07;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_08)))
            {
                dados.imagem_08 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_08;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_09)))
            {
                dados.imagem_09 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_09;
            }
            if (!(string.IsNullOrEmpty(dados.imagem_10)))
            {
                dados.imagem_10 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + dados.imagem_10;
            }

            dados.Atende = new AtendeViewModel();
            foreach (GarotaCategoriaAtendeViewModel i in dadosAtende)
            {
                dados.Atende.desc_atende += i.Atende.desc_atende + ", ";
            }

          //  string teste = dados.Atende.desc_atende;
            if (!(string.IsNullOrEmpty(dados.Atende.desc_atende)))
            {
                dados.Atende.desc_atende = dados.Atende.desc_atende.Substring(0, dados.Atende.desc_atende.Length - 2);
            }

            dados.Idioma = new IdiomaViewModel();
            foreach (GarotaCategoriaIdiomaViewModel i in dadosIdioma)
            {
                dados.Idioma.desc_idioma += i.Atende.desc_idioma + ", ";
            }

            if (!string.IsNullOrEmpty(dados.Idioma.desc_idioma))
            {
                dados.Idioma.desc_idioma = dados.Idioma.desc_idioma.Substring(0, dados.Idioma.desc_idioma.Length - 2);
            }

            foreach (TipoCriticaViewModel i in listaTipoCritica)
            {
                i.imagem =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBancoEmotions.val_parametro.Replace("//", "/").ToString() + "/" + i.imagem;
            }

            dados.ListaTipoCritica = listaTipoCritica;



            return dados;
        }

        //GrupoPegadora 
        [AllowAnonymous]
        [HttpGet("RetornaDadosGarotaCategoriaGrupo")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosGrupo(int id_grupo)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosGrupo(id_grupo));
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
            }
            return dados;
        }

        //Ranking 
        [AllowAnonymous]
        [HttpGet("CarregaGarotaCurtidasVisualizadasTotal")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotal(int id_categoria)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasVisualizadasTotal(id_categoria));

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
            }
            return dados;
        }


        // Ranking 
        [AllowAnonymous]
        [HttpGet("CarregaGarotasRankingVisualizacoes")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoes(int id_categoria)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosVisualizadas(id_categoria));

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
            }
            return dados;
        }

        // Ranking
        [AllowAnonymous]
        [HttpGet("CarregaGarotasRankingCurticoes")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoes(int id_categoria)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidas(id_categoria));

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
            }
            return dados;
        }

        // RankingTodos
        [AllowAnonymous]
        [HttpGet("CarregaGarotaCurtidasVisualizadasTotalTodos")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotalTodos(int id_categoria)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasVisualizadasTotalTodos(id_categoria));

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
            }
            return dados;
        }

        // RankingTodos
        [AllowAnonymous]
        [HttpGet("CarregaGarotasRankingVisualizacoesTodos")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoesTodos(int id_categoria)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosVisualizadasTodos(id_categoria));

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
            }
            return dados;
        }


        // RankingTodos
        [AllowAnonymous]
        [HttpGet("CarregaGarotasRankingCurticoesTodos")]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoesTodos(int id_categoria)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasTodos(id_categoria));

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            foreach (GarotaCategoriaViewModel i in dados)
            {
                i.imagem_01 =  caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.imagem_01;
            }
            return dados;
        }


        //pegadora
        [AllowAnonymous]
        [HttpGet("AtualizarVisualizacaoGarotaCategoria")]
        public async Task<int> AtualizarVisualizacaoGarotaCategoria(int id_garota_categoria, int id_categoria)
        {
            return await _garotacategoriaRepository.AtualizarVisualizacaoGarotaCategoria(id_garota_categoria, id_categoria);
        }



    }
}
