using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BT.API.Models;
using BT.Data;
using BT.Service;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BT.API
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
            services.AddControllers();

            //ע��Swagger����
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CRM API�����ĵ�", Version = "v1" });

                string path = Path.Combine(Directory.GetCurrentDirectory(), "BT.API.xml");

                options.IncludeXmlComments(path);
            }
          );

            //ע�����ݿ�������

            services.AddDbContext<CRMContext>(optons=>optons.UseMySql(Configuration["Mysql"]));

            //ע��ִ���װ����
            services.AddScoped<IWrapperRepository, WrapperRepository>();

            //ע��JwtBearer�����֤
            //��ԭһ�������˶��� Principal
            //����Ĭ����֤����Jwt Bearer�����֤
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
             {
                 //������֤����
                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     ValidIssuer = "www.zhangquque.com",
                     ValidAudience = "www.gaozijian.com",
                     IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("asdadhajhdkjahsdkjahdkj9au8d9adasidoad89asu813e")),//��Կ
                     ValidateLifetime = true,   //��֤Token����ʱ��
                     //NameClaimType=JwtClaimTypes.Name    //Name����������
                 };
             });

            //�������ļ�����ע�ᵽ��Ե����������
            services.Configure<CustomConfigOptions>(Configuration.GetSection("CustomConfigOptions"));

            //ע��һ�����򷽰�
            services.AddCors(options => options.AddPolicy("cors", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            //ע��ע��AutoMapper
            services.AddAutoMapper(typeof(Startup));

            //ע��redis�ֲ�ʽ�������
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "129.211.14.185:6379";
                options.InstanceName = "fanyaohui";
            });
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

            //����
            app.UseCors("cors");

            //����Swagger�м��
            app.UseSwagger();

            //����API�ĵ�
            app.UseSwaggerUI(options=>options.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM API�����ĵ�"));

            //�����֤
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
