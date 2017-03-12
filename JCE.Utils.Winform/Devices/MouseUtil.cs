/************************************************************************************
 * Copyright (c) 2017 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.Winform.Devices
 * 文件名：MouseUtil
 * 版本号：v1.0.0.0
 * 唯一标识：3b246cf3-c913-4746-852d-e7463b93921a
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2017/3/4 17:57:39
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2017/3/4 17:57:39
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace JCE.Utils.Winform.Devices
{
    /// <summary>
    /// 鼠标工具类 - 模拟鼠标点击
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand,Resources = HostProtectionResource.ExternalProcessMgmt)]
    public class MouseUtil
    {
        #region Property(属性)
        /// <summary>
        /// 检查鼠标是否已经安装
        /// </summary>
        public static bool MousePresent
        {
            get { return SystemInformation.MousePresent; }
        }

        /// <summary>
        /// 检查鼠标是否存在滚轮
        /// </summary>
        public static bool WheelExists
        {
            get
            {
                if (!SystemInformation.MousePresent)
                {
                    throw new InvalidOperationException("没有找到鼠标.");
                }
                return SystemInformation.MouseWheelPresent;
            }
        }

        /// <summary>
        /// 获取鼠标滚轮每次滚动的行数
        /// </summary>
        public static int WheelScrollLines
        {
            get
            {
                if (!WheelExists)
                {
                    throw new InvalidOperationException("没有找到鼠标滑轮.");
                }
                return SystemInformation.MouseWheelScrollLines;
            }
        }
        #endregion

        #region 鼠标操作函数
        /// <summary>
        /// 鼠标事件
        /// </summary>
        /// <param name="flags">鼠标事件标志</param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="data"></param>
        /// <param name="extraInfo"></param>
        [DllImport("user32.dll")]
        private static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        /// <summary>
        /// 鼠标事件标志
        /// </summary>
        [Flags]
        enum MouseEventFlag:uint
        {
            /// <summary>
            /// 移动
            /// </summary>
            Move = 0x0001,
            /// <summary>
            /// 左键点击
            /// </summary>
            LeftDown = 0x0002,
            /// <summary>
            /// 左键松开
            /// </summary>
            LeftUp = 0x0004,
            /// <summary>
            /// 右键点击
            /// </summary>
            RightDown = 0x0008,
            /// <summary>
            /// 右键松开
            /// </summary>
            RightUp = 0x0010,
            /// <summary>
            /// 中间键点击
            /// </summary>
            MiddleDown = 0x0020,
            /// <summary>
            /// 中间键松开
            /// </summary>
            MiddleUp = 0x0040,
            /// <summary>
            /// 下滑
            /// </summary>
            XDown = 0x0080,
            /// <summary>
            /// 上滑
            /// </summary>
            XUp = 0x0100,
            /// <summary>
            /// 滚动
            /// </summary>
            Wheel = 0x0800,
            /// <summary>
            /// 虚拟键
            /// </summary>
            VirtualDesk = 0x4000,
            /// <summary>
            /// 物理键
            /// </summary>
            Absolute = 0x8000
        }

        /// <summary>
        /// 连续两次鼠标点击之间会被处理成双击事件的间隔时间
        /// </summary>
        /// <returns>以毫秒表示的双击时间</returns>
        [DllImport("user32.dll",EntryPoint = "GetDoubleClickTime")]
        public static extern int GetDoubleClickTime();

        /// <summary>
        /// 获取光标的位置，以屏幕坐标表示
        /// </summary>
        /// <param name="lpPoint">POINT结构指针，该结构接收光标的屏幕坐标</param>
        /// <returns>如果成功，返回值非零，如果失败，返回值为零</returns>
        [DllImport("user32.dll",EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos(Point lpPoint);

        /// <summary>
        /// 把光标移到屏幕的指定位置。如果新位置不在由ClipCursor函数设置的屏幕矩形区域之内，则系统自动调整坐标，使得光标在矩形之内
        /// </summary>
        /// <param name="x">指定光标的新的x坐标，以屏幕坐标表示</param>
        /// <param name="y">指定光标的新的y坐标，以屏幕坐标表示</param>
        /// <returns>如果成功，返回非零值，如果失败，返回值为零</returns>
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

        #endregion

        /// <summary>
        /// 在当前鼠标的位置左键点击一下
        /// </summary>
        public static void MouseClick()
        {
            mouse_event(MouseEventFlag.LeftDown, 0,0,0,UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary>
        /// 移动到坐标位置点击
        /// </summary>
        /// <param name="location">要点击的坐标位置，屏幕绝对值</param>
        public static void MouseClick(Point location)
        {
            MouseMove(location);
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary>
        /// 移动到坐标位置右击
        /// </summary>
        /// <param name="location">要点击的坐标位置，屏幕绝对值</param>
        public static void MouseRightClick(Point location)
        {
            MouseMove(location);
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary>
        /// 移动到坐标位置
        /// </summary>
        /// <param name="location">指定光标的坐标位置，屏幕绝对值</param>
        public static void MouseMove(Point location)
        {
            SetCursorPos(location.X, location.Y);
        }
    }
}
