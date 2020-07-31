using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Routines.Api.Data;
using Routines.Api.Services;

namespace Routines.Api
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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
                //var data = new  XmlDataContractSerializerOutputFormatter();
                //setup.OutputFormatters.Insert(0,data);//����֮�еĵ�һ������Ϊ����Ĭ������,����Ϊxml
            }).AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters().//����������xml��ʽ��һ��ʹ�����ַ�ʽ
            ConfigureApiBehaviorOptions(setup=> {
                //���api������֤����
                setup.InvalidModelStateResponseFactory = context =>
                {
                    var problemdetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type="www.baidu.com",
                        Title="�д���",
                        Status=StatusCodes.Status422UnprocessableEntity,
                        Detail="�뿴��ϸ��Ϣ",
                        Instance=context.HttpContext.Request.Path,
                    };
                    problemdetails.Extensions.Add("tranceid", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemdetails)
                    {
                        ContentTypes = { "application/problem+json"}
                    };

                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//ɨ���ļ��µ������ļ�
            services.AddScoped<ICompanyRepository,CompanyRepository>();

            services.AddDbContext<RoutineDbContext>(ops =>
            {
                ops.UseSqlite("Data Source=routine.db");
            });
            //  services.AddControllersWithViews();

            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
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
                app.UseExceptionHandler(appbulidler=> {
                    appbulidler.Run(async context => {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected");
                    });
                });
            }
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
