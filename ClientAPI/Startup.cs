using Application.Services;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ClientAPI
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
            // Registrar o ClienteRepository e ClienteService no contêiner de dependências
            services.AddScoped<ClienteRepository>(); // Registra o repositório
            services.AddScoped<ClienteService>(); // Registra o serviço

            services.AddControllers(); // Registra os controladores

            // Adicionar o Swagger (caso não tenha feito)
            services.AddSwaggerGen();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Ativa o middleware do Swagger
            app.UseSwagger();

            // Habilita o UI do Swagger para que você possa testar a API
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Clientes v1");
                c.RoutePrefix = string.Empty; // Faz com que o Swagger abra na raiz (http://localhost:5001/)
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
