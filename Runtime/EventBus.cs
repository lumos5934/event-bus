using System;
using System.Collections.Generic;

namespace LLib
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _events = new();
        private static readonly object _lock = new();

        public static IEventSubscription Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            lock (_lock)
            {
                if (!_events.TryGetValue(type, out var handlers))
                {
                    handlers = new List<Delegate>();
                    _events[type] = handlers;
                }
                
                handlers.Add(handler);
            }
            
            return new EventSubscription(() => Unsubscribe(handler));
        }
        
        public static void Publish<T>(T e)
        {
            List<Delegate> snapshot;
            lock (_lock)
            {
                if (!_events.TryGetValue(typeof(T), out var handlers)) 
                    return;
                
                snapshot = new List<Delegate>(handlers);
            }

            foreach (var handler in snapshot)
            {
                try
                {
                    ((Action<T>)handler).Invoke(e);
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"[EventBus] Error in {typeof(T)}: {ex}");
                }
            }
        }
        
        private static void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            lock (_lock)
            {
                if (_events.TryGetValue(type, out var handlers))
                {
                    handlers.Remove(handler);
                    
                    if (handlers.Count == 0)
                    {
                        _events.Remove(type);
                    }
                }
            }
        }
    }
}