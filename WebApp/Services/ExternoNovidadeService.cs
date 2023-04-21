using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IExternoNovidadeService
    {
        Task<PagedViewModel<NovidadeViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null);
        Task<NovidadeViewModel> ObterPorId(int id);
        Task<IEnumerable<NovidadeViewModel>> ObterTodos();

        Task<IEnumerable<NovidadeViewModel>> RetornarDadosAtivos(int id_categoria);
    }
    public class ExternoNovidadeService : Service, IExternoNovidadeService
    {
        private readonly HttpClient _httpClient;

        public ExternoNovidadeService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AgenciaUrl);

            _httpClient = httpClient;
        }

        public async Task<NovidadeViewModel> ObterPorId(int id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<NovidadeViewModel>(response);
        }

        public async Task<PagedViewModel<NovidadeViewModel>> ObterTodosPaged(int pageSize, int pageIndex, string query = null)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<PagedViewModel<NovidadeViewModel>>(response);
        }

        public async Task<IEnumerable<NovidadeViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync($"/api/externoNovidades/obtertodos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<NovidadeViewModel>>(response);
        }

        public async Task<IEnumerable<NovidadeViewModel>> RetornarDadosAtivos(int id_categoria)
        {
            var response = await _httpClient.GetAsync($"/api/externoGarotaCategorias/RetornarDadosAtivos?id_categoria={id_categoria}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<NovidadeViewModel>>(response);
        }
    }
}