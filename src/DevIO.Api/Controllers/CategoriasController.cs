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
    //  [Route("api/categorias")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categorias")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;


        public CategoriasController(ICategoriaRepository categoriaRepository,
                                     ICategoriaService categoriaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos")]
        public async Task<IEnumerable<CategoriaViewModel>> ObterTodos()
        {
            var categoria = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.RetornarTodosPOC());

            return categoria;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<CategoriaViewModel>> Consultar(string descricao = "")
        {
            var categoria = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ConsultarPOC(descricao));

            return categoria;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaViewModel>> ObterPorId(int id)
        {
            var categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorIdPOC(id));

            if (categoria == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return categoria;
        }


        // [ClaimsAuthorize("Categoria", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CategoriaViewModel>> Adicionar(CategoriaViewModel categoriaViewModel)
        {
         //   if (ModelState.IsValid) return BadRequest();
            var categoria = _mapper.Map<Categoria>(categoriaViewModel);
            var result = await _categoriaService.Incluir(categoria);

            if (!result) return CustomResponse(categoriaViewModel);//BadRequest();

            return Ok(categoria);
        }

        //   [ClaimsAuthorize("Categoria", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaViewModel>> Atualizar(int id, CategoriaViewModel categoriaViewModel)
        {          

            if (id != categoriaViewModel.id_categoria)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(categoriaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _categoriaService.Alterar(_mapper.Map<Categoria>(categoriaViewModel));

            return CustomResponse(categoriaViewModel);
        }


        // [ClaimsAuthorize("Categoria", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaViewModel>> Excluir(int id)
        {
            var categoria = await ObterCategoria(id);

            if (categoria == null) return NotFound();

            await _categoriaService.Excluir(id);

            return CustomResponse(categoria);
        }



        private async Task<CategoriaViewModel> ObterCategoria(int id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorIdPOC(id));
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IEnumerable<CategoriaViewModel>> GetAll()
        //{

        //    var estilo = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());

        //    return estilo;
        //}



    }
}
