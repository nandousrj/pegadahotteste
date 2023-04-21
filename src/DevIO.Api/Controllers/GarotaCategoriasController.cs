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
    [Route("api/v{version:apiVersion}/GarotaCategorias")]
    public class GarotaCategoriaCategoriasController : MainController
    {

        private readonly IGarotaCategoriaRepository _garotacategoriaRepository;
        private readonly IGarotaCategoriaService _garotacategoriaService;
        private readonly IControleSistemaRepository _controlesistemaRepository;
        private readonly IMapper _mapper;


        public GarotaCategoriaCategoriasController(IGarotaCategoriaRepository garotacategoriaRepository,
                                     IGarotaCategoriaService garotacategoriaService,
                                     IControleSistemaRepository controlesistemaRepository,
                                     INotificador notificador,
                                     IMapper mapper//,
                                     /*IUser user*/) : base(notificador/*, user*/)
        {
            _garotacategoriaRepository = garotacategoriaRepository;
            _garotacategoriaService = garotacategoriaService;
            _controlesistemaRepository = controlesistemaRepository;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GarotaCategoriaViewModel>> Consultar(string nome, string apelido, string cpf)
        {
            var dados = _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.ConsultarPOC(nome, apelido, cpf));

            return dados;
        }

        [AllowAnonymous]
        [HttpGet("obterporid/{id:int}")]
        public async Task<GarotaCategoriaViewModel> ObterPorId(int id)
        {
            var dados = _mapper.Map<GarotaCategoriaViewModel>(await _garotacategoriaRepository.ObterPorIdPOC(id));

            return dados;
        }



        [AllowAnonymous]
        [HttpGet("RetornarDadosGarotaCategoriaUsuario")]
        private async Task<GarotaCategoriaViewModel> RetornarDadosGarotaCategoriaUsuario(int id_categoria, string desc_zona, int destaque)
        {
            return _mapper.Map<GarotaCategoriaViewModel>(await _garotacategoriaRepository.RetornarDadosGarotaCategoriaUsuario(id_categoria, desc_zona, destaque));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosGarotaCategoriaUsuarioPerfil")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosGarotaCategoriaUsuarioPerfil(int id_usuario)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosGarotaCategoriaUsuarioPerfil(id_usuario));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosGarotaCategoriaLogada")]
        private async Task<GarotaCategoriaViewModel> RetornarDadosGarotaCategoriaLogada(int id_garota)
        {
            return _mapper.Map<GarotaCategoriaViewModel>(await _garotacategoriaRepository.RetornarDadosGarotaCategoriaLogada(id_garota));
        }


        [AllowAnonymous]
        [HttpGet("RetornarDadosPrincipal")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosPrincipal(int id_categoria, string desc_zona, int ind_destaque, string apelido, int id_estilo)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosPrincipal(id_categoria, desc_zona, ind_destaque, apelido, id_estilo));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosGrupo")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosGrupo(int id_grupo)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosGrupo(id_grupo));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosVisualizadas")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosVisualizadas(int id_categoria)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosVisualizadas(id_categoria));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosVisualizadasTodos")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosVisualizadasTodos(int id_categoria)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosVisualizadasTodos(id_categoria));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosCurtidas")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosCurtidas(int id_categoria)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidas(id_categoria));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosCurtidasTodos")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosCurtidasTodos(int id_categoria)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasTodos(id_categoria));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosCurtidasVisualizadasTotal")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosCurtidasVisualizadasTotal(int id_categoria)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasVisualizadasTotal(id_categoria));
        }

        [AllowAnonymous]
        [HttpGet("RetornarDadosCurtidasVisualizadasTotalTodos")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosCurtidasVisualizadasTotalTodos(int id_categoria)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarDadosCurtidasVisualizadasTotalTodos(id_categoria));
        }


        [AllowAnonymous]
        [HttpGet("RetornarDadosDetalhe")]
        private async Task<GarotaCategoriaViewModel> RetornarDadosDetalhe(int id_garota_categoria, int id_categoria)
        {
            return _mapper.Map<GarotaCategoriaViewModel>(await _garotacategoriaRepository.RetornarDadosDetalhe(id_garota_categoria, id_categoria));
        }

        [AllowAnonymous]
        [HttpGet("RelatorioAluguel")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RelatorioAluguel()
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RelatorioAluguel());
        }

        [AllowAnonymous]
        [HttpGet("ConsultarGarotaPorId")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> ConsultarGarotaPorId(int id_garota)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.ConsultarGarotaPorId(id_garota));
        }

        [AllowAnonymous]
        [HttpGet("RetornarTodos")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarTodos(int status)
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornarTodosPOC(status));
        }


        [AllowAnonymous]
        [HttpGet("AtualizarPromocaoMesGratis")]
        private async Task<int> AtualizarPromocaoMesGratis()
        {
            return await _garotacategoriaRepository.AtualizarPromocaoMesGratis();
        }

        [AllowAnonymous]
        [HttpGet("RetornaCupomAtivo")]
        private async Task<IEnumerable<GarotaCategoriaViewModel>> RetornaCupomAtivo()
        {
            return _mapper.Map<IEnumerable<GarotaCategoriaViewModel>>(await _garotacategoriaRepository.RetornaCupomAtivo());
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

        // [ClaimsAuthorize("GarotaCategoria", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<ActionResult<GarotaCategoriaViewModel>> AdicionarAlternativo(GarotaCategoriaImagemViewModel GarotaCategoriaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_01_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_02_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_03_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_04_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_05_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_06_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_07_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_08_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_09_upload))
            {
                return CustomResponse(ModelState);
            }

            if (!await ValidarArquivo(GarotaCategoriaViewModel.imagem_10_upload))
            {
                return CustomResponse(ModelState);
            }
            GarotaCategoriaViewModel.imagem_01 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_02 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_03 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_04 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_05 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_06 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_07 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_08 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_09 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_10 = "sera_alterado";
            GarotaCategoriaViewModel.imagem_banner = "sera_alterado";


            var dados = _mapper.Map<GarotaCategoria>(GarotaCategoriaViewModel);
            await _garotacategoriaService.Incluir(dados);
            GarotaCategoriaViewModel.id_garota_categoria = dados.id_garota_categoria;

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_01_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_03_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_04_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_05_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_06_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_07_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_08_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_09_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_10_upload, GarotaCategoriaViewModel.id_garota_categoria))
            {
                return CustomResponse(ModelState);
            }

            if (!(GarotaCategoriaViewModel.imagem_banner == null || GarotaCategoriaViewModel.imagem_banner.Length == 0))
            {
                if (!await UploadArquivoAlternativo(GarotaCategoriaViewModel.imagem_banner_upload, GarotaCategoriaViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }
                else
                {
                    GarotaCategoriaViewModel.imagem_banner = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_banner_upload.FileName);
                }
            }

            GarotaCategoriaViewModel.imagem_01 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_01_upload.FileName);
            GarotaCategoriaViewModel.imagem_02 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_02_upload.FileName);
            GarotaCategoriaViewModel.imagem_03 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_03_upload.FileName);
            GarotaCategoriaViewModel.imagem_04 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_04_upload.FileName);
            GarotaCategoriaViewModel.imagem_05 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_05_upload.FileName);
            GarotaCategoriaViewModel.imagem_06 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_06_upload.FileName);
            GarotaCategoriaViewModel.imagem_07 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_07_upload.FileName);
            GarotaCategoriaViewModel.imagem_08 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_08_upload.FileName);
            GarotaCategoriaViewModel.imagem_09 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_09_upload.FileName);
            GarotaCategoriaViewModel.imagem_10 = GarotaCategoriaViewModel.id_garota_categoria + "_" + Util.TiraAcentos(GarotaCategoriaViewModel.imagem_10_upload.FileName);
            await _garotacategoriaService.Alterar(_mapper.Map<GarotaCategoria>(GarotaCategoriaViewModel));

            return CustomResponse(GarotaCategoriaViewModel);
        }



        //   [ClaimsAuthorize("GarotaCategoria", "Atualizar")]
        [AllowAnonymous]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GarotaCategoriaViewModel>> Atualizar(int id, GarotaCategoriaImagemViewModel dadosViewModel)
        {
            if (id != dadosViewModel.id_garota_categoria)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(dadosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);


            if (!(dadosViewModel.imagem_01_upload == null || dadosViewModel.imagem_01_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_01_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_01_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_01 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_01_upload.FileName);
            }


            if (!(dadosViewModel.imagem_02_upload == null || dadosViewModel.imagem_02_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_02_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_02_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_02 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_02_upload.FileName);
            }

            if (!(dadosViewModel.imagem_03_upload == null || dadosViewModel.imagem_03_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_03_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_03_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_03 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_03_upload.FileName);
            }

            if (!(dadosViewModel.imagem_04_upload == null || dadosViewModel.imagem_04_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_04_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_04_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_04 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_04_upload.FileName);
            }

            if (!(dadosViewModel.imagem_05_upload == null || dadosViewModel.imagem_05_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_05_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_05_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_05 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_05_upload.FileName);
            }

            if (!(dadosViewModel.imagem_06_upload == null || dadosViewModel.imagem_06_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_06_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_06_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_06 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_06_upload.FileName);
            }

            if (!(dadosViewModel.imagem_07_upload == null || dadosViewModel.imagem_07_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_07_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_07_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_07 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_07_upload.FileName);
            }

            if (!(dadosViewModel.imagem_08_upload == null || dadosViewModel.imagem_08_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_08_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_08_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_08 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_08_upload.FileName);
            }

            if (!(dadosViewModel.imagem_09_upload == null || dadosViewModel.imagem_09_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_09_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_09_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_09 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_09_upload.FileName);
            }

            if (!(dadosViewModel.imagem_10_upload == null || dadosViewModel.imagem_10_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_10_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_10_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_10 = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_10_upload.FileName);
            }

            if (!(dadosViewModel.imagem_banner_upload == null || dadosViewModel.imagem_banner_upload.Length == 0))
            {
                if (!await ValidarArquivo(dadosViewModel.imagem_banner_upload))
                {
                    return CustomResponse(ModelState);
                }

                if (!await UploadArquivoAlternativo(dadosViewModel.imagem_banner_upload, dadosViewModel.id_garota_categoria))
                {
                    return CustomResponse(ModelState);
                }

                dadosViewModel.imagem_banner = dadosViewModel.id_garota_categoria + "_" + Util.TiraAcentos(dadosViewModel.imagem_banner_upload.FileName);
            }


            var dados = _mapper.Map<GarotaCategoria>(dadosViewModel);
            await _garotacategoriaService.Alterar(dados);

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
                NotificarErro("Selecione as imagens para esta Garota Categoria!");
                return false;
            }

            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));
           
            string caminhoImagem = Util.caminhoFoto.ToString();
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
                NotificarErro("Forneça as imagens para esta Garota Categoria!");
                return false;
            }


            var caminhoPastaImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_LOCAL"));
            var caminhoImagemBanco = _mapper.Map<ControleSistemaViewModel>(await _controlesistemaRepository.ObterPorNomePOC("IMG_FOTOS"));

            string caminhoImagem = Util.caminhoFoto.ToString();

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
