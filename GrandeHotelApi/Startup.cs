using GrandeHotel.Lib.Data.Models;
using GrandeHotel.Lib.Data.Services;
using GrandeHotel.Lib.Data.Services.Impl;
using GrandeHotelApi.Models.Auth;
using GrandeHotelApi.Services;
using GrandeHotelApi.Services.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;

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
            services.AddDbContext<GrandeHotelCustomContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("grande_hotel"));
            });

            services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-566527.okta.com/oauth2/default";
                    options.Audience = "api://grande-hotel";
                    // options.RequireHttpsMetadata = false;
                    // options.MetadataAddress = 
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = "api://grande-hotel",
                        ValidateIssuer = true,
                        ValidIssuer = "https://dev-566527.okta.com/oauth2/default",
                        // Meta
                    };
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ITokenService, OktaTokenService>();

            services.AddControllers();
            services.AddOpenApiDocument(config =>
            {
                // Document name (default to: v1)
                config.DocumentName = "Grande Hotel API";

                // Document / API version (default to: 1.0.0)
                config.Version = "1.0.0";

                // Document title (default to: My Title)
                config.Title = "Grade Hotel API";

                // Document description
                config.Description = "API for the fictional Grande Hotel";
            });

            services.Configure<OktaSettings>(Configuration.GetSection("Okta"));
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStaticFiles();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSpaStaticFiles();
            app.UseSpa(spa => spa.Options.SourcePath = "ClientApp");
        }
    }
}
