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
    //  [Route("api/Novidades")]
     [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Novidades")]
    public class NovidadesController : MainController
    {
        private readonly INovidadeRepository _novidadeRepository;
        private readonly INovidadeService _novidadeService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public NovidadesController(INovidadeRepository novidadeRepository,
                                     INovidadeService novidadeService,
                                     INotificador notificador,
                                     IControleSistemaRepository controlesistemaRepository,
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

            return dados;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<NovidadeViewModel>> Consultar()
        {
            var dados = _mapper.Map<IEnumerable<NovidadeViewModel>>(await _novidadeRepository.ConsultarPOC());

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NovidadeViewModel>> ObterPorId(int id)
        {
            var dados = _mapper.Map<NovidadeViewModel>(await _novidadeRepository.ObterPorIdPOC(id));

            if (dados == null) return NotFound(); //pra retornar NotFound tem que ter Action Result

            return dados;
        }


        // [ClaimsAuthorize("Novidade", "Adicionar")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<NovidadeViewModel>> Adicionar(NovidadeViewModel dadosViewModel)
        {
            //        if (!ModelState.IsValid) return CustomResponse(ModelState);

            var dados = _mapper.Map<Novidade>(dadosViewModel);


            //Random rnd = new Random();
            //int numero = rnd.Next(1, 10000000);
            //dados.id_Novidade = numero;

            var result = await _novidadeService.Incluir(dados);

            var imagemNome = dados.id_novidade + "_" + dadosViewModel.imagem_01;
            if (!UploadArquivo(dadosViewModel.imagem_01_upload, imagemNome))
            {
                return CustomResponse(dadosViewModel);
            }

            dadosViewModel.imagem_01 = imagemNome;


            //    if (!result) return CustomResponse(dadosViewModel);//BadRequest();

            return Ok(dados);
        }

        //   [ClaimsAuthorize("Novidade", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<NovidadeViewModel>> Atualizar(int id, NovidadeImagemViewModel dadosViewModel)
        {
            if (id != dadosViewModel.id_novidade)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await ValidarArquivo(dadosViewModel.imagem_01_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(dadosViewModel.imagem_01_upload, dadosViewModel.id_novidade))
            {
                return CustomResponse(ModelState);
            }

            dadosViewModel.imagem_01 = dadosViewModel.id_novidade + "_" + Util.TiraAcentos(dadosViewModel.imagem_01_upload.FileName);
            var dados = _mapper.Map<Novidade>(dadosViewModel);
            await _novidadeService.Alterar(dados);

            return CustomResponse(dadosViewModel);
        }


        // [ClaimsAuthorize("Novidade", "Excluir")]
        [AllowAnonymous]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<NovidadeViewModel>> Excluir(int id)
        {
            var dados = await ObterDados(id);

            if (dados == null) return NotFound();

            await _novidadeService.Excluir(id);

            return CustomResponse(dados);
        }



        private async Task<NovidadeViewModel> ObterDados(int id)
        {
            return _mapper.Map<NovidadeViewModel>(await _novidadeRepository.ObterPorIdPOC(id));
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotificarErro("Forneça uma imagem para este Novidade!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);

         //   string caminhoImagem = Util.caminhoNovidade.ToString();
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_NOVIDADE"));

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

        // [ClaimsAuthorize("Novidade", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<ActionResult<NovidadeViewModel>> AdicionarAlternativo(NovidadeImagemViewModel NovidadeViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            //Random rnd = new Random();
            //int numero = rnd.Next(1, 10000000);
            //var imgPrefixo = numero.ToString();

            //  var imgPrefixo = Guid.NewGuid() + "_";

            if (!await ValidarArquivo(NovidadeViewModel.imagem_01_upload))
            {
                return CustomResponse(ModelState);
            }
            NovidadeViewModel.imagem_01 = "sera_alterado";
            var dados = _mapper.Map<Novidade>(NovidadeViewModel);
            await _novidadeService.Incluir(dados);
            NovidadeViewModel.id_novidade = dados.id_novidade;

            if (!await UploadArquivoAlternativo(NovidadeViewModel.imagem_01_upload, NovidadeViewModel.id_novidade))
            {
                return CustomResponse(ModelState);
            }

            NovidadeViewModel.imagem_01 = NovidadeViewModel.id_novidade + "_" + Util.TiraAcentos(NovidadeViewModel.imagem_01_upload.FileName);
            await _novidadeService.Alterar(_mapper.Map<Novidade>(NovidadeViewModel));

            return CustomResponse(NovidadeViewModel);
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
                NotificarErro("Selecione uma imagem para este Novidade!");
                return false;
            }

            string caminhoImagem = Util.caminhoNovidade.ToString();
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
                NotificarErro("Forneça uma imagem para este Novidade!");
                return false;
            }

           // string caminhoImagem = Util.caminhoNovidade.ToString();
            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagem = _mapper.Map<ControleSistemaViewModel>(_controlesistemaRepository.ObterPorNomePOC("IMG_NOVIDADE"));

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
