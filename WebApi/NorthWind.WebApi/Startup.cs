using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NorthWind.Domain.Commands.Handlers;
using NorthWind.Domain.Repositories;
using NorthWind.Infrastructure.Contexts;
using NorthWind.Infrastructure.Repositories;
using NorthWind.Infrastructure.UnitOfWork;
using NorthWind.Shared;
using NorthWind.WebApi.Middlewares;
using NorthWind.WebApi.Security;
using System;
using System.Text;

namespace NorthWind.WebApi
{
    public class Startup
    {
        readonly static string ISSUER = "065ef4fa";
        readonly static string AUDIENCE = "23b883a4a3e6";
        readonly static string SECRET_KEY = "065ef4fa-18ee-4053-8cf3-23b883a4a3e6";
        readonly static SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Authentication
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // Compression
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            // Cors
            services.AddCors();

            // Authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim("NorthWind", "User"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("NorthWind", "Admin"));
            });

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddScoped<NorthWindDataContext, NorthWindDataContext>();
            services.AddTransient<IUow, Uow>();

            services.AddTransient<AccountHandler, AccountHandler>();
            services.AddTransient<CustomerHandler, CustomerHandler>();
            services.AddTransient<OrderHandler, OrderHandler>();
            services.AddTransient<ProductHandler, ProductHandler>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Runtime.ConnectionString = Configuration.GetConnectionString("NorthWindConnectionString");

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();                
            }

            app.UseResponseCompression();

            app.UseExceptionHandler(p => GlobalExceptionHandlerMiddleware.Handle(p, env.IsDevelopment()));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = ISSUER,

                ValidateAudience = true,
                ValidAudience = AUDIENCE,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();

            // Seed
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var northWindDataContext = serviceScope.ServiceProvider.GetService<NorthWindDataContext>())
                northWindDataContext.SeedData();
        }
    }
}
