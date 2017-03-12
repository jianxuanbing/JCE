/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Extensions
 * 文件名：EventHandlerExtenders
 * 版本号：v1.0.0.0
 * 唯一标识：790ca5af-1aaf-4510-9a46-b29c688dc728
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/4/22 21:46:41
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/4/22 21:46:41
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Utils.Extensions
{
    /// <summary>
    /// 事件处理器（EventHandler）扩展
    /// </summary>
    public static class EventHandlerExtenders
    {
        /// <summary>
        /// 引用任何类型事件的实现事件到当前的线程上
        /// </summary>
        /// <typeparam name="TEventArgs">引用事件类型</typeparam>
        /// <param name="eventHandler">事件处理器</param>
        /// <param name="sender">需要传递的对象实例</param>
        /// <param name="e">要传递的事件</param>
        /// <example>
        /// <code>
        ///   public class MyEventArgs : EventArgs
        ///    {
        ///        private string msg;
        ///
        ///        public MyEventArgs(string messageData)
        ///        {
        ///            msg = messageData;
        ///       }
        ///        public string Message
        ///        {
        ///            get { return msg; }
        ///            set { msg = value; }
        ///        }
        ///    }
        ///   public class ClassWithACustomEvent
        ///   {
        ///     // Declare an event of delegate type EventHandler of 
        ///     // MyEventArgs.
        ///
        ///       public event EventHandler<MyEventArgs/> SampleEvent;
        ///
        ///       public void OnDemoEvent(MyEventArgs e)
        ///       {
        ///           // Raise the event on the current thread
        ///           SampleEvent.RaiseEvent(this, e);
        ///       }
        ///
        ///       public void OnDemoEventUIThread(MyEventArgs e)
        ///        {
        ///           // Raise the event on the subscribers UI thread, if possible
        ///          SampleEvent.RaiseEventOnUIThread(this, e);
        ///     }
        ///   }
        ///   public class Sample
        ///   {
        ///     public static void Main()
        ///      {
        ///         ClassWithACustomEvent theClass = new ClassWithACustomEvent();
        ///         theClass.SampleEvent += new EventHandler<MyEventArgs/>(SampleEventHandler);
        ///         theClass.OnDemoEvent(new MyEventArgs("Hey there, Bruce!"));
        ///         theClass.OnDemoEvent(new MyEventArgs("How are you today?"));
        ///         theClass.OnDemoEventUIThread(new MyEventArgs("I'm pretty good."));
        ///         theClass.OnDemoEventUIThread(new MyEventArgs("Thanks for asking!"));
        ///      }
        ///
        ///       private static void SampleEventHandler(object src, MyEventArgs e)
        ///      {
        ///         Console.WriteLine(e.Message);
        ///     }
        ///   }
        ///   /*
        ///   This example produces the following results:
        ///
        ///   Hey there, JT!
        ///   How are you today?
        ///   I'm pretty good.
        ///   Thanks for asking!
        ///
        ///   */
        /// </code>
        /// </example>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender,
            TEventArgs e) where TEventArgs : EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(sender, e);
            }
        }
        /// <summary>
        /// 引用任何类型事件的实现事件到当前的UI线程上
        /// </summary>
        /// <typeparam name="TEventArgs">引用事件类型</typeparam>
        /// <param name="eventHandler">事件处理器</param>
        /// <param name="sender">需要传递的对象实例</param>
        /// <param name="e">要传递的事件</param>
        public static void RaiseEventOnUiThread<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender,
            TEventArgs e) where TEventArgs : EventArgs
        {
            if (eventHandler != null)
            {
                foreach (var @delegate in eventHandler.GetInvocationList())
                {
                    var singleCast = (EventHandler<TEventArgs>)@delegate;
                    var syncInvoke = singleCast.Target as ISynchronizeInvoke;
                    if (syncInvoke != null && syncInvoke.InvokeRequired)
                    {
                        //调用订阅服务器线程上的事件
                        syncInvoke.Invoke(eventHandler, new[]
                        {
                            sender, e
                        });
                    }
                    else
                    {
                        //引发此线程上的事件
                        singleCast(sender, e);
                    }
                }
            }
        }
    }
}
