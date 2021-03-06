using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using nw_api.Data.Interfaces;
using nw_api.Data.Repository;
using nw_api.Interfaces;
using nw_api.Services;
using AutoMapper;
using nw_api.Data.Entities;

namespace nw_api
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
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INetWorthService, NetWorthService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICashRepository, CashRepository>();
            services.AddTransient<IInvestedAssetsRepository, InvestedAssetsRepository>();
            services.AddTransient<ILiabilitiesRepository, LiabilitiesRepository>();
            services.AddTransient<IUseAssetsRepository, UseAssetsRepository>();
            services.AddTransient<INetWorthRepository, NetWorthRepository>();
            
            services.AddAutoMapper(typeof(Startup));
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build();
                }));
            
            services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    SaveSigninToken = true
                };
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}