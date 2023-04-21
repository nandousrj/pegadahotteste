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
      [Route("api/ExternoTipoCriticas")]
  //  [Authorize]
 //   [ApiVersion("1.0")]
//    [Route("api/v{version:apiVersion}/ExternoTipoCriticas")]
    public class ExternoTipoCriticasController : MainController
    {
        private readonly ITipoCriticaRepository _tipocriticaRepository;
        private readonly ITipoCriticaService _tipocriticaService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public ExternoTipoCriticasController(ITipoCriticaRepository tipocriticaRepository,
                                     ITipoCriticaService tipocriticaService,
                                     IControleSistemaRepository controlesistemaRepository,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _tipocriticaRepository = tipocriticaRepository;
            _tipocriticaService = tipocriticaService;
            _controlesistemaRepository = controlesistemaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<TipoCriticaViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<TipoCriticaViewModel>>(await _tipocriticaRepository.RetornarTodosPOC(status));


            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_EMOTIONS"));

            //string caminhoImagem = Util.caminhoFoto.ToString();

            // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoImagem + imgPrefixo.ToString(), imgPrefixo + "_" + Util.TiraAcentos(arquivo.FileName)); //"wwwroot/app/demo-webapi/src/assets"
            foreach (TipoCriticaViewModel i in dados)
            {
                i.imagem = "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.ToString() + "/" + i.imagem;
            }

            return dados;
        }


        [AllowAnonymous]
        [HttpGet("RetornaDadosQtdTipoCategoriaPOC")]
        public async Task<IEnumerable<TipoCriticaViewModel>> RetornaDadosQtdTipoCategoriaPOC(int id_garota_categoria, int id_categoria)
        {
            var dados = _mapper.Map<IEnumerable<TipoCriticaViewModel>>(await _tipocriticaRepository.RetornaDadosQtdTipoCategoriaPOC(id_garota_categoria, id_categoria));


            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_EMOTIONS"));

            //string caminhoImagem = Util.caminhoFoto.ToString();

            // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoImagem + imgPrefixo.ToString(), imgPrefixo + "_" + Util.TiraAcentos(arquivo.FileName)); //"wwwroot/app/demo-webapi/src/assets"
            foreach (TipoCriticaViewModel i in dados)
            {
                i.imagem = "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + "/" + i.imagem;
            }

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoCriticaViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<TipoCriticaViewModel>(await _tipocriticaRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }

        [HttpGet("ObterDados")]
        public async Task<TipoCriticaViewModel> ObterDados(int id)
        {
            return _mapper.Map<TipoCriticaViewModel>(await _tipocriticaRepository.ObterPorIdPOC(id));
        }





    }
}
