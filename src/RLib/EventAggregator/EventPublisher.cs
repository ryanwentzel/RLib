/*
Copyright (c) 2009 Derick Bailey. All Rights Reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

using System;
using System.Collections.Generic;

namespace RLib.EventAggregator
{
    public class EventPublisher : IEventPublisher
    {
        private EventHandlers EventHandlers { get; set; }
        private LatchManager<Type> Latches { get; set; }
        private IDictionary<Type, object> Publications { get; set; }

        public EventPublisher()
        {
            Latches = new LatchManager<Type>();
            EventHandlers = new EventHandlers();
            Publications = new Dictionary<Type, object>();
        }

        public EventHandlerOptions RegisterHandler<T>(IEventHandler<T> eventHandler)
        {
            EventHandlerOptions handlerOptions = new EventHandlerOptions(eventHandler);
            EventHandlers.Add<T>(handlerOptions);
            return handlerOptions;
        }

        public IList<EventHandlerOptions> RegisterHandlers(object eventHandler)
        {
            List<EventHandlerOptions> handlerOptionsList = new List<EventHandlerOptions>();
            Type[] handlers = eventHandler.GetType().GetInterfaces();

            Type eventHandlerType = typeof(IEventHandler<>);

            foreach (Type handler in handlers)
            {
                if (handler.Name.Equals(eventHandlerType.Name))
                {
                    Type eventType = handler.GetGenericArguments()[0];
                    EventHandlerOptions handlerOptions = new EventHandlerOptions(eventHandler);
                    EventHandlers.Add(eventType, handlerOptions);
                    handlerOptionsList.Add(handlerOptions);
                }
            }

            return handlerOptionsList;
        }

        public void UnregisterHandler<T>(IEventHandler<T> eventHandler)
        {
            EventHandlers.Remove(eventHandler);
        }

        public void UnregisterHandlers(object eventHandler)
        {
            Type[] handlers = eventHandler.GetType().GetInterfaces();

            Type eventHandlerType = typeof(IEventHandler<>);

            foreach (Type handler in handlers)
            {
                if (handler.Name.Equals(eventHandlerType.Name))
                {
                    Type eventType = handler.GetGenericArguments()[0];
                    EventHandlers.Remove(eventType, eventHandler);
                }
            }
        }

        public void Publish<T>(T eventData)
        {
            Type handleType = typeof(T);
            Latches.RunWithLock(handleType, delegate
            {
                SetPublication(eventData);
                EventHandlers.Handle(eventData);
            });
        }

        public T GetMostRecentPublication<T>()
        {
            T publicationValue;

            if (Publications.ContainsKey(typeof(T)))
                publicationValue = (T)Publications[typeof(T)];
            else
                publicationValue = default(T);

            return publicationValue;
        }

        public void OnHandlerError(Action<Exception> errorHandler)
        {
            EventHandlers.OnHandlerError(errorHandler);
        }

        private void SetPublication<T>(T eventData)
        {
            if (Publications.ContainsKey(typeof(T)))
                Publications[typeof(T)] = eventData;
            else
                Publications.Add(typeof(T), eventData);
        }
    }
}
