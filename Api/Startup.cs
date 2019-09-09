using Composition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api
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

            services.AddScoped<IAnimalProvider, AnimalProvider>();

            services.AddSingleton<Gato>();
            services.AddSingleton<Cachorro>();
            services.AddSingleton<DefaultAnimal>();

            services.AddTransient<Func<EAnimal, IAnimal>>(
                serviceProvider => key =>
                {
                    switch (key)
                    {
                        case EAnimal.Gato:
                            return serviceProvider.GetService<Gato>();
                        case EAnimal.Cachorro:
                            return serviceProvider.GetService<Cachorro>();
                        default:
                            return serviceProvider.GetService<DefaultAnimal>();
                    }
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
            app.UseMvc();
        }
    }
}
