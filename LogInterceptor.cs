using System;
using System.Linq;
using Castle.DynamicProxy;

namespace AutoFac_Interception
{
    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = $"{invocation.TargetType.ToString()}.{invocation.Method.Name}()";
            var parameters = string.Join(',', invocation.Arguments.Select(x => x.ToString()).ToArray());
            Console.WriteLine($"LogInterceptor::{methodName} - called with {parameters}");
            invocation.Proceed();
            Console.WriteLine($"LogInterceptor::{methodName} - returned {invocation.ReturnValue}");
        }
    }
}