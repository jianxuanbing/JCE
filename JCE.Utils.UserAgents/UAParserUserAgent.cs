/************************************************************************************
 * Copyright (c) 2016 All Rights Reserved. 
 * CLR版本：4.0.30319.42000
 * 机器名称：JIAN
 * 命名空间：JCE.Utils.UserAgents
 * 文件名：UAParserUserAgent
 * 版本号：v1.0.0.0
 * 唯一标识：7216e981-108a-42d5-8e86-e11255830765
 * 当前的用户域：JIAN
 * 创建人：简玄冰
 * 电子邮箱：jianxuanhuo1@126.com
 * 创建时间：2016/7/10 9:55:05
 * 描述：
 *
 * =====================================================================
 * 修改标记：
 * 修改时间：2016/7/10 9:55:05
 * 修改人：简玄冰
 * 版本号：v1.0.0.0
 * 描述：
 *
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using UAParser;

namespace JCE.Utils.UserAgents
{
    /// <summary>
    /// UAParser用户代理
    /// </summary>
    public class UAParserUserAgent:IUserAgent
    {
        private static readonly Parser s_uap;

        private static readonly Regex s_pdfConverterPattern = new Regex(@"wkhtmltopdf",
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        #region Mobile UAs, OS & Devices
        private static readonly HashSet<string> s_MobileOS = new HashSet<string>
        {
            "Android", 
            "iOS", 
            "Windows Mobile", 
            "Windows Phone", 
            "Windows CE",
            "Symbian OS", 
            "BlackBerry OS", 
            "BlackBerry Tablet OS", 
            "Firefox OS", 
            "Brew MP", 
            "webOS",
            "Bada",
            "Kindle",
            "Maemo"
        };

        private static readonly HashSet<string> s_MobileBrowsers = new HashSet<string>
        {
            "Android", 
            "Firefox Mobile", 
            "Opera Mobile", 
            "Opera Mini", 
            "Mobile Safari",
            "Amazon Silk", 
            "webOS Browser", 
            "MicroB", 
            "Ovi Browser", 
            "NetFront", 
            "NetFront NX",
            "Chrome Mobile", 
            "Chrome Mobile iOS", 
            "UC Browser", 
            "Tizen Browser", 
            "Baidu Explorer", 
            "QQ Browser Mini",
            "QQ Browser Mobile", 
            "IE Mobile", 
            "Polaris", 
            "ONE Browser", 
            "iBrowser Mini", 
            "Nokia Services (WAP) Browser",
            "Nokia Browser", 
            "Nokia OSS Browser", 
            "BlackBerry WebKit", 
            "BlackBerry", "Palm", 
            "Palm Blazer",
            "Palm Pre", 
            "Teleca Browser", 
            "SEMC-Browser", 
            "PlayStation Portable", 
            "Nokia", 
            "Maemo Browser",
            "Obigo", 
            "Bolt", 
            "Iris", 
            "UP.Browser", 
            "Minimo", 
            "Bunjaloo",
            "Jasmine", 
            "Dolfin", 
            "Polaris",
            "Skyfire"
        };

        private static readonly HashSet<string> s_MobileDevices = new HashSet<string>
        {
            "BlackBerry", 
            "MI PAD", 
            "iPhone", 
            "iPad", 
            "iPod",
            "Kindle", 
            "Kindle Fire", 
            "Nokia", 
            "Lumia", 
            "Palm", 
            "DoCoMo",
            "HP TouchPad",
            "Xoom",
            "Motorola",
            "Generic Feature Phone",
            "Generic Smartphone"
        };
        #endregion

        private readonly HttpContextBase _httpContext;

        private string _rawValue;
        private UserAgentInfo _userAgent;
        private DeviceInfo _device;
        private OSInfo _os;

        private bool? _isBot;
        private bool? _isMobileDevice;
        private bool? _isTablet;
        private bool? _isPdfConverter;
        static UAParserUserAgent()
        {
            s_uap = UAParser.Parser.GetDefault();
        }

        public UAParserUserAgent(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        public string RawValue
        {
            get
            {
                if (_rawValue == null)
                {
                    if (_httpContext.Request != null)
                    {
                        _rawValue = _httpContext.Request.UserAgent.ToString();
                    }
                    else
                    {
                        _rawValue = "";
                    }
                }

                return _rawValue;
            }
            // for (unit) test purpose
            set
            {
                _rawValue = value;
                _userAgent = null;
                _device = null;
                _os = null;
                _isBot = null;
                _isMobileDevice = null;
                _isTablet = null;
                _isPdfConverter = null;
            }
        }

        public virtual UserAgentInfo UserAgent
        {
            get
            {
                if (_userAgent == null)
                {
                    var tmp = s_uap.ParseUserAgent(this.RawValue);
                    _userAgent = new UserAgentInfo(tmp.Family, tmp.Major, tmp.Minor, tmp.Patch);
                }
                return _userAgent;
            }
        }

        public virtual DeviceInfo Device
        {
            get
            {
                if (_device == null)
                {
                    var tmp = s_uap.ParseDevice(this.RawValue);
                    _device = new DeviceInfo(tmp.Family, tmp.IsSpider);
                }
                return _device;
            }
        }

        public virtual OSInfo OS
        {
            get
            {
                if (_os == null)
                {
                    var tmp = s_uap.ParseOS(this.RawValue);
                    _os = new OSInfo(tmp.Family, tmp.Major, tmp.Minor, tmp.Patch, tmp.PatchMinor);
                }
                return _os;
            }
        }

        public virtual bool IsBot
        {
            get
            {
                if (!_isBot.HasValue)
                {
                    _isBot = _httpContext.Request.Browser.Crawler || this.Device.IsBot;
                }
                return _isBot.Value;
            }
        }

        public virtual bool IsMobileDevice
        {
            get
            {
                if (!_isMobileDevice.HasValue)
                {
                    _isMobileDevice =
                        s_MobileOS.Contains(this.OS.Family) ||
                        s_MobileBrowsers.Contains(this.UserAgent.Family) ||
                        s_MobileDevices.Contains(this.Device.Family);
                }
                return _isMobileDevice.Value;
            }
        }

        public virtual bool IsTablet
        {
            get
            {
                if (!_isTablet.HasValue)
                {
                    _isTablet =
                        Regex.IsMatch(this.Device.Family, "iPad|Kindle Fire|Nexus 10|Xoom|Transformer|MI PAD|IdeaTab", RegexOptions.CultureInvariant) ||
                        this.OS.Family == "BlackBerry Tablet OS";
                }
                return _isTablet.Value;
            }
        }

        public virtual bool IsPdfConverter
        {
            get
            {
                if (!_isPdfConverter.HasValue)
                {
                    _isPdfConverter = s_pdfConverterPattern.IsMatch(this.RawValue);
                }
                return _isPdfConverter.Value;
            }
        }
    }
}
