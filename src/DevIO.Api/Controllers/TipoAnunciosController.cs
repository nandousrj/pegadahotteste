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
    //  [Route("api/tipoanuncios")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tipoanuncios")]
    public class TipoAnunciosController : MainController
    {
        private readonly ITipoAnuncioRepository _tipoanuncioRepository;
        private readonly ITipoAnuncioService _tipoanuncioService;
        private readonly IMapper _mapper;


        public TipoAnunciosController(ITipoAnuncioRepository tipoanuncioRepository,
                                     ITipoAnuncioService tipoanuncioService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _tipoanuncioRepository = tipoanuncioRepository;
            _tipoanuncioService = tipoanuncioService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<TipoAnuncioViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<TipoAnuncioViewModel>>(await _tipoanuncioRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<TipoAnuncioViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<TipoAnuncioViewModel>>(await _tipoanuncioRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoAnuncioViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<TipoAnuncioViewModel>(await _tipoanuncioRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("TipoAnuncio", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TipoAnuncioViewModel>> Adicionar(TipoAnuncioViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<TipoAnuncio >(dadosViewModel);
            var result = await _tipoanuncioService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("TipoAnuncio", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TipoAnuncioViewModel>> Atualizar(int id, TipoAnuncioViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_tipo_anuncio)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tipoanuncioService.Alterar(_mapper.Map<TipoAnuncio>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Produto", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TipoAnuncioViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _tipoanuncioService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<TipoAnuncioViewModel> ObterDados(int id)
        {
            return _mapper.Map<TipoAnuncioViewModel>(await _tipoanuncioRepository.ObterPorIdPOC(id));
        }

       
    }
}
