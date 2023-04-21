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
    //  [Route("api/idiomas")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/idiomas")]
    public class IdiomasController : MainController
    {
        private readonly IIdiomaRepository _idiomaRepository;
        private readonly IIdiomaService _idiomaService;
        private readonly IMapper _mapper;


        public IdiomasController(IIdiomaRepository idiomaRepository,
                                     IIdiomaService idiomaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _idiomaRepository = idiomaRepository;
            _idiomaService = idiomaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<IdiomaViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<IdiomaViewModel>>(await _idiomaRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<IdiomaViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<IdiomaViewModel>>(await _idiomaRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IdiomaViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<IdiomaViewModel>(await _idiomaRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Idioma", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<IdiomaViewModel>> Adicionar(IdiomaViewModel dadosViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<Idioma>(dadosViewModel);
            var result = await _idiomaService.Incluir(dados);

            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Idioma", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<IdiomaViewModel>> Atualizar(int id, IdiomaViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_idioma)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _idiomaService.Alterar(_mapper.Map<Idioma>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Produto", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<IdiomaViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _idiomaService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<IdiomaViewModel> ObterDados(int id)
        {
            return _mapper.Map<IdiomaViewModel>(await _idiomaRepository.ObterPorIdPOC(id));
        }

       
    }
}
