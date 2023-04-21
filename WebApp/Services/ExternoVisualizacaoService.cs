using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoVisualizacaoService
    {
        Task<PagedViewModel<VisualizacaoViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<VisualizacaoViewModel> ObterPorId(int id);
        Task<IEnumerable<VisualizacaoViewModel>> ObterTodos(int status);

        Task<int> RetornarTotalVisualizacaoGarotasPOC();
        Task<int> RetornarTotalVisualizacaoGarotasTotalPOC();
        Task<IEnumerable<VisualizacaoViewModel>> RetornarTotalVisualizacaoSiteGrupo();
    }
    public class ExternoVisualizacaoService : Service, IExternoVisualizacaoService
    {
        private readonly HttpClient _httpClient;

        public ExternoVisualizacaoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }

        public async Task<VisualizacaoViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<VisualizacaoViewModel>(response);
        }

        public async Task<PagedViewModel<VisualizacaoViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<VisualizacaoViewModel>>(response);
        }

        public async Task<IEnumerable<VisualizacaoViewModel>> ObterTodos(int status)
        {
            var response = await _httpClient.GetAsync($"/api/externoVisualizacao/obtertodos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<VisualizacaoViewModel>>(response);
        }

        public async Task<int> RetornarTotalVisualizacaoGarotasPOC()
        {
            var response = await _httpClient.GetAsync($"/api/externoVisualizacao/RetornarTotalVisualizacaoGarotasPOC");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<int>(response);
        }

        public async Task<int> RetornarTotalVisualizacaoGarotasTotalPOC()
        {
            var response = await _httpClient.GetAsync($"/api/externoVisualizacao/RetornarTotalVisualizacaoGarotasTotalPOC");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<int>(response);
        }

        public async Task<IEnumerable<VisualizacaoViewModel>> RetornarTotalVisualizacaoSiteGrupo()
        {
            var response = await _httpClient.GetAsync($"/api/externoVisualizacao/RetornarTotalVisualizacaoSiteGrupo");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<VisualizacaoViewModel>>(response);
        }
    }
}