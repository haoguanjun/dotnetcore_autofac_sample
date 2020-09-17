using System;
using autofac_sample;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Mvc;

public class ServiceAutofacModule : Autofac.Module {
    protected override void Load (ContainerBuilder builder) {

        // register interceptor
        builder.RegisterType<DbServiceInterceptor> ();

        // register type, and enable interceptor injection
        builder.RegisterType<DbService> ().As<IDbService> ()
            .EnableInterfaceInterceptors ()
            .InstancePerLifetimeScope ();

        /* make property autowire at one controller
        builder.RegisterType<WeatherForecastController>()
            .PropertiesAutowired();
        */

        // make property autowire at all api controller
        var controllerBaseType = typeof (ControllerBase);
        builder.RegisterAssemblyTypes (typeof (Program).Assembly)
            .Where (t => controllerBaseType.IsAssignableFrom (t) &&
                t != controllerBaseType)
            .PropertiesAutowired ();

        /*
        // register all assembly
        var assembly = assembly.Load ("Domain.Services");
        builder.registerAssemblyType (assembly)
            .AsImplementedInterfaces ()
            .InstancePerLifetimeScope ();
        */
        
        /*
        // filter types
        var controllerBaseType = typeof (ControllerBase);
        builder.RegisterAssemblyTypes (typeof (Program).Assembly)
            .Where (t => controllerBaseType.IsAssignableFrom (t) &&
                t != controllerBaseType)
            .PropertiesAutowired ();
        */

        /*
        // LoadFile
        var assemblyPath = string.Empty;
        var assemblyServices = assembly.LoadFile(assemblyPath );
        */

    }
}