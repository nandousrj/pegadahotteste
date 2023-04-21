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
    //  [Route("api/sexos")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sexos")]
    public class SexosController : MainController
    {
        private readonly ISexoRepository _sexoRepository;
        private readonly ISexoService _sexoService;
        private readonly IMapper _mapper;


        public SexosController(ISexoRepository sexoRepository,
                                     ISexoService sexoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _sexoRepository = sexoRepository;
            _sexoService = sexoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<SexoViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<SexoViewModel>>(await _sexoRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<SexoViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<SexoViewModel>>(await _sexoRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SexoViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<SexoViewModel>(await _sexoRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Sexo", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<SexoViewModel>> Adicionar(SexoViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<Sexo>(dadosViewModel);
            var result = await _sexoService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Sexo", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SexoViewModel>> Atualizar(int id, SexoViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_sexo)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _sexoService.Alterar(_mapper.Map<Sexo>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Sexo", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SexoViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _sexoService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<SexoViewModel> ObterDados(int id)
        {
            return _mapper.Map<SexoViewModel>(await _sexoRepository.ObterPorIdPOC(id));
        }

       
    }
}
