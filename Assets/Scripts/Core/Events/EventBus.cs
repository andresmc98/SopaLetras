using System;
using System.Collections.Generic;

namespace Core.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _subscribers = new Dictionary<Type, List<Delegate>>();

        public static void Subscribe<T>(Action<T> callback) where T : GameEvent
        {
            var type = typeof(T);
            
            if(!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Delegate>();
            
            _subscribers[type].Add(callback);
        }

        public static void Unsubscribe<T>(Action<T> callback) where T : GameEvent
        {
            var type = typeof(T);
            
            if (!_subscribers.TryGetValue(type, out var subscribers))
                return;

            subscribers.Remove(callback);
        }

        public static void Publish<T>(T gameEvent) where T : GameEvent
        {
            var type = typeof(T);

            if (!_subscribers.ContainsKey(type))
                return;
            
            foreach (var del in _subscribers[type].ToArray())
                (del as Action<T>)?.Invoke(gameEvent);
        }

        public static void Clear()
        {
            _subscribers.Clear();
        }
    }
}