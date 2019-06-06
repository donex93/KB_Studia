using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using KnowledgeBase.Common.RabbitMQ;
using KnowledgeBase.Services.Articles.Data;
using KnowledgeBase.Services.Articles.Handlers;
using KnowledgeBase.Services.Articles.Messages.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KnowledgeBase.Services.Articles
{
    public class Startup
    {
        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ArticlesDbContext>
                (options => options.UseMySql(Configuration.GetConnectionString("Default")));

            services.AddMvc();
            services.AddScoped<GetArticleHandler>();

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateArticle>()
                .SubscribeCommand<UpdateArticle>()
                .SubscribeCommand<DeleteArticle>();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                Container.Dispose();
            });
        }
    }
}
