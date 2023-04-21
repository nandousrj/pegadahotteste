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
    //  [Route("api/tipocontatos")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tipocontatos")]
    public class TipoContatosController : MainController
    {
        private readonly ITipoContatoRepository _tipocontatoRepository;
        private readonly ITipoContatoService _tipocontatoService;
        private readonly IMapper _mapper;


        public TipoContatosController(ITipoContatoRepository tipocontatoRepository,
                                     ITipoContatoService tipocontatoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _tipocontatoRepository = tipocontatoRepository;
            _tipocontatoService = tipocontatoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos")]
        public async Task<IEnumerable<TipoContatoViewModel>> ObterTodos()
        {
            var tipocontato = _mapper.Map<IEnumerable<TipoContatoViewModel>>(await _tipocontatoRepository.RetornarTodosPOC());

            return tipocontato;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<TipoContatoViewModel>> Consultar(string descricao = "")
        {
            var tipocontato = _mapper.Map<IEnumerable<TipoContatoViewModel>>(await _tipocontatoRepository.ConsultarPOC(descricao));

            return tipocontato;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoContatoViewModel>> ObterPorId(int id)
        {
            var tipocontato = _mapper.Map<TipoContatoViewModel>(await _tipocontatoRepository.ObterPorIdPOC(id));

            if (tipocontato == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return tipocontato;
        }


        // [ClaimsAuthorize("TipoContato", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TipoContatoViewModel>> Adicionar(TipoContatoViewModel tipocontatoViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var tipocontato = _mapper.Map<TipoContato>(tipocontatoViewModel);
            var result = await _tipocontatoService.Incluir(tipocontato);

            if (!result) return CustomResponse(tipocontatoViewModel);//BadRequest();

            return Ok(tipocontato);
        }

        //   [ClaimsAuthorize("TipoContato", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TipoContatoViewModel>> Atualizar(int id, TipoContatoViewModel tipocontatoViewModel)
        {          

            if (id != tipocontatoViewModel.id_tipo_contato)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(tipocontatoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tipocontatoService.Alterar(_mapper.Map<TipoContato>(tipocontatoViewModel));

            return CustomResponse(tipocontatoViewModel);
        }


        // [ClaimsAuthorize("TipoContato", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TipoContatoViewModel>> Excluir(int id)
        {
            var tipocontato = await ObterTipoContato(id);

            if (tipocontato == null) return NotFound();

            await _tipocontatoService.Excluir(id);

            return CustomResponse(tipocontato);
        }



        private async Task<TipoContatoViewModel> ObterTipoContato(int id)
        {
            return _mapper.Map<TipoContatoViewModel>(await _tipocontatoRepository.ObterPorIdPOC(id));
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
