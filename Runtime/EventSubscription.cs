
using System;

namespace LLib
{
    internal class EventSubscription : IEventSubscription
    {
        private Action _unsubscribe;

        public EventSubscription(Action unsubscribe)
        {
            _unsubscribe = unsubscribe;
        }
        
        public void Dispose()
        {
            _unsubscribe?.Invoke();
            _unsubscribe = null;
        }
    }
}
