using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevSpace_API.Data;
using DevSpace_API.Data.Article;
using DevSpace_API.Data.Drafts;
using DevSpace_API.Data.Hashtags;
using DevSpace_API.Data.Likes;
using DevSpace_API.Data.ReadingList;
using DevSpace_API.Data.Userdetails;
using DevSpace_API.DataContext;
using DevSpace_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace DevSpace_API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // using Microsoft.EntityFrameworkCore;
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevSpaceConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000",
                                            "https://localhost:3000")
                                            .WithMethods("PUT", "DELETE", "GET", "POST")
                                            .AllowAnyHeader();
                    });
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
                    {
                        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false, //shoud be true?
                            ValidIssuer = Configuration["AuthSettings:Issuer"],
                            ValidAudience = Configuration["AuthSettings:Audience"],
                            RequireExpirationTime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                            ValidateIssuerSigningKey = true
                        };
                    });

            services.AddScoped<IArticleRepo, SqlArticleRepo>();
            services.AddScoped<IReadingListRepo, SqlReadingListRepo>();
            services.AddScoped<IHashtagRepo, SqlHashtagRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDetailRepo, SqlUserDetailRepo>();
            services.AddScoped<IDraftRepo, SqlDraftRepo>();
            services.AddScoped<ILikeRepo, SqlLikeRepo>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
