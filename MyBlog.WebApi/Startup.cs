using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using MyBlog.Service;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;  // Encoding 引入

using MyBlog.WebApi.Utility._AutoMapper;

namespace MyBlog.WebApi
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
            
            #region SqlSugar IOC//自动释放 1. 注册
            services.AddSqlSugar(new IocConfig()
            {
                ConnectionString = this.Configuration["SqlConn"],
                DbType = IocDbType.SqlServer,
                IsAutoCloseConnection = true
            });
            #endregion


            #region IOC 依赖注入
                services.AddCustomIOC();
            #endregion


            #region  02- JWT 鉴权
            services.AddCustomJWT();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog.WebApi", Version = "v1" });

                #region   swagger使用  鉴权 组件：Swagger想要使用鉴权，需要注册服务的时候添加以下代码
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "直接在下框中输入Bearer {token} (注意：两者之间是一个空格)",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                     new OpenApiSecurityScheme
                        {
                         Reference=new OpenApiReference
                         {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                          }
                         },
                new string[] {}
                 }
                });
                #endregion

            });
            #region    autoMapper
            services.AddAutoMapper(typeof(CustomAutoMapperProfile));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBlog.WebApi v1"));
            }

            app.UseRouting();
            // 添加到 管道中
            // 01-在 webApi项目里 鉴权：Authentica，UseAuthentication：用户认证
            app.UseAuthentication();// 鉴权

            app.UseAuthorization();//授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    public static class IOCExtend
    {
        // 依赖注入
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            // Repository 和 接口
            services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
            // Service 和 接口
            services.AddScoped<IBlogNewsService, BlogNewsService>();
            services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
            services.AddScoped<ITypeInfoService, TypeInfoService>();
            services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
            services.AddScoped<IWriterInfoService, WriterInfoService>();
            return services;
        }
        public static IServiceCollection AddCustomJWT(this IServiceCollection services)
        {
            // 自定义 方法：代码模块化
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),// 密钥， 必须与JWT项目里相同
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:6060", //JWT 服务器
                ValidateAudience = true,
                ValidAudience = "http://localhost:5000",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(60)// 过期时间
              };
            });
         return services;
        }

    }
}
