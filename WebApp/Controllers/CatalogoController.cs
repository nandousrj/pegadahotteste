using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class CatalogoController : MainController
    {

        private readonly IExternoGarotaCategoriaService _garotacategoriaService;
        

        public CatalogoController(IExternoGarotaCategoriaService garotacategoriaService)
        {
            _garotacategoriaService = garotacategoriaService;
        }

        [HttpGet]
        [Route("")]
      ///  [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var pegadoras = await _garotacategoriaService.RetornarDadosPrincipal(1, "", 0, "", 0);      
            return View(pegadoras);
        }

        [HttpGet]
        [Route("catalogo/pegadora/{id_garota_categoria}")]
        public async Task<IActionResult> Pegadora(int id_garota_categoria)
        {
            var pegadora = await _garotacategoriaService.RetornarDadosDetalhe(id_garota_categoria, 1);
            return View(pegadora);
        }
    }
}
