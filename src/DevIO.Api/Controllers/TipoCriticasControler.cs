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
    //  [Route("api/TipoCriticas")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/TipoCriticas")]
    public class TipoCriticasController : MainController
    {
        private readonly ITipoCriticaRepository _tipocriticaRepository;
        private readonly ITipoCriticaService _tipocriticaService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public TipoCriticasController(ITipoCriticaRepository tipocriticaRepository,
                                     ITipoCriticaService tipocriticaService,
                                     IControleSistemaRepository controlesistemaRepository,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _tipocriticaRepository = tipocriticaRepository;
            _tipocriticaService = tipocriticaService;
            _controlesistemaRepository = controlesistemaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<TipoCriticaViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<TipoCriticaViewModel>>(await _tipocriticaRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<TipoCriticaViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<TipoCriticaViewModel>>(await _tipocriticaRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoCriticaViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<TipoCriticaViewModel>(await _tipocriticaRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("TipoCritica", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TipoCriticaViewModel>> Adicionar(TipoCriticaViewModel dadosViewModel)
        {
            //        if (!ModelState.IsValid) return CustomResponse(ModelState);

            var dados = _mapper.Map<TipoCritica>(dadosViewModel);



            var result = await _tipocriticaService.Incluir(dados);

            var imagemNome = dados.id_tipo_critica + "_" + dadosViewModel.imagem;
            if (!UploadArquivo(dadosViewModel.imagem_upload, imagemNome))
            {
                return CustomResponse(dadosViewModel);
            }

            dadosViewModel.imagem = imagemNome;


            if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("TipoCritica", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TipoCriticaViewModel>> Atualizar(int id, TipoCriticaImagemViewModel dadosViewModel)
        {
            if (id != dadosViewModel.id_tipo_critica)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await ValidarArquivo(dadosViewModel.imagem_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(dadosViewModel.imagem_upload, dadosViewModel.id_tipo_critica))
            {
                return CustomResponse(ModelState);
            }

            dadosViewModel.imagem = dadosViewModel.id_tipo_critica + "_" + Util.TiraAcentos(dadosViewModel.imagem_upload.FileName);
            var dados = _mapper.Map<TipoCritica>(dadosViewModel);
            await _tipocriticaService.Alterar(dados);

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("TipoCritica", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TipoCriticaViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _tipocriticaService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<TipoCriticaViewModel> ObterDados(int id)
        {
            return _mapper.Map<TipoCriticaViewModel>(await _tipocriticaRepository.ObterPorIdPOC(id));
        }


        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para este TipoCritica!");
                return false;
            }

            // string caminhoImagem = Util.caminhoEmotions.ToString();
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_EMOTIONS"));

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

        [ClaimsAuthorize("TipoCritica", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<ActionResult<TipoCriticaViewModel>> AdicionarAlternativo(TipoCriticaImagemViewModel tipoCriticaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await ValidarArquivo(tipoCriticaViewModel.imagem_upload))
            {
                return CustomResponse(ModelState);
            }

            tipoCriticaViewModel.imagem = "sera_alterado";
            var dados = _mapper.Map<TipoCritica>(tipoCriticaViewModel);
            await _tipocriticaService.Incluir(dados);
            tipoCriticaViewModel.id_tipo_critica = dados.id_tipo_critica;

            await _tipocriticaService.Incluir(_mapper.Map<TipoCritica>(tipoCriticaViewModel));

            if (!await UploadArquivoAlternativo(tipoCriticaViewModel.imagem_upload, tipoCriticaViewModel.id_tipo_critica))
            {
                return CustomResponse(ModelState);
            }

            tipoCriticaViewModel.imagem = tipoCriticaViewModel.id_tipo_critica + "_" + Util.TiraAcentos(tipoCriticaViewModel.imagem_upload.FileName);
            await _tipocriticaService.Alterar(_mapper.Map<TipoCritica>(tipoCriticaViewModel));


            return CustomResponse(tipoCriticaViewModel);
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

            string caminhoImagem = Util.caminhoEmotions.ToString();
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
                NotificarErro("Forneça uma imagem para este Tipo Critica!");
                return false;
            }
            // string caminhoImagem = Util.caminhoEmotions.ToString();
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_EMOTIONS"));

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
