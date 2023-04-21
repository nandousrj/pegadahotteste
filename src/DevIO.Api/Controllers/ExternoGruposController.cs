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
    //  [Route("api/ExternoGrupos")]
    //  [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ExternoGrupos")]
    public class ExternoGruposController : MainController
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IGrupoService _grupoService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IVisualizacaoRepository _visualizacaoRepository;
        private readonly IVisualizacaoService _visualizacaoService;
        private readonly IMapper _mapper;


        public ExternoGruposController(IGrupoRepository grupoRepository,
                                     IGrupoService grupoService,
                                     IControleSistemaRepository controlesistemaRepository,
                                     IVisualizacaoRepository visualizacaoRepository,
                                     IVisualizacaoService visualizacaoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _grupoRepository = grupoRepository;
            _grupoService = grupoService;
            _controlesistemaRepository = controlesistemaRepository;
            _visualizacaoRepository = visualizacaoRepository;
            _visualizacaoService = visualizacaoService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GrupoViewModel>> ObterPorId(int id)
        {

            int quantidade = await _visualizacaoRepository.AlterarSiteVisualizacaoGrupoPOC(id);

            var dados = _mapper.Map<GrupoViewModel>(await _grupoRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_GRUPO"));


            dados.imagem_01 = "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + "/" + dados.imagem_01;


            return dados;
        }






    }
}
