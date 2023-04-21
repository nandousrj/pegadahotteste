using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoParametrosService
    {
        Task<PagedViewModel<ParametrosViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<ParametrosViewModel> ObterPorId(int id);
        Task<IEnumerable<ParametrosViewModel>> ObterTodos();
    }
    public class ExternoParametrosService : Service, IExternoParametrosService
    {
        private readonly HttpClient _httpClient;

        public ExternoParametrosService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }

        public async Task<ParametrosViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/api/externoParametros/ObterPorId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ParametrosViewModel>(response);
        }

        public async Task<PagedViewModel<ParametrosViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<ParametrosViewModel>>(response);
        }

        public async Task<IEnumerable<ParametrosViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/api/externoParametross/obtertodos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<ParametrosViewModel>>(response);
        }
    }
}