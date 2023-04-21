using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoZonaService
    {
        Task<PagedViewModel<ZonaViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<ZonaViewModel> ObterPorId(int id);
        Task<IEnumerable<ZonaViewModel>> ObterTodos(int status);
       
    }
    public class ExternoZonaService : Service, IExternoZonaService
    {
        private readonly HttpClient _httpClient;

        public ExternoZonaService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }

        public async Task<ZonaViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ZonaViewModel>(response);
        }

        public async Task<PagedViewModel<ZonaViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<ZonaViewModel>>(response);
        }

        public async Task<IEnumerable<ZonaViewModel>> ObterTodos(int status)
        {
            var response = await _httpClient.GetAsync($"/api/externoZonas/obtertodos/{status}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<ZonaViewModel>>(response);
        }
       
    }
}