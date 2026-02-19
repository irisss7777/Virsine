using System;

namespace Plugins.MessagePipe.MessageBus.Runtime
{
    public interface IMessageDisposable : IDisposable
    {
        public event Action OnDispose;
    }
}