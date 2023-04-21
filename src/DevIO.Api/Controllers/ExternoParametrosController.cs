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
   
    [Route("api/EstornoParametros")]
    //  [ApiVersion("1.0")]
    //  [Route("api/v{version:apiVersion}/externoparametros")]
    public class ExternoParametrosController : MainController
    {
        private readonly IParametrosRepository _parametrosRepository;
        private readonly IParametrosService _parametrosService;
        private readonly IMapper _mapper;


        public ExternoParametrosController(IParametrosRepository parametrosRepository,
                                     IParametrosService parametrosService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _parametrosRepository = parametrosRepository;
            _parametrosService = parametrosService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ParametrosViewModel>> ObterPorId(int id)
        {
            var parametro = _mapper.Map<ParametrosViewModel>(await _parametrosRepository.ObterPorIdExternoPOC(id));

            if (parametro == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return parametro;
        }

    }
}
