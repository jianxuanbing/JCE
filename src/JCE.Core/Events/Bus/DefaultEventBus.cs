using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Events.Bus.Factories;

namespace JCE.Core.Events.Bus
{
    /// <summary>
    /// 默认事件总线
    /// </summary>
    public class DefaultEventBus:IEventBus
    {
        /// <summary>
        /// 事件处理器工厂
        /// </summary>
        public IEventHandlerFactory Factory { get; set; }

        /// <summary>
        /// 初始化一个<see cref="DefaultEventBus"/>类型的实例
        /// </summary>
        /// <param name="factory"></param>
        public DefaultEventBus(IEventHandlerFactory factory)
        {
            Factory = factory;
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = Factory.GetHandler<TEvent>();
            foreach (var handler in handlers)
            {
                handler.Handle(@event);                    
            }
        }
    }
}