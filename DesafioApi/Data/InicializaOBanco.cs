using DesafioApi.Data.Entity;
using DesafioApi.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioApi.Data
{
    public class InicializaOBanco
    {
        public static void Initializate(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DesafioContext>();
                context.Database.EnsureCreated();

                if (context.Usuarios != null && context.Usuarios.Any())
                    return;


                var usuario = new Usuario
                {
                    Nome = "admin",
                    Email = "admin@admin.com",
                    Senha = "7523c62abdb7628c5a9dad8f97d8d8c5c040ede36535e531a8a3748b6cae7e00"
                };

                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
        }
    }
}
