using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

namespace AutoFac_Interception
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            
            //Configure application types in IoC Container
            builder.RegisterType<BarService>().As<IBarService>().EnableInterfaceInterceptors();
            builder.RegisterType<FooService>().As<IFooService>().EnableInterfaceInterceptors();

            //Configure interception types in IoC Container
            builder.Register(x => new LogInterceptor());
            var container = builder.Build();
                        
            //Fetch and run service code
            var fooService = container.Resolve<IFooService>();
            var result = fooService.CalculateData(30, 70, 10);
                        
            Console.WriteLine($"Value of result is: {result}");
        }
    }
}
