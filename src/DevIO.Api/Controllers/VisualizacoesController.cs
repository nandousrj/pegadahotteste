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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Visualizacoes")]
    public class VisualizacoesController : MainController
    {

        /*
         
        Classes usadas:
        Visualizacao
        Estilo
        CarotaCategoria
        Zona
        Novidade



         */
        private readonly IVisualizacaoRepository _visualizacaoRepository;
        private readonly IVisualizacaoService _visualizacaoService;
        private readonly IMapper _mapper;


        public VisualizacoesController(IVisualizacaoRepository visualizacaoRepository,
                                     IVisualizacaoService visualizacaoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _visualizacaoRepository = visualizacaoRepository;
            _visualizacaoService = visualizacaoService;
            _mapper = mapper;
        }

        //[AllowAnonymous]
        //[HttpGet("obtertodos/{status:int}")]
        //public async Task<IEnumerable<GrupoViewModel>> ObterTodos(int status)
        //{
        //    var dados = _mapper.Map<IEnumerable<GrupoViewModel>>(await _grupoRepository.RetornarTodosPOC(status));

        //    return dados;
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IEnumerable<GrupoViewModel>> Consultar(string descricao = "")
        //{
        //    var dados = _mapper.Map<IEnumerable<GrupoViewModel>>(await _grupoRepository.ConsultarPOC(descricao));

        //    return dados;
        //}

        [AllowAnonymous]
        [HttpGet("TotalVisualizacaoSite")]
        public async Task<ActionResult<int>> RetornarTotalVisualizacaoSite()
        {
            int dados = _mapper.Map<int>(await _visualizacaoRepository.RetornarTotalVisualizacaoSitePOC());

            // if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("TotalVisualizacaoGarotas")]
        public async Task<ActionResult<int>> RetornarTotalVisualizacaoGarotasPOC()
        {
            int dados = _mapper.Map<int>(await _visualizacaoRepository.RetornarTotalVisualizacaoGarotasPOC());

            // if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("TotalVisualizacaoSiteGrupo")]
        public async Task<IEnumerable<VisualizacaoViewModel>> RetornarTotalVisualizacaoSiteGrupo()
        {
            var dados = _mapper.Map<IEnumerable<VisualizacaoViewModel>>(await _visualizacaoRepository.RetornarTotalVisualizacaoSiteGrupoPOC());

            return dados;
        }


        //[ClaimsAuthorize("Visualizacoes", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<VisualizacaoViewModel>> Adicionar(int id_garota_categoria,  int id_categoria)
        {
            VisualizacaoViewModel visualizacao = new VisualizacaoViewModel();// { GarotaCategoria.id_garota_categoria = id_garota_categoria, GarotaCategoria.Categoria.id_categoria = id_categoria });
            visualizacao.GarotaCategoria.id_garota_categoria = id_garota_categoria;
            visualizacao.GarotaCategoria.Categoria.id_categoria = id_categoria;

           if (!ModelState.IsValid) return CustomResponse(ModelState);

            var dados = _mapper.Map<Visualizacao>(visualizacao);
            var result = await _visualizacaoService.Incluir(dados);
          
            return Ok(dados);
        }

    //    [ClaimsAuthorize("Vizualiacoes", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ActionResult<VisualizacaoViewModel>> Atualizar(int id_garota_categoria, int id_categoria)
        {
            VisualizacaoViewModel dadosViewModel = new VisualizacaoViewModel();// { GarotaCategoria.id_garota_categoria = id_garota_categoria, GarotaCategoria.Categoria.id_categoria = id_categoria });
            dadosViewModel.GarotaCategoria.id_garota_categoria = id_garota_categoria;
            dadosViewModel.GarotaCategoria.Categoria.id_categoria = id_categoria;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var dados = _mapper.Map<Visualizacao>(dadosViewModel);
            await _visualizacaoService.Alterar(dados);         

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Grupo", "Excluir")]
        //[AllowAnonymous]
        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<GrupoViewModel>> Excluir(int id)
        //{
        //    var dados = await ObterDados(id);

        //    if (dados == null) return NotFound();

        //    await _grupoService.Excluir(id);

        //    return CustomResponse(dados);
        //}



        //private async Task<GrupoViewModel> ObterDados(int id)
        //{
        //    return _mapper.Map<GrupoViewModel>(await _grupoRepository.ObterPorIdPOC(id));
        //}





    }
}
