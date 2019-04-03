using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AVLCarSystemApp.DataAccessImplementations;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using Swashbuckle.AspNetCore.Swagger;

namespace AVLCarSystemApp
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
      //services.AddScoped(s => new CarSystemContext())
      services.AddDbContext<CarSystemContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddScoped<ICountryRepository, CountryRepository>();
      services.AddScoped<ICityRepository, CityRepository>();
      services.AddScoped<IEngineRepository, EngineRepository>();
      services.AddScoped<IEngineTypeRepository, EngineTypeRepository>();
      services.AddScoped<IEquipmentRepository, EquipmentRepository>();
      services.AddScoped<IInventoryRepository, InventoryRepository>();
      services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
      services.AddScoped<IModelRepository, ModelRepository>();
      services.AddScoped<ISalonRepository, SalonRepository>();
      services.AddMvc();
      services.AddAutoMapper();

      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
                  builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials());
      });

      // Register the Swagger generator, defining 1 or more Swagger documents
      services.AddSwaggerGen(gen =>
      {
        gen.SwaggerDoc("v1", new Info()
        {
          Title = "My API",
          Version = "v1"
        });
      });

      //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

      //services.AddAuthentication(options =>
      //{
      //  options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      //  options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
      //})
      //.AddCookie("Cookies")
      //.AddOpenIdConnect(options => 
      //{
      //  options.Authority = Configuration["KeycloakConfiguration:Authority"];
      //  options.ClientId = Configuration["KeycloakConfiguration:ClientId"];
      //  options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["KeycloakConfiguration:RequireHttpMetadata"]);
      //  options.SaveTokens = Convert.ToBoolean(Configuration["KeycloakConfiguration:SaveTokens"]);
      //  options.ClientSecret = Configuration["KeycloakConfiguration:ClientSecret"];
      //  options.GetClaimsFromUserInfoEndpoint =
      //    Convert.ToBoolean(Configuration["KeycloakConfiguration:GetClaimsFromUserInfoEndpoint"]);
      //  options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
      //  options.Scope.Add("openid");
      //});

      services.AddAuthentication(o =>
      {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(cfg =>
      {
        // only for testing
        cfg.RequireHttpsMetadata = false;
        cfg.Authority = Configuration["KeycloakConfiguration:Authority"];
        cfg.Audience = Configuration["KeycloakConfiguration:ClientId"];
        

        cfg.Events = new JwtBearerEvents()
        {
          OnAuthenticationFailed = c =>
          {
            c.NoResult();
            c.Response.StatusCode = 401;
            c.Response.ContentType = "application/json";
            return c.Response.WriteAsync(c.Exception.ToString());
          }
        };
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      
      app.UseAuthentication();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
      }

      app.UseCors("CorsPolicy");
      app.UseMvc();
    }
  }
}
