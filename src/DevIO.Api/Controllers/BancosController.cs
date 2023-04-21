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
    //  [Route("api/Bancos")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Bancos")]
    public class BancosController : MainController
    {
        private readonly IBancoRepository _bancoRepository;
        private readonly IBancoService _bancoService;
        private readonly IMapper _mapper;


        public BancosController(IBancoRepository bancoRepository,
                                     IBancoService bancoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _bancoRepository = bancoRepository;
            _bancoService = bancoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{id:int}")]
        public async Task<IEnumerable<BancoViewModel>> ObterTodos(int id)
        {
            var dados = _mapper.Map<IEnumerable<BancoViewModel>>(await _bancoRepository.RetornarTodosPOC());

            return dados;
        }

        //[AllowAnonymous]       
        //[HttpGet]
        //public async Task<IEnumerable<BancoViewModel>> Consultar(string descricao = "")
        //{
        //    var dados = _mapper.Map<IEnumerable<BancoViewModel>>(await _bancoRepository.ConsultarPOC(descricao));

        //    return dados;
        //}

        //[AllowAnonymous]
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<BancoViewModel>> ObterPorId(int id)
        //{
        //    var dados = _mapper.Map<BancoViewModel>(await _bancoRepository.ObterPorIdPOC(id));

        //    if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

        //    return dados;
        //}


        //// [ClaimsAuthorize("Banco", "Adicionar")]
        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<ActionResult<BancoViewModel>> Adicionar(BancoViewModel dadosViewModel)
        //{
        // //   if (ModelState.IsValid) return BadRequest();
        //    var dados = _mapper.Map<Banco>(dadosViewModel);
        //    var result = await _bancoService.Incluir(dados);

        //    if (!result) return CustomResponse(dadosViewModel);//BadRequest();

        //    return Ok(dados);
        //}

        ////   [ClaimsAuthorize("Banco", "Atualizar")]
        //[AllowAnonymous]
        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<BancoViewModel>> Atualizar(int id, BancoViewModel dadosViewModel)
        //{          

        //    if (id != dadosViewModel.id_banco)
        //    {
        //        NotificarErro("O id informado não é o mesmo que foi passado na query");
        //        return CustomResponse(dadosViewModel);
        //    }

        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    await _bancoService.Alterar(_mapper.Map<Banco>(dadosViewModel));

        //    return CustomResponse(dadosViewModel);
        //}


        //// [ClaimsAuthorize("Produto", "Excluir")]
        //[AllowAnonymous]
        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<BancoViewModel>> Excluir(int id)
        //{
        //    var dados = await ObterDados(id);

        //    if (dados == null) return NotFound();

        //    await _bancoService.Excluir(id);

        //    return CustomResponse(dados);
        //}



        //private async Task<BancoViewModel> ObterDados(int id)
        //{
        //    return _mapper.Map<BancoViewModel>(await _bancoRepository.ObterPorIdPOC(id));
        //}

       
    }
}
