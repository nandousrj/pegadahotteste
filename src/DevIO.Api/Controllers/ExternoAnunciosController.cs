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
    //  [Route("api/Anuncios")]
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ExternoAnuncios")]
    public class ExternoAnunciosController : MainController
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IAnuncioService _anuncioService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public ExternoAnunciosController(IAnuncioRepository anuncioRepository,
                                     IAnuncioService anuncioService,
                                     IControleSistemaRepository controlesistemaRepository,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _anuncioRepository = anuncioRepository;
            _anuncioService = anuncioService;
            _controlesistemaRepository = controlesistemaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("ObterTodosAtivos")]
        public async Task<IEnumerable<AnuncioViewModel>> ObterTodosAtivos()
        {
            var dados = _mapper.Map<IEnumerable<AnuncioViewModel>>(await _anuncioRepository.RetornarTodosAtivosPOC());

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_ANUNCIO"));
                       
            foreach (AnuncioViewModel i in dados)
            {
                i.imagem01 = "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + "/" + i.imagem01;
            }

            return dados;
        }


    }
}
