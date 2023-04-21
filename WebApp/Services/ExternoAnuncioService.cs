using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoAnuncioService
    {
        Task<PagedViewModel<AnuncioViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<AnuncioViewModel> ObterPorId(int id);
        Task<IEnumerable<AnuncioViewModel>> ObterTodosAtivos();
    }
    public class ExternoAnuncioService : Service, IExternoAnuncioService
    {
        private readonly HttpClient _httpClient;

        public ExternoAnuncioService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }

        public async Task<AnuncioViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<AnuncioViewModel>(response);
        }

        public async Task<PagedViewModel<AnuncioViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<AnuncioViewModel>>(response);
        }

        public async Task<IEnumerable<AnuncioViewModel>> ObterTodosAtivos()
        {
            var response = await _httpClient.GetAsync($"/api/externoAnuncios/ObterTodosAtivos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<AnuncioViewModel>>(response);
        }
    }
}