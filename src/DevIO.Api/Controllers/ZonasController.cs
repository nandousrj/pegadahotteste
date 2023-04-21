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
    //  [Route("api/zonas")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/zonas")]
    public class ZonasController : MainController
    {
        private readonly IZonaRepository _zonaRepository;
        private readonly IZonaService _zonaService;
        private readonly IMapper _mapper;


        public ZonasController(IZonaRepository zonaRepository,
                                     IZonaService zonaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _zonaRepository = zonaRepository;
            _zonaService = zonaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<ZonaViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<ZonaViewModel>>(await _zonaRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<ZonaViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<ZonaViewModel>>(await _zonaRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ZonaViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<ZonaViewModel>(await _zonaRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Zona", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ZonaViewModel>> Adicionar(ZonaViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<Zona>(dadosViewModel);
            var result = await _zonaService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Zona", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ZonaViewModel>> Atualizar(int id, ZonaViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_zona)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _zonaService.Alterar(_mapper.Map<Zona>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Produto", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ZonaViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _zonaService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<ZonaViewModel> ObterDados(int id)
        {
            return _mapper.Map<ZonaViewModel>(await _zonaRepository.ObterPorIdPOC(id));
        }

       
    }
}
