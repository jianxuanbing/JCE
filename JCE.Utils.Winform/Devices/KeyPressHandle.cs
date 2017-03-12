/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Winform.Devices
 * 文件名：KeyPressHandle
 * 版本号：v1.0.0.0
 * 唯一标识：3448be85-e774-4afb-aabb-423ef726c3ad
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/5 15:28:59
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/5 15:28:59
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCE.Utils.Winform.Devices
{
    /// <summary>
    /// 按键处理-全局键盘钩子
    /// </summary>
    public class KeyPressHandle
    {
        /// <summary>
        /// 键盘钩子构造
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        /// <summary>
        /// 钩子委托
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        static int hHook = 0;
        public const int WH_KEYBOARD_LL = 13;//如果LowLevel键盘截获，如果是WH_KEYBOARD=2，并不能对系统键盘截取，Acrobat Reader会在你截取之前获得键盘。
        static HookProc KeyBoardHookProcedure;

        /// <summary>
        /// 设置钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        /// <summary>
        /// 移除钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        /// <summary>
        /// 调用下一个钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user.dll")]
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
        /// <summary>
        /// 获取当前线程ID
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();
        /// <summary>
        /// 获取组件处理器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        /// <summary>
        /// 开始钩子事件
        /// </summary>
        public static void Hook_Start()
        {
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);
                hHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyBoardHookProcedure,
                    GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
                //如果设置钩子失败
                if (hHook == 0)
                {
                    Hook_Clear();
                }
            }
        }
        /// <summary>
        /// 取消钩子事件
        /// </summary>
        public static void Hook_Clear()
        {
            bool retKeyboard = true;
            if (hHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
        }

        public static int KeyBoardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                Keys k = (Keys)Enum.Parse(typeof(Keys), kbh.vkCode.ToString());
                switch (k)
                {
                    case Keys.F2:
                        if (kbh.flags == 0)
                        {
                            //这里写按下后做的事情
                        }
                        else if (kbh.flags == 128)
                        {
                            //这里写放开后做什么事
                        }
                        return 1;
                }
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }
    }
}
