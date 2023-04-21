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
   
    [Route("api/externoestilos")]
    //  [ApiVersion("1.0")]
    //  [Route("api/v{version:apiVersion}/externoestilos")]
    public class ExternoEstilosController : MainController
    {
        private readonly IEstiloRepository _estiloRepository;
        private readonly IEstiloService _estiloService;
        private readonly IVisualizacaoRepository _visualizacaoRepository;
        private readonly IVisualizacaoService _visualizacaoService;
        private readonly IMapper _mapper;


        public ExternoEstilosController(IEstiloRepository estiloRepository,
                                     IEstiloService estiloService,
                                     IVisualizacaoRepository visualizacaoRepository,
                                     IVisualizacaoService visualizacaoService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _estiloRepository = estiloRepository;
            _estiloService = estiloService;
            _visualizacaoRepository = visualizacaoRepository;
            _visualizacaoService = visualizacaoService;
            _mapper = mapper;
        }

        //index
     //   [AllowAnonymous]
        [HttpGet("obtertodos")]
        public async Task<IEnumerable<EstiloViewModel>> ObterTodos()
        {

            int dados = _mapper.Map<int>(await _visualizacaoRepository.RetornarTotalVisualizacaoSitePOC());
            var estilo = _mapper.Map<IEnumerable<EstiloViewModel>>(await _estiloRepository.RetornarTodosPOC());

            return estilo;
        }

    }
}
