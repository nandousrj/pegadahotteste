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
    //  [Route("api/Grupos")]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Grupos")]
    public class GruposController : MainController
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IGrupoService _grupoService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public GruposController(IGrupoRepository grupoRepository,
                                     IGrupoService grupoService,
                                     INotificador notificador,
                                     IControleSistemaRepository controlesistemaRepository,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _grupoRepository = grupoRepository;
            _grupoService = grupoService;
            _controlesistemaRepository = controlesistemaRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("obtertodos/{status:int}")]
        public async Task<IEnumerable<GrupoViewModel>> ObterTodos(int status)
        {
            var dados = _mapper.Map<IEnumerable<GrupoViewModel>>(await _grupoRepository.RetornarTodosPOC(status));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GrupoViewModel>> Consultar(string descricao = "")
        {
            var dados = _mapper.Map<IEnumerable<GrupoViewModel>>(await _grupoRepository.ConsultarPOC(descricao));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GrupoViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<GrupoViewModel>(await _grupoRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Grupo", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<GrupoViewModel>> Adicionar(GrupoViewModel dadosViewModel)
        {
            //        if (!ModelState.IsValid) return CustomResponse(ModelState);

            var dados = _mapper.Map<Grupo>(dadosViewModel);


            //Random rnd = new Random();
            //int numero = rnd.Next(1, 10000000);
            //dados.id_grupo = numero;

            var result = await _grupoService.Incluir(dados);

            var imagemNome = dados.id_grupo + "_" + dadosViewModel.imagem_01;
            if (!UploadArquivo(dadosViewModel.imagem_01_upload, imagemNome))
            {
                return CustomResponse(dadosViewModel);
            }

            dadosViewModel.imagem_01 = imagemNome;


            //    if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Grupo", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GrupoViewModel>> Atualizar(int id, GrupoImagemViewModel dadosViewModel)
        {
            if (id != dadosViewModel.id_grupo)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await ValidarArquivo(dadosViewModel.imagem_01_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(dadosViewModel.imagem_01_upload, dadosViewModel.id_grupo))
            {
                return CustomResponse(ModelState);
            }

            dadosViewModel.imagem_01 = dadosViewModel.id_grupo + "_" + Util.TiraAcentos(dadosViewModel.imagem_01_upload.FileName);
            var dados = _mapper.Map<Grupo>(dadosViewModel);
            await _grupoService.Alterar(dados);

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Grupo", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GrupoViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _grupoService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<GrupoViewModel> ObterDados(int id)
        {
            return _mapper.Map<GrupoViewModel>(await _grupoRepository.ObterPorIdPOC(id));
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para este Grupo!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

          //  string caminhoImagem = Util.caminhoGrupo.ToString();
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_GRUPO"));

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + caminhoPastaImagemBanco.val_parametro.Replace("//", "/").ToString() + caminhoImagem.val_parametro.Replace("//", "/").ToString(), imgNome); //wwwroot

            if (System.IO.File.Exists(filePath))
            {
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            //public static bool IsBase64String(string base64)
            //{
            //    Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            //    return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
            //}
            //  long _fileSize = new System.IO.FileInfo(filePath).Length;
            long teste = filePath.Length;
            var fileSize = FileSizeHelpers.GetFileSizeFromBase64String(filePath, true);
            var fileSizeInKB = FileSizeHelpers.GetFileSizeFromBase64String(filePath, true, UnitsOfMeasurement.KiloByte);
            var fileSizeInMB = FileSizeHelpers.GetFileSizeFromBase64String(filePath, true, UnitsOfMeasurement.MegaByte);

            var characterCount = filePath.Length;
            var paddingCount = filePath.Substring(characterCount - 2, 2)
                                           .Count(c => c == '=');
            var valor = (3 * (characterCount / 4)) - paddingCount;



            //ver depois esssa nova maneira
            string base64String = "data:image/jpeg;base64......";

            var stringLength = base64String.Length - "data:image/png;base64,".Length;

            var sizeInBytes = 4 * Math.Ceiling(((float)stringLength / 3)) * 0.5624896334383812;
            var sizeInKb = sizeInBytes / 1000;
            ///////////////////////////////

            //// ver também verificando a imagem

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }
        #region UploadAlternativo

        // [ClaimsAuthorize("Grupo", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<ActionResult<GrupoViewModel>> AdicionarAlternativo(GrupoImagemViewModel grupoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //Random rnd = new Random();
            //int numero = rnd.Next(1, 10000000);
            //var imgPrefixo = numero.ToString();

            //  var imgPrefixo = Guid.NewGuid() + "_";

            if (!await ValidarArquivo(grupoViewModel.imagem_01_upload))
            {
                return CustomResponse(ModelState);
            }
            grupoViewModel.imagem_01 = "sera_alterado";
            var dados = _mapper.Map<Grupo>(grupoViewModel);
            await _grupoService.Incluir(dados);
            grupoViewModel.id_grupo = dados.id_grupo;

            if (!await UploadArquivoAlternativo(grupoViewModel.imagem_01_upload, grupoViewModel.id_grupo))
            {
                return CustomResponse(ModelState);
            }

            grupoViewModel.imagem_01 = grupoViewModel.id_grupo + "_" + Util.TiraAcentos(grupoViewModel.imagem_01_upload.FileName);
            await _grupoService.Alterar(_mapper.Map<Grupo>(grupoViewModel));

            return CustomResponse(grupoViewModel);
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

            string caminhoImagem = Util.caminhoGrupo.ToString();
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
                NotificarErro("Forneça uma imagem para este Grupo!");
                return false;
            }

            //  string caminhoImagem = Util.caminhoGrupo.ToString();
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_ANUNCIO"));

            var path = Path.Combine(Directory.GetCurrentDirectory(), "" + caminhoImagem, imgPrefixo + "_" + Util.TiraAcentos(arquivo.FileName)); //"wwwroot/app/demo-webapi/src/assets"

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
