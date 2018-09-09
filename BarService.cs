using System;
using Autofac.Extras.DynamicProxy;

namespace AutoFac_Interception
{
    [Intercept(typeof(LogInterceptor))]
    public class BarService : IBarService
    {
        private Random _randomNumberGenerator = null;
        public BarService()
        {
            this._randomNumberGenerator = new Random();
        }
        public int GetData(int minValue, int maxValue)
        {
            return _randomNumberGenerator.Next(minValue, maxValue);
        }
    }
}