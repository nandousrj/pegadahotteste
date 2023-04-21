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
    //  [Route("api/TipoLogs")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/TipoLogs")]
    public class TipoLogsController : MainController
    {
        private readonly ITipoLogRepository _tipologRepository;
        private readonly ITipoLogService _tipologService;
        private readonly IMapper _mapper;


        public TipoLogsController(ITipoLogRepository tipologRepository,
                                     ITipoLogService tipologService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _tipologRepository = tipologRepository;
            _tipologService = tipologService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos")]
        public async Task<IEnumerable<TipoLogViewModel>> ObterTodos()
        {
            var TipoLog = _mapper.Map<IEnumerable<TipoLogViewModel>>(await _tipologRepository.RetornarTodosPOC());

            return TipoLog;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<TipoLogViewModel>> Consultar(string descricao = "")
        {
            var TipoLog = _mapper.Map<IEnumerable<TipoLogViewModel>>(await _tipologRepository.ConsultarPOC(descricao));

            return TipoLog;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoLogViewModel>> ObterPorId(int id)
        {
            var TipoLog = _mapper.Map<TipoLogViewModel>(await _tipologRepository.ObterPorIdPOC(id));

            if (TipoLog == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return TipoLog;
        }


        // [ClaimsAuthorize("TipoLog", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TipoLogViewModel>> Adicionar(TipoLogViewModel TipoLogViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var TipoLog = _mapper.Map<TipoLog>(TipoLogViewModel);
            var result = await _tipologService.Incluir(TipoLog);

            if (!result) return CustomResponse(TipoLogViewModel);//BadRequest();

            return Ok(TipoLog);
        }

        //   [ClaimsAuthorize("TipoLog", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TipoLogViewModel>> Atualizar(int id, TipoLogViewModel dadosViewModel)
        {          

            if (id != dadosViewModel.id_tipo_log)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _tipologService.Alterar(_mapper.Map<TipoLog>(dadosViewModel));

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("TipoLog", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TipoLogViewModel>> Excluir(int id)
        {
            var TipoLog = await ObterTipoLog(id);

            if (TipoLog == null) return NotFound();

            await _tipologService.Excluir(id);

            return CustomResponse(TipoLog);
        }



        private async Task<TipoLogViewModel> ObterTipoLog(int id)
        {
            return _mapper.Map<TipoLogViewModel>(await _tipologRepository.ObterPorIdPOC(id));
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IEnumerable<TipoLogViewModel>> GetAll()
        //{

        //    var TipoLog = _mapper.Map<IEnumerable<TipoLogViewModel>>(await _TipoLogRepository.ObterTodos());

        //    return TipoLog;
        //}



    }
}
