using DevIO.Api.Data;
using DevIO.Api.Extensions;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<MeuDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); //DefaultConnection do appsettubgs.json
            //});

            //services.AddDbContext<MeuDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>() // usando as traduções de erro de login para portugues
                .AddDefaultTokenProviders(); // para gerar tokens


            // JTW

            var appSetingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSetingsSection);

            var appSettings = appSetingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true; // se for trabalhar com https aí é bom usar true
                x.SaveToken = true; // pra ir guardando no http authentication properties
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, //vai validar quem está emitindo, o mesmo usuário que recebeu 
                    IssuerSigningKey = new SymmetricSecurityKey(key), // configurando a chave, de ASCII 
                    ValidateIssuer = true, // validando apenas o issuer
                    ValidateAudience = true, // vendo onde que o token é válido
                    ValidAudience = appSettings.ValidoEm, // o localhost ou o site da aplicação
                    ValidIssuer = appSettings.Emissor //MeuSistema
                };
            });



            return services;
        }
    }
}

