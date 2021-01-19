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
                    // 设定允许跨域的来源，有多个可以用','隔开
                    builder.WithOrigins(configuration["App:CorsOrigins"]
                                 .Split(",", StringSplitOptions.RemoveEmptyEntries) // StringSplitOptions.RemoveEmptyEntries 去除空的数组元素
                                 .Select(o => o.RemovePostFix("/"))
                                 .ToArray())
                           .WithAbpExposedHeaders()
                           .SetIsOriginAllowedToAllowWildcardSubdomains()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials(); // 允许凭据的策略
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HL7消息日志接口", Version = "v1" });

                // 使用反射获取xml文件。并构造出文件的路径
                System.Collections.Generic.List<string> listAssembly = new System.Collections.Generic.List<string>() { $"SamJan.LogService.Host" };
                foreach (var assembly in listAssembly)
                {
                    var xmlPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, $"{assembly}.xml");
                    if (File.Exists(xmlPath))
                        // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
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
