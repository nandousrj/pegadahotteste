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
    //  [Route("api/atendes")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/atendes")]
    public class AtendesController : MainController
    {
        private readonly IAtendeRepository _atendeRepository;
        private readonly IAtendeService _atendeService;
        private readonly IMapper _mapper;


        public AtendesController(IAtendeRepository atendeRepository,
                                     IAtendeService atendeService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _atendeRepository = atendeRepository;
            _atendeService = atendeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<AtendeViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<AtendeViewModel>>(await _atendeRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<AtendeViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<AtendeViewModel>>(await _atendeRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AtendeViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<AtendeViewModel>(await _atendeRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Atende", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AtendeViewModel>> Adicionar(AtendeViewModel AtendeViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<Atende>(AtendeViewModel);
            var result = await _atendeService.Incluir(dados);

            if (!result) return CustomResponse(AtendeViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Atende", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AtendeViewModel>> Atualizar(int id, AtendeViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_atende)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _atendeService.Alterar(_mapper.Map<Atende>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Produto", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AtendeViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _atendeService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<AtendeViewModel> ObterDados(int id)
        {
            return _mapper.Map<AtendeViewModel>(await _atendeRepository.ObterPorIdPOC(id));
        }

       
    }
}
