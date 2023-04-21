using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApp.Core.Communication;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public abstract class Service // abastract, só pode ser herdada, nunca instanciada
    {
        protected StringContent ObterConteudo(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // desconsidera palavras minusculas e minusculas
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401: // não autorizado, não conhece o usuário
                case 403: // acesso negado
                case 404: // recurso não encontrado
                case 500: // erro de servidor
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400: // significa que tem mensagens de erro
                    return false;
            }

            response.EnsureSuccessStatusCode(); // garanta que retornou um código de sucesso
            return true;
        }

        protected ResponseResult RetornoOk()
        {
            return new ResponseResult();
        }
    }
}