using System;
using System.Threading;

namespace BuyBackAPI.Utility
{
    public class DbClientFactory<T>
    {
        private static Lazy<T> _factoryLazy = new Lazy<T>(
            () => (T)Activator.CreateInstance(typeof(T)),
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static T instance
        {
            get
            {
                return _factoryLazy.Value;
            }
        }
    }
}
