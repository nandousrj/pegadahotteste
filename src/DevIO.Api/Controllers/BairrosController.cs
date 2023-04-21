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
    //  [Route("api/bairros")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/bairros")]
    public class BairrosController : MainController
    {
        private readonly IBairroRepository _bairroRepository;
        private readonly IBairroService _bairroService;
        private readonly IMapper _mapper;


        public BairrosController(IBairroRepository bairroRepository,
                                     IBairroService bairroService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _bairroRepository = bairroRepository;
            _bairroService = bairroService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{id:int}")]
        public async Task<IEnumerable<BairroViewModel>> ObterTodos(int id)
        {
            var dados = _mapper.Map<IEnumerable<BairroViewModel>>(await _bairroRepository.RetornarTodosPOC(id));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<BairroViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<BairroViewModel>>(await _bairroRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BairroViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<BairroViewModel>(await _bairroRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Bairro", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<BairroViewModel>> Adicionar(BairroViewModel BairroViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var dados = _mapper.Map<Bairro>(BairroViewModel);
            var result = await _bairroService.Incluir(dados);

            if (!result) return CustomResponse(BairroViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Bairro", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BairroViewModel>> Atualizar(int id, BairroViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_bairro)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _bairroService.Alterar(_mapper.Map<Bairro>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Produto", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BairroViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _bairroService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<BairroViewModel> ObterDados(int id)
        {
            return _mapper.Map<BairroViewModel>(await _bairroRepository.ObterPorIdPOC(id));
        }

       
    }
}
