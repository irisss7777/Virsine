using System;
using System.Collections.Concurrent;
using Zenject;
using MessagePipe;

namespace Plugins.MessagePipe.MessageBus.Runtime
{
    public class MessageBus : IDisposable
    {
        private readonly DiContainer _container;
        
        private readonly ConcurrentDictionary<Type, object> _publisherCache = new();
        private readonly ConcurrentDictionary<Type, object> _subscriberCache = new();
        
        [Inject]
        public MessageBus(DiContainer container)
        {
            _container = container;
        }

        public void Subscribe<T>(Action<T> action, IMessageDisposable messageDisposable)
        {
            var subscriber = GetSubscriber<T>();

            var disposable = subscriber.Subscribe(action);

            messageDisposable.OnDispose += OnDispose;
            
            return;

            void OnDispose()
            {
                disposable.Dispose();
                messageDisposable.OnDispose -= OnDispose;
            }
        }
        
        public void Publish<T>(T dto)
        {
            var publisher = GetPublisher<T>();
            publisher.Publish(dto);
        }

        private ISubscriber<T> GetSubscriber<T>()
        {
            var type = typeof(T);
            if (!_subscriberCache.TryGetValue(type, out var subscriber))
            {
                subscriber = _container.Resolve<ISubscriber<T>>();
                _subscriberCache[type] = subscriber;
            }
            return (ISubscriber<T>)subscriber;
        }
        
        private IPublisher<T> GetPublisher<T>()
        {
            var type = typeof(T);
            if (!_publisherCache.TryGetValue(type, out var publisher))
            {
                publisher = _container.Resolve<IPublisher<T>>();
                _publisherCache[type] = publisher;
            }
            return (IPublisher<T>)publisher;
        }

        public void Dispose()
        {
            _publisherCache.Clear();
            _subscriberCache.Clear();
        }
    }
}