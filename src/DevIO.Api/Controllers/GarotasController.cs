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
    //  [Route("api/PrincipalSite")]
      [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Garotas")]
    public class GarotasController : MainController
    {
     
        private readonly IGarotaRepository _garotaRepository;
        private readonly IGarotaService _garotaService;
        private readonly IMapper _mapper;


        public GarotasController(IGarotaRepository garotaRepository,
                                     IGarotaService garotaService,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _garotaRepository = garotaRepository;
            _garotaService = garotaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<GarotaViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<GarotaViewModel>>(await _garotaRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GarotaViewModel>> Consultar(string nome = "")
        {
            var dados = _mapper.Map<IEnumerable<GarotaViewModel>>(await _garotaRepository.ConsultarPOC(nome));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet]
        private async Task<GarotaViewModel> ObterDados(int id)
        {
            return _mapper.Map<GarotaViewModel>(await _garotaRepository.ObterPorIdPOC(id));
        }

        [AllowAnonymous]
        [HttpGet("RetornaDadosGarotaSenha")]
        private async Task<GarotaViewModel> RetornarDadosGarotaSenha(string email, string senha)
        {
            return _mapper.Map<GarotaViewModel>(await _garotaRepository.RetornarDadosGarotaSenha(email, senha));
        }

        [AllowAnonymous]
        [HttpGet]
        private async Task<GarotaViewModel> RetornarDadosGarotaNascimentoSenha(string nascimento, string senha)
        {
            return _mapper.Map<GarotaViewModel>(await _garotaRepository.RetornarDadosGarotaNascimentoSenha(nascimento, senha));
        }


        [AllowAnonymous]
        [HttpGet]
        private async Task<GarotaViewModel> VerificarCPFGarotaExistente(string cpf)
        {
            return _mapper.Map<GarotaViewModel>(await _garotaRepository.VerificarCPFGarotaExistente(cpf));
        }


        [AllowAnonymous]
        [HttpGet]
        private async Task<GarotaViewModel> RetornarEsqueceuSenha(string cpf, string email)
        {
            return _mapper.Map<GarotaViewModel>(await _garotaRepository.RetornarEsqueceuSenha(cpf, email));
        }


        [AllowAnonymous]
        [HttpGet]
        private async Task<GarotaViewModel> RetornarEsqueceuSenhaNascimento(string nascimento, string email)
        {
            return _mapper.Map<GarotaViewModel>(await _garotaRepository.RetornarEsqueceuSenhaNascimento(nascimento, email));
        }


        [AllowAnonymous]
        [HttpGet]
        private async Task<GarotaViewModel> RetornarEsqueceuEmail(string cpf)
        {
            return _mapper.Map<GarotaViewModel>(await _garotaRepository.RetornarEsqueceuEmail(cpf));
        }


        // [ClaimsAuthorize("Grupo", "Excluir")]
        //[AllowAnonymous]
        //[HttpDelete("{id:int}")]
        //public async Task<ActionResult<GrupoViewModel>> Excluir(int id)
        //{
        //    var dados = await ObterDados(id);

        //    if (dados == null) return NotFound();

        //    await _grupoService.Excluir(id);

        //    return CustomResponse(dados);
        //}


        #region UploadAlternativo

        // [ClaimsAuthorize("Garota", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<ActionResult<GarotaViewModel>> AdicionarAlternativo(GarotaImagemViewModel GarotaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);          

            if (!await ValidarArquivo(GarotaViewModel.imagem_doc_frente_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaViewModel.imagem_doc_tras_upload))
            {
                return CustomResponse(ModelState);
            }
            GarotaViewModel.imagem_doc_frente = "sera_alterado_f";
            GarotaViewModel.imagem_doc_tras = "sera_alterado_t";

            var dados = _mapper.Map<Garota>(GarotaViewModel);
            await _garotaService.Incluir(dados);
            GarotaViewModel.id_garota = dados.id_garota;

            if (!await UploadArquivoAlternativo(GarotaViewModel.imagem_doc_frente_upload, GarotaViewModel.id_garota))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaViewModel.imagem_doc_tras_upload, GarotaViewModel.id_garota))
            {
                return CustomResponse(ModelState);
            }

            GarotaViewModel.imagem_doc_frente = GarotaViewModel.id_garota + "_" + Util.TiraAcentos(GarotaViewModel.imagem_doc_frente_upload.FileName);
            GarotaViewModel.imagem_doc_tras = GarotaViewModel.id_garota + "_" + Util.TiraAcentos(GarotaViewModel.imagem_doc_tras_upload.FileName);
            await _garotaService.Alterar(_mapper.Map<Garota>(GarotaViewModel));

            return CustomResponse(GarotaViewModel);
        }



        //   [ClaimsAuthorize("Garota", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GarotaViewModel>> Atualizar(int id, GarotaImagemViewModel dadosViewModel)
        {
            if (id != dadosViewModel.id_garota)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);


            if (!(dadosViewModel.imagem_doc_frente_upload == null || dadosViewModel.imagem_doc_frente_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_doc_frente_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_doc_frente_upload, dadosViewModel.id_garota))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_doc_frente = dadosViewModel.id_garota + "_" + Util.TiraAcentos(dadosViewModel.imagem_doc_frente_upload.FileName);
            }

            if (!(dadosViewModel.imagem_doc_tras_upload == null || dadosViewModel.imagem_doc_tras_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_doc_tras_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_doc_tras_upload, dadosViewModel.id_garota))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_doc_tras = dadosViewModel.id_garota + "_" + Util.TiraAcentos(dadosViewModel.imagem_doc_tras_upload.FileName);
            }
            
            var dados = _mapper.Map<Garota>(dadosViewModel);
            await _garotaService.Alterar(dados);

            return CustomResponse(dadosViewModel);
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
                NotificarErro("Selecione uma imagem para este Garota!");
                return false;
            }

            string caminhoImagem = Util.caminhoDocFoto.ToString();
            //  int tamMaximoDoc = Util.tamMaximoDoc;
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
                        NotificarErro("Nome da Imagem 01 maior do que o suportado.");
                        return false;
                    }
                }
                else
                {
                    NotificarErro("Imagem 01 maior do que o tamanho suportado.");
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
                NotificarErro("Forneça uma imagem para este Garota!");
                return false;
            }

            string caminhoImagem = Util.caminhoDocFoto.ToString();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoImagem + imgPrefixo.ToString(), imgPrefixo + "_" + Util.TiraAcentos(arquivo.FileName)); //"wwwroot/app/demo-webapi/src/assets"

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
