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
    
    [Authorize]
    [Route("api/estilos")]
    //  [ApiVersion("1.0")]
    //  [Route("api/v{version:apiVersion}/estilos")]
    public class EstilosController : MainController
    {
        private readonly IEstiloRepository _estiloRepository;
        private readonly IEstiloService _estiloService;
        private readonly IMapper _mapper;


        public EstilosController(IEstiloRepository estiloRepository,
                                     IEstiloService estiloService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _estiloRepository = estiloRepository;
            _estiloService = estiloService;
            _mapper = mapper;
        }

     //   [AllowAnonymous]
        [HttpGet("obtertodos")]
        public async Task<IEnumerable<EstiloViewModel>> ObterTodos()
        {
            var estilo = _mapper.Map<IEnumerable<EstiloViewModel>>(await _estiloRepository.RetornarTodosPOC());

            return estilo;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<EstiloViewModel>> Consultar(string descricao = "")
        {
            var estilo = _mapper.Map<IEnumerable<EstiloViewModel>>(await _estiloRepository.ConsultarPOC(descricao));

            return estilo;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EstiloViewModel>> ObterPorId(int id)
        {
            var estilo = _mapper.Map<EstiloViewModel>(await _estiloRepository.ObterPorIdPOC(id));

            if (estilo == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return estilo;
        }


        // [ClaimsAuthorize("Estilo", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<EstiloViewModel>> Adicionar(EstiloViewModel estiloViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var estilo = _mapper.Map<Estilo>(estiloViewModel);
            var result = await _estiloService.Incluir(estilo);

            if (!result) return CustomResponse(estiloViewModel);//BadRequest();

            return Ok(estilo);
        }

        //   [ClaimsAuthorize("Estilo", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<EstiloViewModel>> Atualizar(int id, EstiloViewModel estiloViewModel)
        {          

            if (id != estiloViewModel.id_estilo)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(estiloViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _estiloService.Alterar(_mapper.Map<Estilo>(estiloViewModel));

            return CustomResponse(estiloViewModel);
        }


        // [ClaimsAuthorize("Estilo", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EstiloViewModel>> Excluir(int id)
        {
            var estilo = await ObterEstilo(id);

            if (estilo == null) return NotFound();

            await _estiloService.Excluir(id);

            return CustomResponse(estilo);
        }



        private async Task<EstiloViewModel> ObterEstilo(int id)
        {
            return _mapper.Map<EstiloViewModel>(await _estiloRepository.ObterPorIdPOC(id));
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IEnumerable<EstiloViewModel>> GetAll()
        //{

        //    var estilo = _mapper.Map<IEnumerable<EstiloViewModel>>(await _estiloRepository.ObterTodos());

        //    return estilo;
        //}



    }
}
