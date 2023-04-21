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
    //  [Route("api/PermissoesInstituicaoes")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/PermissoesInstituicaos")]
    public class PermissoesInstituicoesController : MainController
    {
        private readonly IPermissoesInstituicaoRepository _permissoesInstituicaoRepository;
        private readonly IPermissoesInstituicaoService _permissoesInstituicaoService;
        private readonly IMapper _mapper;


        public PermissoesInstituicoesController(IPermissoesInstituicaoRepository permissoesInstituicaoRepository,
                                     IPermissoesInstituicaoService permissoesInstituicaoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _permissoesInstituicaoRepository = permissoesInstituicaoRepository;
            _permissoesInstituicaoService = permissoesInstituicaoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{id:int}")]
        public async Task<IEnumerable<PermissoesInstituicaoViewModel>> ObterTodos(int id = 0)
        {
            var dados = _mapper.Map<IEnumerable<PermissoesInstituicaoViewModel>>(await _permissoesInstituicaoRepository.RetornarTodosPOC());

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<PermissoesInstituicaoViewModel>> Consultar(string nome = "")
        {
            var dados = _mapper.Map<IEnumerable<PermissoesInstituicaoViewModel>>(await _permissoesInstituicaoRepository.ConsultarPOC(nome));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PermissoesInstituicaoViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<PermissoesInstituicaoViewModel>(await _permissoesInstituicaoRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("PermissoesInstituicao", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<PermissoesInstituicaoViewModel>> Adicionar(PermissoesInstituicaoViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<PermissoesInstituicao>(dadosViewModel);
            var result = await _permissoesInstituicaoService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("PermissoesInstituicao", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PermissoesInstituicaoViewModel>> Atualizar(int id, PermissoesInstituicaoViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_instituicao)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _permissoesInstituicaoService.Alterar(_mapper.Map<PermissoesInstituicao>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("PermissoesInstituicao", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PermissoesInstituicaoViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _permissoesInstituicaoService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<PermissoesInstituicaoViewModel> ObterDados(int id)
        {
            return _mapper.Map<PermissoesInstituicaoViewModel>(await _permissoesInstituicaoRepository.ObterPorIdPOC(id));
        }

       
    }
}
