using System;
using System.Net;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using WebApp.Services;
using Polly.CircuitBreaker;
using Refit;

namespace WebApp.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static IAutenticacaoService _autenticacaoService;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // vai tratar o que vai ser feito no middleware
        public async Task InvokeAsync(HttpContext httpContext/*, IAutenticacaoService autenticacaoService*/)
        {
        //    _autenticacaoService = autenticacaoService;

            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ValidationApiException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (ApiException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }
            catch (RpcException ex)
            {
                //400 Bad Request	    INTERNAL
                //401 Unauthorized      UNAUTHENTICATED
                //403 Forbidden         PERMISSION_DENIED
                //404 Not Found         UNIMPLEMENTED

                var statusCode = ex.StatusCode switch
                {
                    StatusCode.Internal => 400,
                    StatusCode.Unauthenticated => 401,
                    StatusCode.PermissionDenied => 403,
                    StatusCode.Unimplemented => 404,
                    _ => 500
                };

                var httpStatusCode = (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), statusCode.ToString());

                HandleRequestExceptionAsync(httpContext, httpStatusCode);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                //TODO: rever 08/04/2023
                //if (_autenticacaoService.TokenExpirado())
                //{
                //    if (_autenticacaoService.RefreshTokenValido().Result)
                //    {
                //        context.Response.Redirect(context.Request.Path);
                //        return;
                //    }
                //}

                //_autenticacaoService.Logout();
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");// context.Request.Path é o restinho do path, que pega e sabe de qual local você estava vindo
                return; // tem que ter o return, senão ele vai continuar o processo abaixo
            }

            context.Response.StatusCode = (int)statusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }


        #region Se quiser usar com o Refit
        //public async Task InvokeAsync(HttpContext httpContext)
        //{
        //    try
        //    {
        //        await _next(httpContext);
        //    }
        //    catch (CustomHttpRequestException ex)
        //    {
        //        HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        //    }
        //    catch (ValidationApiException ex)
        //    {
        //        HandleRequestExceptionAsync(httpContext, ex.StatusCode);
        //    }
        //    catch(ApiException ex)
        //    {
        //        HandleRequestExceptionAsync(httpContext, ex.StatusCode); // erro 401 para ir para o login
        //    }
        //}

        //private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode statusCode)
        //{
        //    if (statusCode == HttpStatusCode.Unauthorized)
        //    {
        //        context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}"); // context.Request.Path é o restinho do path, que pega e sabe de qual local você estava vindo
        //        return; // tem que ter o return, senão ele vai continuar o processo abaixo
        //    }

        //    context.Response.StatusCode = (int)statusCode;
        //}

        #endregion

    }
}