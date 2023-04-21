using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoEstiloService
    {
        Task<PagedViewModel<EstiloViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<EstiloViewModel> ObterPorId(int id);
        Task<IEnumerable<EstiloViewModel>> ObterTodos();
    }
    public class ExternoEstiloService : Service, IExternoEstiloService
    {
        private readonly HttpClient _httpClient;

        public ExternoEstiloService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }

        public async Task<EstiloViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<EstiloViewModel>(response);
        }

        public async Task<PagedViewModel<EstiloViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<EstiloViewModel>>(response);
        }

        public async Task<IEnumerable<EstiloViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/api/externoestilos/obtertodos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<EstiloViewModel>>(response);
        }
    }
}