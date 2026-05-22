using System;
using System.Collections.Concurrent;


namespace FacadeCompanyName.FacadeProjectName.Web.Core.AspNetCore.Builders
{
    public static class TrimEngine
    {
        private static readonly ConcurrentDictionary<Type, Action<object>> _cache = new();

        public static void Trim(object obj)
        {
            try
            {
                if (obj == null) return;

                var type = obj.GetType();

                var handler = _cache.GetOrAdd(type, CreateHandler);

                handler(obj);
            }
            catch { }
        }

        private static Action<object> CreateHandler(Type type)
        {
            return TrimExpressionBuilder.Build(type);
        }
    }
}