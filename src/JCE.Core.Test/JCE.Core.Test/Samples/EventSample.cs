using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Events;
using JCE.Core.Events.Bus.Factories;
using JCE.Core.Events.Bus.Handlers;

namespace JCE.Core.Test.Samples
{
    /// <summary>
    /// 事件样例
    /// </summary>
    public class EventSample:IEvent
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 事件处理器工厂样例
    /// </summary>
    public class EventHandlerFactorySample : IEventHandlerFactory
    {
        /// <summary>
        /// 事件处理器列表
        /// </summary>
        private readonly IEventHandler[] _handlers;

        /// <summary>
        /// 初始化一个<see cref="EventHandlerFactorySample"/>类型的实例
        /// </summary>
        /// <param name="handlers">事件处理器列表</param>
        public EventHandlerFactorySample(params IEventHandler[] handlers)
        {
            _handlers = handlers;
        }

        /// <summary>
        /// 获取事件处理器列表
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <returns></returns>
        public List<IEventHandler<TEvent>> GetHandler<TEvent>() where TEvent : IEvent
        {
            return _handlers.Select(t => t as IEventHandler<TEvent>).ToList();
        }
    }

    public class DefaultEventHandlerSample : IEventHandler<EventSample>
    {
        public void Handle(EventSample @event)
        {            
        }
    }

    public class EventHandlerSample:IEventHandler<EventSample>
    {
        public void Handle(EventSample @event)
        {
            Console.WriteLine("装一下逼先");
            Console.WriteLine(@event.Name);
        }
    }
}
