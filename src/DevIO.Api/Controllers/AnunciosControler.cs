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
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Anuncios")]
    public class AnunciosController : MainController
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IAnuncioService _anuncioService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public AnunciosController(IAnuncioRepository anuncioRepository,
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
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<AnuncioViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<AnuncioViewModel>>(await _anuncioRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]       
        [HttpGet]
        public async Task<IEnumerable<AnuncioViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<AnuncioViewModel>>(await _anuncioRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AnuncioViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<AnuncioViewModel>(await _anuncioRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Anuncio", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AnuncioViewModel>> Adicionar(AnuncioViewModel dadosViewModel)
        {
    //        if (!ModelState.IsValid) return CustomResponse(ModelState);

            var dados = _mapper.Map<Anuncio>(dadosViewModel);

           

            var result = await _anuncioService.Incluir(dados);

            var imagemNome = dados.id_anuncio + "_" + dadosViewModel.imagem01;
            if (!UploadArquivo(dadosViewModel.imagem01_upload, imagemNome))
            {
                return CustomResponse(dadosViewModel);
            }

            dadosViewModel.imagem01 = imagemNome;


            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Anuncio", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AnuncioViewModel>> Atualizar(int id, AnuncioImagemViewModel dadosViewModel)
        {
            if (id != dadosViewModel.id_anuncio)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await ValidarArquivo(dadosViewModel.imagem01_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(dadosViewModel.imagem01_upload, dadosViewModel.id_anuncio))
            {
                return CustomResponse(ModelState);
            }

            dadosViewModel.imagem01 = dadosViewModel.id_anuncio + "_" + Util.TiraAcentos(dadosViewModel.imagem01_upload.FileName);
            var dados = _mapper.Map<Anuncio>(dadosViewModel);
            await _anuncioService.Alterar(dados);

            return CustomResponse(dadosViewModel);           
        }


        // [ClaimsAuthorize("Anuncio", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AnuncioViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _anuncioService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<AnuncioViewModel> ObterDados(int id)
        {
            return _mapper.Map<AnuncioViewModel>(await _anuncioRepository.ObterPorIdPOC(id));
        }


        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para este Anuncio!");
                return false;
            }

            //string caminhoImagem = Util.caminhoAnuncio.ToString();

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>( _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));            
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>( _controlesistemaRepository.ObterPorNomePOC("IMG_ANUNCIO"));


            var imageDataByteArray = Convert.FromBase64String(arquivo);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagem.val_parametro.Replace("//", "/").ToString(), imgNome); //wwwroot

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
        #region UploadAlternativo

        [ClaimsAuthorize("Anuncio", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<ActionResult<AnuncioViewModel>> AdicionarAlternativo(AnuncioImagemViewModel AnuncioViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await ValidarArquivo(AnuncioViewModel.imagem01_upload))
            {
                return CustomResponse(ModelState);
            }

            AnuncioViewModel.imagem01 = "sera_alterado";
            var dados = _mapper.Map<Anuncio>(AnuncioViewModel);
            await _anuncioService.Incluir(dados);
            AnuncioViewModel.id_anuncio = dados.id_anuncio;

            await _anuncioService.Incluir(_mapper.Map<Anuncio>(AnuncioViewModel));

            if (!await UploadArquivoAlternativo(AnuncioViewModel.imagem01_upload, AnuncioViewModel.id_anuncio))
            {
                return CustomResponse(ModelState);
            }

            AnuncioViewModel.imagem01 = AnuncioViewModel.id_anuncio + "_" + Util.TiraAcentos(AnuncioViewModel.imagem01_upload.FileName);
            await _anuncioService.Alterar(_mapper.Map<Anuncio>(AnuncioViewModel));          
            

            return CustomResponse(AnuncioViewModel);
        }

        [RequestSizeLimit(40000000)]
        //[DisableRequestSizeLimit]
        [HttpPost("imagem")]
        public ActionResult AdicionarImagem(IFormFile file)
        {
            return Ok(file);
        }

        private async Task<bool> ValidarArquivo(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                NotificarErro("Selecione uma imagem para este Grupo!");
                return false;
            }

            string caminhoImagem = Util.caminhoAnuncio.ToString();            
            int tamMaximoParamNome = Util.tamMaximoParamNome;
            int tamMaximoImagens = Util.tamMaximoImagens;

            string extensao = System.IO.Path.GetExtension(arquivo.FileName);
            if (extensao.ToUpper() == ".JPEG" || extensao.ToUpper() == ".JPG" || extensao.ToUpper() == ".PNG")
            {
                if (arquivo.Length < tamMaximoImagens)
                {
                    if (arquivo.FileName.Length <= tamMaximoParamNome)
                    {

                    }
                    else
                    {
                        NotificarErro("Nome da Imagem maior do que o suportado.");
                        return false;
                    }
                }
                else
                {
                    NotificarErro("Imagem maior do que o tamanho suportado.");
                    return false;
                }
            }
            else
            {
                NotificarErro("Extensão não aceita.");
                return false;
            }
            return true;
        }
        private async Task<bool> UploadArquivoAlternativo(IFormFile arquivo, int imgPrefixo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                NotificarErro("Forneça uma imagem para este Anúncio!");
                return false;
            }
           // string caminhoImagem = Util.caminhoAnuncio.ToString();
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_ANUNCIO"));

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagem.val_parametro.Replace("//", "/").ToString(), imgPrefixo + "_" + arquivo.FileName);

            //if (System.IO.File.Exists(path))
            //{
            //    NotificarErro("Já existe um arquivo com este nome!");
            //    return false;
            //}

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        #endregion



    }
}
