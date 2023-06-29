using AndreiToledo.RestWithBooksAPI.Business;
using AndreiToledo.RestWithBooksAPI.Business.Implementations;
using AndreiToledo.RestWithBooksAPI.Configurations;
using AndreiToledo.RestWithBooksAPI.Hypermedia.Enricher;
using AndreiToledo.RestWithBooksAPI.Hypermedia.Filters;
using AndreiToledo.RestWithBooksAPI.Model.Context;
using AndreiToledo.RestWithBooksAPI.Repository;
using AndreiToledo.RestWithBooksAPI.Repository.Generic;
using AndreiToledo.RestWithBooksAPI.Services;
using AndreiToledo.RestWithBooksAPI.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace AndreiToledo.RestWithBooksAPI
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Configura��o para JWT - inicio
            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                   Configuration.GetSection("TokenConfigurations")
               )
               .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfigurations.Issuer,
                        ValidAudience = tokenConfigurations.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                    };
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            // Configura��o para JWT - fim


            // CORS - para aceitar aplica��o em qualquer linguagem consumindo a API
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();

            // Configura��o com o Banco de Dados
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml").ToString());
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json").ToString());
            })
            .AddXmlSerializerFormatters();

            // Adiciona para HATEOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

            services.AddSingleton(filterOptions);

            // Versionamento API
            services.AddApiVersioning();

            // Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API's From 0 to Azure whith ASP.NET Core 5 and Docker",
                        Version = "v1",
                        Description = "API RESTful developed 'REST API's' From 0 to Azure ASP.NET Core 5",
                        Contact = new OpenApiContact
                        {
                            Name = "Andrei Toledo",
                            Url = new Uri("https://github.com/andreitoledo")
                        }
                    });            
            });

            // Dependency Injection            
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();                        
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();

            // Login JWT
            services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();

            // PATCH
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Repositorio Generico
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // cors - depois do UseHttpsRedirection/UseRouting e antes do app.UseEndpoints(endpoints =>
            app.UseCors();

            // Gera Json
            app.UseSwagger();
            // Gera a pagina HTML do Swagger
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "REST API's From 0 to Azure whith ASP.NET Core 5 and Docker - v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }       

        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }           
        }
    }
}
