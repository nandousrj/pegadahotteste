using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoGarotaCategoriaService
    {

        Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosPrincipal(int id_categoria, string desc_zona, int ind_destaque, string apelido, int id_estilo);
        Task<GarotaCategoriaViewModel> RetornarDadosDetalhe(int id_garota_categoria, int id_categoria);
        Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosGrupo(int id_grupo);
        Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotal(int id_categoria);
        Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoes(int id_categoria);
        Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoes(int id_categoria);
        Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotalTodos(int id_categoria);
        Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoesTodos(int id_categoria);
        Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoesTodos(int id_categoria);
        Task<int> AtualizarVisualizacaoGarotaCategoria(int id_garota_categoria, int id_categoria);
        Task<PagedViewModel<GarotaCategoriaViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<GarotaCategoriaViewModel> ObterPorId(int id);
        Task<IEnumerable<GarotaCategoriaViewModel>> ObterTodos();
    }
    public class ExternoGarotaCategoriaService : Service, IExternoGarotaCategoriaService
    {
        private readonly HttpClient _httpClient;

        public ExternoGarotaCategoriaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }



        public async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosPrincipal(int id_categoria, string desc_zona, int ind_destaque, string apelido, int id_estilo)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/RetornarDadosPrincipal?id_categoria={id_categoria}&desc_zona{desc_zona}&ind_destaque={ind_destaque}&apelido={apelido}&id_estilo={id_estilo}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }

        public async Task<GarotaCategoriaViewModel> RetornarDadosDetalhe(int id_garota_categoria, int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/RetornarGarotaCategoriaDetalhe?id_garota_categoria={id_garota_categoria}&id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<GarotaCategoriaViewModel>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> RetornarDadosGrupo(int id_grupo)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/RetornarDadosGrupo?id_grupo={id_grupo}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotal(int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/CarregaGarotaCurtidasVisualizadasTotal?id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoes(int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/CarregaGarotasRankingVisualizacoes?id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoes(int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/CarregaGarotasRankingCurticoes?id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotaCurtidasVisualizadasTotalTodos(int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/CarregaGarotaCurtidasVisualizadasTotalTodos?id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingVisualizacoesTodos(int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/CarregaGarotasRankingVisualizacoesTodos?id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> CarregaGarotasRankingCurticoesTodos(int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/CarregaGarotasRankingCurticoesTodos?id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }



        public async Task<int> AtualizarVisualizacaoGarotaCategoria(int id_garota_categoria, int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/v1/externoGarotaCategorias/AtualizarVisualizacaoGarotaCategoria?id_garota_categoria={id_garota_categoria}&id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<int>(response);
        }
     
        

        public async Task<GarotaCategoriaViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<GarotaCategoriaViewModel>(response);
        }

        public async Task<PagedViewModel<GarotaCategoriaViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<GarotaCategoriaViewModel>>(response);
        }

        public async Task<IEnumerable<GarotaCategoriaViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/api/externoGarotaCategorias/obtertodos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GarotaCategoriaViewModel>>(response);
        }
    }
}