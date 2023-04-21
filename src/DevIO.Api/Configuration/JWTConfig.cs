using DevIO.Api.Data;
using DevIO.Api.Extensions;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public class JWTConfig
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        // private readonly PermissoesUsuarioRepository _permissoesusuarioRepository;

        //   private readonly IPermissoesUsuarioRepository _permissoesusuarioRepository;
        //    private readonly IPermissoesUsuarioService _permissoesusuarioService;
        //    private readonly IPermissoesUsuarioService _permissoesusuarioService;

        //public JWTConfig(IPermissoesUsuarioRepository permissoesusuarioRepository, IPermissoesUsuarioService permissoesusuarioService)
        //{
        //    _permissoesusuarioRepository = permissoesusuarioRepository;
        //    _permissoesusuarioService = permissoesusuarioService;


        //}

        public JWTConfig(RequestDelegate next, IOptions<AppSettings> appSettings)//, //IPermissoesUsuarioRepository permissoesusuarioRepository,
                                                                                 // IPermissoesUsuarioService permissoesusuarioService)
        {
            _next = next;
            _appSettings = appSettings.Value;
            //   _permissoesusuarioRepository = permissoesusuarioRepository;
            //  _permissoesusuarioService = permissoesusuarioService;
        }
        public async Task Invoke(HttpContext context, IPermissoesUsuarioService permissoesusuarioService)//, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, permissoesusuarioService, token);//, permissoesUsuarioRepository);

            await _next(context);
        }


        private async void attachUserToContext(HttpContext context, IPermissoesUsuarioService permissoesusuarioService, string token)//, IPermissoesUsuarioRepository permissaousuariorepository)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //  var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //  var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);


                // attach user to context on successful jwt validation
                //     context.Items["User"] = userService.GetById(userId);

                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //  PermissoesUsuario permissoesUsuario = new PermissoesUsuario();
             //   PermissoesUsuario permissoesUsuario = await permissoesusuarioService.RetornarUsuario(userId);
                // var teste =  await _permissoesusuarioRepository.RetornarUsuario(userId);

                //PermissoesUsuario permissoesUsuarioTeste = new PermissoesUsuario()
                //{
                //    id_usuario = 1,
                //    login = "fernando.silva",
                //    email = "fusinforj@gmail.com"
                //};

                //if (permissoesUsuario.id_usuario != permissoesUsuarioTeste.id_usuario)
                //{
                //    string aqui01;
                //};

                //if (permissoesUsuario.login != permissoesUsuarioTeste.login)
                //{
                //    string aqui02;
                //};

                //if (permissoesUsuario.email != permissoesUsuarioTeste.email)
                //{
                //    string aqui03;
                //};

                //User user = new User()
                //{
                //    Id = 1,//permissoesUsuario.id_usuario,
                //    FirstName = permissoesUsuario.login.Trim(),
                //    LastName = permissoesUsuario.email.Trim(),
                //    Username = permissoesUsuario.login.Trim()
                //};

                User user = new User()
                {
                    Id = 1,
                    FirstName = "2fernando.silva",
                    LastName = "f2usinforj@gmail.com",
                    Username = "2fernando.silva"
                };
                // Business.Models.User use = new Business.Models.User { Id = pemissoesUsuario.id_usuario.To , }
                context.Items["User"] = user;// permissaousuariorepository.RetornarUsuario(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

        //public async Task<PermissoesUsuario> RetornarDados(int id_usuario)
        //{
        //    //  var dados = _mapper.Map<PermissoesUsuarioViewModel>(await _permissoesusuarioRepository.RetornarUsuario(id_usuario));
        //    PermissoesUsuario dados = new PermissoesUsuario();
        //    var teste = await _permissoesusuarioService.RetornarUsuario(id_usuario);

        //    if (dados == null) return null; //pra retornar NotFound tem que ter Action Result

        //    return dados;
        //}

        //public static IServiceCollection AddIdentityConfiguration2(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var appSetingsSection = configuration.GetSection("AppSettings");
        //    services.Configure<AppSettings>(appSetingsSection);

        //    var appSettings = appSetingsSection.Get<AppSettings>();
        //    var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        //    services.AddAuthentication(x =>
        //        {
        //            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        }).AddJwtBearer(x =>
        //        {
        //            x.RequireHttpsMetadata = true; // se for trabalhar com https aí é bom usar true
        //        x.SaveToken = true; // pra ir guardando no http authentication properties
        //        x.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuerSigningKey = true, //vai validar quem está emitindo, o mesmo usuário que recebeu 
        //            IssuerSigningKey = new SymmetricSecurityKey(key), // configurando a chave, de ASCII 
        //            ValidateIssuer = true, // validando apenas o issuer
        //            ValidateAudience = true, // vendo onde que o token é válido
        //            ValidAudience = appSettings.ValidoEm, // o localhost ou o site da aplicação
        //            ValidIssuer = appSettings.Emissor //MeuSistema
        //        };
        //        });



        //    return services;
        //}
    }
}
