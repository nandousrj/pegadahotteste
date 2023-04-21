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
    //  [Route("api/PermissoesSistemas")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/PermissoesSistemas")]
    public class PermissoesSistemasController : MainController
    {
        private readonly IPermissoesSistemaRepository _permissoessistemaRepository;
        private readonly IPermissoesSistemaService _permissoessistemaService;
        private readonly IMapper _mapper;


        public PermissoesSistemasController(IPermissoesSistemaRepository permissoessistemaRepository,
                                     IPermissoesSistemaService permissoessistemaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _permissoessistemaRepository = permissoessistemaRepository;
            _permissoessistemaService = permissoessistemaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{id:int}")]
        public async Task<IEnumerable<PermissoesSistemaViewModel>> ObterTodos(int id = 0)
        {
            var dados = _mapper.Map<IEnumerable<PermissoesSistemaViewModel>>(await _permissoessistemaRepository.RetornarTodosPOC());

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<PermissoesSistemaViewModel>> Consultar(string nome = "")
        {
            var dados = _mapper.Map<IEnumerable<PermissoesSistemaViewModel>>(await _permissoessistemaRepository.ConsultarPOC(nome));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PermissoesSistemaViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<PermissoesSistemaViewModel>(await _permissoessistemaRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("PermissoesSistema", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<PermissoesSistemaViewModel>> Adicionar(PermissoesSistemaViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<PermissoesSistema>(dadosViewModel);
            var result = await _permissoessistemaService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("PermissoesSistema", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PermissoesSistemaViewModel>> Atualizar(int id, PermissoesSistemaViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_sistema)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _permissoessistemaService.Alterar(_mapper.Map<PermissoesSistema>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("PermissoesSistema", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PermissoesSistemaViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _permissoessistemaService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<PermissoesSistemaViewModel> ObterDados(int id)
        {
            return _mapper.Map<PermissoesSistemaViewModel>(await _permissoessistemaRepository.ObterPorIdPOC(id));
        }

       
    }
}
