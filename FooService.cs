using System;
using Autofac.Extras.DynamicProxy;

namespace AutoFac_Interception
{
    [Intercept(typeof(LogInterceptor))]
    public class FooService : IFooService
    {
        private IBarService _barService;
        public FooService(IBarService barService)
        {
            _barService = barService;
        }
        public double CalculateData(int minValue, int maxValue, int someOtherParam)
        {
            //Equivalent to getting data from back-end service
            var data = _barService.GetData(minValue, maxValue);

            //Equivalent to computing result based on business logic. Ignore 
            var result = (double) data / someOtherParam;

            return result;
        }
    }
}