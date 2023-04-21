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
    //  [Route("api/tipocontas")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tipocontas")]
    public class TipoContasController : MainController
    {
        private readonly ITipoContaRepository _tipocontaRepository;
        private readonly ITipoContaService _tipocontaService;
        private readonly IMapper _mapper;


        public TipoContasController(ITipoContaRepository tipocontaRepository,
                                     ITipoContaService tipocontaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _tipocontaRepository = tipocontaRepository;
            _tipocontaService = tipocontaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{id:int}")]
        public async Task<IEnumerable<TipoContaViewModel>> ObterTodos(int id = 0)
        {
            var dados = _mapper.Map<IEnumerable<TipoContaViewModel>>(await _tipocontaRepository.RetornarTodosPOC());

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<TipoContaViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<TipoContaViewModel>>(await _tipocontaRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoContaViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<TipoContaViewModel>(await _tipocontaRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("TipoConta", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TipoContaViewModel>> Adicionar(TipoContaViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<TipoConta>(dadosViewModel);
            var result = await _tipocontaService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("TipoConta", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TipoContaViewModel>> Atualizar(int id, TipoContaViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_tipo_conta)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tipocontaService.Alterar(_mapper.Map<TipoConta>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("TipoConta", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TipoContaViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _tipocontaService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<TipoContaViewModel> ObterDados(int id)
        {
            return _mapper.Map<TipoContaViewModel>(await _tipocontaRepository.ObterPorIdPOC(id));
        }

       
    }
}
