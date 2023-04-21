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
    //  [Route("api/olhos")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/olhos")]
    public class OlhosController : MainController
    {
        private readonly IOlhosRepository _olhosRepository;
        private readonly IOlhosService _olhosService;
        private readonly IMapper _mapper;


        public OlhosController(IOlhosRepository olhosRepository,
                                     IOlhosService olhosService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _olhosRepository = olhosRepository;
            _olhosService = olhosService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<OlhosViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<OlhosViewModel>>(await _olhosRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<OlhosViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<OlhosViewModel>>(await _olhosRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OlhosViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<OlhosViewModel>(await _olhosRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Olhos", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<OlhosViewModel>> Adicionar(OlhosViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<Olhos>(dadosViewModel);
            var result = await _olhosService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Olhos", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<OlhosViewModel>> Atualizar(int id, OlhosViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_olhos)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _olhosService.Alterar(_mapper.Map<Olhos>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Olhos", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OlhosViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _olhosService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<OlhosViewModel> ObterDados(int id)
        {
            return _mapper.Map<OlhosViewModel>(await _olhosRepository.ObterPorIdPOC(id));
        }

       
    }
}
