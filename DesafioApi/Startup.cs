using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioApi.Data;
using DesafioApi.Data.Entity;
using DesafioApi.Entity;
using DesafioApi.Iterfaces;
using DesafioApi.Model;
using DesafioApi.Model.Patrimonio;
using DesafioApi.Model.Usuario;
using DesafioApi.Repository;
using DesafioApi.Repository.Iterfaces;
using DesafioApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DesafioApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<DesafioContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DesafioDBConnectionString")));

            services.AddScoped<IMarcasRepository, MarcasRepository>();
            services.AddScoped<IPatrimoniosRepository, PatrimonioRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUsuariosRepository, UsuarioRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "DesafioApi",
                        ValidAudience = "Consumidor",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["ChaveDeSeguranca"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Token invalido" + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Token valido" + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });
           
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            InicializaOBanco.Initializate(app);
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Marca, MarcaDto>();
                cfg.CreateMap<MarcaDto, Marca>();
                cfg.CreateMap<MarcaParaAtualizarDto, Marca>();
                cfg.CreateMap<MarcaParaAdicionarDto, Marca>();
                cfg.CreateMap<Patrimonio, PatrimonioDto>();
                cfg.CreateMap<PatrimonioDto, Patrimonio>();
                cfg.CreateMap<PatrimonioParaAtualizarDto, Patrimonio>();
                cfg.CreateMap<PatrimonioParaAdicionarDto, Patrimonio>();
                cfg.CreateMap<PatrimonioParaAdicionarDto, PatrimonioDto>();
                cfg.CreateMap<Usuario, UsuarioDto>();
                cfg.CreateMap<UsuarioDto, Usuario>();
                cfg.CreateMap<Usuario, UsuarioParaAdicionarDto>();
                cfg.CreateMap<UsuarioParaAdicionarDto, Usuario>();
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
