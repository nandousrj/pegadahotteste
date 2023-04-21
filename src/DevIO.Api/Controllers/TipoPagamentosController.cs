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
    //  [Route("api/tipopagamentos")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tipopagamentos")]
    public class TipoPagamentosController : MainController
    {
        private readonly ITipoPagamentoRepository _tipopagamentoRepository;
        private readonly ITipoPagamentoService _tipopagamentoService;
        private readonly IMapper _mapper;


        public TipoPagamentosController(ITipoPagamentoRepository tipopagamentoRepository,
                                     ITipoPagamentoService tipopagamentoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _tipopagamentoRepository = tipopagamentoRepository;
            _tipopagamentoService = tipopagamentoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<TipoPagamentoViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<TipoPagamentoViewModel>>(await _tipopagamentoRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<TipoPagamentoViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<TipoPagamentoViewModel>>(await _tipopagamentoRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoPagamentoViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<TipoPagamentoViewModel>(await _tipopagamentoRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("TipoPagamento", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TipoPagamentoViewModel>> Adicionar(TipoPagamentoViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<TipoPagamento>(dadosViewModel);
            var result = await _tipopagamentoService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("TipoPagamento", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TipoPagamentoViewModel>> Atualizar(int id, TipoPagamentoViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_tipo_pagamento)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tipopagamentoService.Alterar(_mapper.Map<TipoPagamento>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("TipoPagamento", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TipoPagamentoViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _tipopagamentoService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<TipoPagamentoViewModel> ObterDados(int id)
        {
            return _mapper.Map<TipoPagamentoViewModel>(await _tipopagamentoRepository.ObterPorIdPOC(id));
        }

       
    }
}
