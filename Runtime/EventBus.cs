using System;
using System.Collections.Generic;

namespace LLib
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> Events = new();


        public static void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (Events.TryGetValue(type, out var del))
            {
                Events[type] = Delegate.Combine(del, handler);
            }
            else
            {
                Events[type] = handler;
            }
        }


        public static void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (Events.TryGetValue(type, out var del))
            {
                var result = Delegate.Remove(del, handler);
                if (result == null)
                {
                    Events.Remove(type);
                }
                else
                {
                    Events[type] = result;
                }
            }
        }


        public static void Publish<T>(T e)
        {
            if (Events.TryGetValue(typeof(T), out var del))
            {
                ((Action<T>)del)?.Invoke(e);
            }
        }


        public static void Clear()
        {
            Events.Clear();
        }
    }
}
