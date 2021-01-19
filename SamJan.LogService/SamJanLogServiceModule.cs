using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SamJan.LogService.Host
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule)
        )]    
    public class SamJanLogServiceModule : AbpModule
    {
        private readonly string DefaultCorsPolicyName = "SamJan";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;

            var configuration = context.Services.GetConfiguration();

            Configure<AbpAspNetCoreMvcOptions>(options=>
            {
                options.ConventionalControllers.Create(typeof(SamJanLogServiceModule).Assembly);
            });

            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    // �趨����������Դ���ж��������','����
                    builder.WithOrigins(configuration["App:CorsOrigins"]
                                 .Split(",", StringSplitOptions.RemoveEmptyEntries) // StringSplitOptions.RemoveEmptyEntries ȥ���յ�����Ԫ��
                                 .Select(o => o.RemovePostFix("/"))
                                 .ToArray())
                           .WithAbpExposedHeaders()
                           .SetIsOriginAllowedToAllowWildcardSubdomains()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials(); // ����ƾ�ݵĲ���
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HL7��Ϣ��־�ӿ�", Version = "v1" });

                // ʹ�÷����ȡxml�ļ�����������ļ���·��
                System.Collections.Generic.List<string> listAssembly = new System.Collections.Generic.List<string>() { $"SamJan.LogService.Host" };
                foreach (var assembly in listAssembly)
                {
                    var xmlPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, $"{assembly}.xml");
                    if (File.Exists(xmlPath))
                        // ����xmlע��. �÷����ڶ����������ÿ�������ע�ͣ�Ĭ��Ϊfalse.
                        c.IncludeXmlComments(xmlPath, true);
                }
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SamJan.LogService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(DefaultCorsPolicyName);

            app.UseAuthorization();

            app.UseConfiguredEndpoints();
        }
    }
}
