using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCE.Core.Events.Bus;
using JCE.Core.Events.Bus.Handlers;
using JCE.Core.Test.Samples;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JCE.Core.Test.Events
{
    /// <summary>
    /// 事件总线测试
    /// </summary>
    [TestClass]
    public class EventBusTest
    {
        /// <summary>
        /// 事件处理器
        /// </summary>
        private IEventHandler<EventSample> _handler;

        /// <summary>
        /// 事件总线
        /// </summary>
        private DefaultEventBus _eventBus;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _handler=new EventHandlerSample();
            var factory=new EventHandlerFactorySample(_handler);
            _eventBus=new DefaultEventBus(factory);
        }

        /// <summary>
        /// 测试发布事件
        /// </summary>
        [TestMethod]
        public void TestPublish()
        {
            var eventSample=new EventSample() {Name = "jce"};
            _eventBus.Publish(eventSample);
            _handler.Handle(eventSample);
        }
    }
}
