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
    //  [Route("api/Trabalhos")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Trabalhos")]
    public class TrabalhosController : MainController
    {
        private readonly ITrabalhoRepository _trabalhoRepository;
        private readonly ITrabalhoService _trabalhoService;
        private readonly IMapper _mapper;


        public TrabalhosController(ITrabalhoRepository trabalhoRepository,
                                     ITrabalhoService trabalhoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _trabalhoRepository = trabalhoRepository;
            _trabalhoService = trabalhoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos")]
        public async Task<IEnumerable<TrabalhoViewModel>> ObterTodos()
        {
            var Trabalho = _mapper.Map<IEnumerable<TrabalhoViewModel>>(await _trabalhoRepository.RetornarTodosPOC());

            return Trabalho;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<TrabalhoViewModel>> Consultar(string descricao = "")
        {
            var Trabalho = _mapper.Map<IEnumerable<TrabalhoViewModel>>(await _trabalhoRepository.ConsultarPOC(descricao));

            return Trabalho;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TrabalhoViewModel>> ObterPorId(int id)
        {
            var Trabalho = _mapper.Map<TrabalhoViewModel>(await _trabalhoRepository.ObterPorIdPOC(id));

            if (Trabalho == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return Trabalho;
        }


        // [ClaimsAuthorize("Trabalho", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TrabalhoViewModel>> Adicionar(TrabalhoViewModel TrabalhoViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var Trabalho = _mapper.Map<Trabalho>(TrabalhoViewModel);
            var result = await _trabalhoService.Incluir(Trabalho);

            if (!result) return CustomResponse(TrabalhoViewModel);//BadRequest();

            return Ok(Trabalho);
        }

        //   [ClaimsAuthorize("Trabalho", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TrabalhoViewModel>> Atualizar(int id, TrabalhoViewModel TrabalhoViewModel)
        {          

            if (id != TrabalhoViewModel.id_trabalho)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(TrabalhoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _trabalhoService.Alterar(_mapper.Map<Trabalho>(TrabalhoViewModel));

            return CustomResponse(TrabalhoViewModel);
        }


        // [ClaimsAuthorize("Trabalho", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TrabalhoViewModel>> Excluir(int id)
        {
            var Trabalho = await ObterTrabalho(id);

            if (Trabalho == null) return NotFound();

            await _trabalhoService.Excluir(id);

            return CustomResponse(Trabalho);
        }



        private async Task<TrabalhoViewModel> ObterTrabalho(int id)
        {
            return _mapper.Map<TrabalhoViewModel>(await _trabalhoRepository.ObterPorIdPOC(id));
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IEnumerable<TrabalhoViewModel>> GetAll()
        //{

        //    var Trabalho = _mapper.Map<IEnumerable<TrabalhoViewModel>>(await _trabalhoRepository.ObterTodos());

        //    return Trabalho;
        //}



    }
}
