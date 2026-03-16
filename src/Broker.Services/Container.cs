using Broker.Contracts.Repositories;
using Broker.Services.Repositories;
using System;
using System.Collections.Concurrent;

namespace Broker.Services
{
    public static class Container
    {
        private static readonly ConcurrentDictionary<Type, object> _instances = new ConcurrentDictionary<Type, object>();

        static Container()
        {
            RegisterInstance<ITopicRepository>(new TopicRepository());
        }

        public static void RegisterInstance<T>(T instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            _instances[typeof(T)] = instance;
        }

        public static T GetInstance<T>()
        {
            if (_instances.TryGetValue(typeof(T), out var instance))
                return (T)instance;

            throw new InvalidOperationException($"No instance registered for type {typeof(T).FullName}");
        }
    }
}
