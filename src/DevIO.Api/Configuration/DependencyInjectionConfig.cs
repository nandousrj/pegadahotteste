using DevIO.Api.Extensions;
using DevIO.Business.Intefaces;
using DevIO.Business.Interfaces;
using DevIO.Business.Notificacoes;
using DevIO.Business.Services;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IEstiloRepository, EstiloRepository>();
            services.AddScoped<ITrabalhoRepository, TrabalhoRepository>();
            services.AddScoped<IZonaRepository, ZonaRepository>();
            services.AddScoped<IBairroRepository, BairroRepository>();
            services.AddScoped<IAtendeRepository, AtendeRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ITipoContatoRepository, TipoContatoRepository>();
            services.AddScoped<IIdiomaRepository, IdiomaRepository>();
            services.AddScoped<IOlhosRepository, OlhosRepository>();
            services.AddScoped<ISexoRepository, SexoRepository>();
            services.AddScoped<ITipoAnuncioRepository, TipoAnuncioRepository>();
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();
            services.AddScoped<ITipoPagamentoRepository, TipoPagamentoRepository>();
            services.AddScoped<ITipoLogRepository, TipoLogRepository>();
            services.AddScoped<ITipoContaRepository, TipoContaRepository>();
            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<ITipoCriticaRepository, TipoCriticaRepository>();
            services.AddScoped<IVisualizacaoRepository, VisualizacaoRepository>();
            services.AddScoped<INovidadeRepository, NovidadeRepository>();
            services.AddScoped<IGarotaRepository, GarotaRepository>();
            services.AddScoped<IGarotaCategoriaRepository, GarotaCategoriaRepository>();
            services.AddScoped<IGarotaCategoriaAtendeRepository, GarotaCategoriaAtendeRepository>();
            services.AddScoped<IGarotaCategoriaIdiomaRepository, GarotaCategoriaIdiomaRepository>();
            services.AddScoped<IControleSistemaRepository, ControleSistemaRepository>();
            services.AddScoped<IParametrosRepository, ParametrosRepository>();

            services.AddScoped<IPermissoesSistemaRepository, PermissoesSistemaRepository>();
            services.AddScoped<IPermissoesInstituicaoRepository, PermissoesInstituicaoRepository>();
            services.AddScoped<IPermissoesUsuarioRepository, PermissoesUsuarioRepository>();


            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();



            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IEstiloService, EstiloService>();
            services.AddScoped<ITrabalhoService, TrabalhoService>();
         //   services.AddScoped<IZonaService, ZonaService>();
            services.AddScoped<IBairroService, BairroService>();
            services.AddScoped<IAtendeService, AtendeService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ITipoContatoService, TipoContatoService>();
            services.AddScoped<IIdiomaService, IdiomaService>();
            services.AddScoped<IOlhosService, OlhosService>();
            services.AddScoped<ISexoService, SexoService>();
            services.AddScoped<ITipoAnuncioService, TipoAnuncioService>();
            services.AddScoped<IAnuncioService, AnuncioService>();
            services.AddScoped<ITipoPagamentoService, TipoPagamentoService>();
            services.AddScoped<ITipoLogService, TipoLogService>();
            services.AddScoped<ITipoContaService, TipoContaService>();            
            services.AddScoped<IBancoService, BancoService>();
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<ITipoCriticaService, TipoCriticaService>();
            services.AddScoped<IVisualizacaoService, VisualizacaoService>();
            services.AddScoped<INovidadeService, NovidadeService>();
            services.AddScoped<IGarotaService, GarotaService>();
            services.AddScoped<IGarotaCategoriaService, GarotaCategoriaService>();
            services.AddScoped<IControleSistemaService, ControleSistemaService>();
            services.AddScoped<IParametrosService, ParametrosService>();


            services.AddScoped<IPermissoesSistemaService, PermissoesSistemaService>();
            services.AddScoped<IPermissoesInstituicaoService, PermissoesInstituicaoService>();
            services.AddScoped<IPermissoesUsuarioService, PermissoesUsuarioService>();

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IFornecedorService, FornecedorService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
