using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoGrupoService
    {
        Task<PagedViewModel<GrupoViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<GrupoViewModel> ObterPorId(int id);
        Task<IEnumerable<GrupoViewModel>> ObterTodos();
    }
    public class ExternoGrupoService : Service, IExternoGrupoService
    {
        private readonly HttpClient _httpClient;

        public ExternoGrupoService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }

        public async Task<GrupoViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/api/externoGrupos/ObterPorId/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<GrupoViewModel>(response);
        }

        public async Task<PagedViewModel<GrupoViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<GrupoViewModel>>(response);
        }

        public async Task<IEnumerable<GrupoViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/api/externoGrupos/obtertodos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<GrupoViewModel>>(response);
        }
    }
}