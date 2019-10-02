using GrandeHotel.Lib.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrandeHotelApi
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
            services.AddDbContext<GrandeHotelContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("grande_hotel"));
            });

            services.AddSwaggerDocument(options =>
            {
                options.Title = "Grande Hotel Api";
                options.Description = "Manage reservations at the Grande Hotel, London, UK";

                // TODO: Re-add when we implement auth/auth
                //options.AddSecurity("bearer", new string[0], new NSwag.OpenApiSecurityScheme
                //{
                //    Type = NSwag.OpenApiSecuritySchemeType.OAuth2,
                //    Description = "Copy 'Bearer ' + valid token (retrieved by using \"/token\" entrypoint) into the field",
                //    Flow = NSwag.OpenApiOAuth2Flow.Password,
                //    Flows = new NSwag.OpenApiOAuthFlows
                //    {
                //        Password = new NSwag.OpenApiOAuthFlow
                //        {
                //            TokenUrl = "https://localhost:44350/token"
                //        }
                //    }
                //});

                //options.OperationProcessors.Add(new OperationSecurityScopeProcessor("bearer"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
