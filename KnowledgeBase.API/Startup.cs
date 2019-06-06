using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using KnowledgeBase.API.Services;
using KnowledgeBase.Common.RabbitMQ;
using KnowledgeBase.Common.RestEase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeBase.API
{
    public class Startup
    {
        public IContainer Container { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.RegisterServiceForwarder<IArticlesService>("articles-service");
            services.AddMvc();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            app.UseRabbitMq();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                Container.Dispose();
            });
        }
    }
}
