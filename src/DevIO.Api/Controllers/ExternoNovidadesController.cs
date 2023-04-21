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
using System.Drawing;
using FileSizeFromBase64.NET;

namespace DevIO.Api.Controllers
{
    //  [Route("api/ExternoNovidades")]
    //  [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ExternoNovidades")]
    public class ExternoNovidadesController : MainController
    {
        private readonly INovidadeRepository _novidadeRepository;
        private readonly INovidadeService _novidadeService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public ExternoNovidadesController(INovidadeRepository novidadeRepository,
                                     INovidadeService novidadeService,
                                     IControleSistemaRepository controlesistemaRepository,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _novidadeRepository = novidadeRepository;
            _novidadeService = novidadeService;
            _controlesistemaRepository = controlesistemaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("RetornaDadosAtivos")]
        public async Task<IEnumerable<NovidadeViewModel>> RetornarDadosAtivos(int id_categoria)
        {

            var dados = _mapper.Map<IEnumerable<NovidadeViewModel>>(await _novidadeRepository.ObterDadosAtivos(id_categoria));
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBancoNovidade = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_NOVIDADE"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

            // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoImagem + imgPrefixo.ToString(), imgPrefixo + "_" + Util.TiraAcentos(arquivo.FileName)); //"wwwroot/app/demo-webapi/src/assets"
            foreach (NovidadeViewModel i in dados)
            {
                if (!string.IsNullOrEmpty(i.imagem_01))
                {
                    i.imagem_01 = "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBancoNovidade.val_parametro.Replace("//", "/").ToString() + "/" + i.imagem_01;
                }
                else
                {
                    i.imagem_01 = null;
                };

                if (!string.IsNullOrEmpty(i.GarotaCategoria.imagem_01))
                {
                    i.GarotaCategoria.imagem_01 = "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagemBanco.val_parametro.Replace("//", "/").ToString() + i.GarotaCategoria.imagem_01;
                }
                else
                {
                    i.GarotaCategoria.imagem_01 = null;
                };
            }

            return dados;
        }






    }
}
