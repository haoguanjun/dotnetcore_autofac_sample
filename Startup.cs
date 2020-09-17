using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using autofac_sample.Controllers;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace autofac_sample {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddControllers ()
                .AddControllersAsServices ();

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "autofac_sample", Version = "v1" });
            });
        }

        public void ConfigureContainer (ContainerBuilder builder) {

            // use autofac module 
            builder.RegisterModule<ServiceAutofacModule>();

            /*
            // move to autofac module

            builder.RegisterType<DbServiceInterceptor> ();

            builder.RegisterType<DbService> ().As<IDbService> ()
                .EnableInterfaceInterceptors ()
                .InstancePerLifetimeScope ();

            // builder.RegisterType<WeatherForecastController>()
            //     .PropertiesAutowired();

            var controllerBaseType = typeof (ControllerBase);
            builder.RegisterAssemblyTypes (typeof (Program).Assembly)
                .Where (t => controllerBaseType.IsAssignableFrom (t) &&
                    t != controllerBaseType)
                .PropertiesAutowired ();
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            // If, for some reason, you need a reference to the built container, you
            // can use the convenience extension method GetAutofacRoot.
            // this.AutofacContainer = app.ApplicationServices.GetAutofacRoot ();

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseSwagger ();
                app.UseSwaggerUI (c => c.SwaggerEndpoint ("/swagger/v1/swagger.json", "autofac_sample v1"));
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}