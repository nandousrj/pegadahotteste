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
    //  [Route("api/externozonas")]  
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/externozonas")]
    public class ExternoZonasController : MainController
    {
        private readonly IZonaRepository _zonaRepository;
        private readonly IZonaService _zonaService;
        private readonly IMapper _mapper;


        public ExternoZonasController(IZonaRepository zonaRepository,
                                     IZonaService zonaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _zonaRepository = zonaRepository;
            _zonaService = zonaService;
            _mapper = mapper;
        }

        ///index
        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<ZonaViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<ZonaViewModel>>(await _zonaRepository.RetornarTodosPOC(status));

            return dados;
        }
       
    }
}
