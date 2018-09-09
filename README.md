# Autofac-Interception
[![Build status](https://ci.appveyor.com/api/projects/status/jo8dljw2xab0bfd0?svg=true)](https://ci.appveyor.com/project/patelhi/autofac-interception)

This repo contains sample project that implements cross cutting concerns in .Net core application using interception feature of Autofac

#### Configure interceptors (ex: logging ) in IoC Container
```
builder.Register(x => new LogInterceptor());
```

#### Configure interception for types in IoC Container
```
builder.RegisterType<BarService>().As<IBarService>().EnableInterfaceInterceptors();
builder.RegisterType<FooService>().As<IFooService>().EnableInterfaceInterceptors();
```

#### Types free of cross cutting concerns (notice no logging calls in these methods)
```
[Intercept(typeof(LogInterceptor))]
public class FooService : IFooService
{
    ...
    public double CalculateData(int minValue, int maxValue, int someOtherParam)
    {
        //Equivalent to getting data from back-end service
        var data = _barService.GetData(minValue, maxValue);

        //Equivalent to computing result based on business logic 
        var result = (double) data / someOtherParam;

        return result;
    }
}
public class BarService : IBarService
{
    ...
    public int GetData(int minValue, int maxValue)
    {
        return _randomNumberGenerator.Next(minValue, maxValue);
    }
}
```

#### Application Execution
```
var fooService = container.Resolve<IFooService>();
var result = fooService.CalculateData(30, 70, 10);

Console.WriteLine($"Value of result is: {result}");
```

#### Application Output (method calls logged by interceptor)
```
LogInterceptor::AutoFac_Interception.FooService.CalculateData() - called with 30,70,10
LogInterceptor::AutoFac_Interception.BarService.GetData() - called with 30,70
LogInterceptor::AutoFac_Interception.BarService.GetData() - returned 43
LogInterceptor::AutoFac_Interception.FooService.CalculateData() - returned 4.3
Value of result is: 4.3
```
