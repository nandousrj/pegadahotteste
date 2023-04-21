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
    //  [Route("api/ControleSistemas")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ControleSistemas")]
    public class ControleSistemasController : MainController
    {
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IControleSistemaService _controlesistemaService;
        private readonly IMapper _mapper;


        public ControleSistemasController(IControleSistemaRepository controlesistemaRepository,
                                     IControleSistemaService controlesistemaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _controlesistemaRepository = controlesistemaRepository;
            _controlesistemaService = controlesistemaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<ControleSistemaViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<ControleSistemaViewModel>>(await _controlesistemaRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<ControleSistemaViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<ControleSistemaViewModel>>(await _controlesistemaRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ControleSistemaViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("ObterControleSistema")]
        public async Task<ActionResult<ControleSistemaViewModel>> ObterPorNome(string cod_parametro)
        {
            var dados = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC(cod_parametro));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("ControleSistema", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ControleSistemaViewModel>> Adicionar(ControleSistemaViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<ControleSistema>(dadosViewModel);
            var result = await _controlesistemaService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("ControleSistema", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ControleSistemaViewModel>> Atualizar(int id, ControleSistemaViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_controle_sistema)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _controlesistemaService.Alterar(_mapper.Map<ControleSistema>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Produto", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ControleSistemaViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _controlesistemaService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<ControleSistemaViewModel> ObterDados(int id)
        {
            return _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorIdPOC(id));
        }

       
    }
}
