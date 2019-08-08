﻿/*
 https://autofaccn.readthedocs.io/en/latest/advanced/interceptors.html
 this is a demo for autofac interceptors
 */


using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            // create builder
            var builder = new ContainerBuilder();

            builder.RegisterType<SomeType>()
              .As<ISomeType>()
              .EnableInterfaceInterceptors()
              .InterceptedBy(typeof(CallLogger));
            // Typed registration
            builder.Register(c => new CallLogger(Console.Out));


            // Enable Interception on Types
            //builder.RegisterType<SomeType>()
            //       .As<ISomeType>()
            //       .EnableInterfaceInterceptors()
            //       .InterceptedBy(typeof(CallLogger));
            //// Named registration
            //builder.Register(c => new CallLogger(Console.Out))
            //       .Named<IInterceptor>("log-calls");


            var type = typeof(SomeType);
            var typeInfo = type.GetTypeInfo();
            var b = LoggerHelper.IsLoggerEnabled(typeInfo);




            var container = builder.Build();
            var willBeIntercepted = container.Resolve<ISomeType>();
            willBeIntercepted.Show("this is a test");
        }
    }
}