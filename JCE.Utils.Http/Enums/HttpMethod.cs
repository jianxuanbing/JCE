using System.ComponentModel;

namespace JCE.Utils.Http.Enums
{
    /// <summary>
    /// Http请求方式
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// GET，请求指定的页面信息，并返回实体主体。
        /// </summary>
        [Description("GET")]
        Get =0,
        /// <summary>
        /// POST，向指定资源提交数据进行处理请求（例如提交表单或者上传文件）。数据被包含在请求体中。POST请求可能会导致新的资源的建立和/或已有资源的修改。
        /// </summary>
        [Description("POST")]
        Post =10,
        /// <summary>
        /// PUT，从客户端向服务器传送的数据取代指定的文档的内容。
        /// </summary>
        [Description("PUT")]
        Put =20,
        /// <summary>
        /// DELETE，请求服务器删除指定的页面。
        /// </summary>
        [Description("DELETE")]
        Delete =30,
        /// <summary>
        /// HEAD，类似于get请求，只不过返回的响应中没有具体的内容，用于获取报头
        /// </summary>
        [Description("HEAD")]
        Head =40,
        /// <summary>
        /// OPTIONS，允许客户端查看服务器的性能。
        /// </summary>
        [Description("OPTIONS")]
        Options =50,
        ///// <summary>
        ///// TRACE，回显服务器收到的请求，主要用于测试或诊断。
        ///// </summary>
        //[Description("TRACE")]
        //Trace =60,
        ///// <summary>
        ///// CONNECT，HTTP/1.1协议中预留给能够将连接改为管道方式的代理服务器。
        ///// </summary>
        //[Description("CONNECT")]
        //Connect=70,
        /// <summary>
        /// PATCH，实体中包含一个表，表中说明与该URI所表示的原内容的区别。
        /// </summary>
        [Description("PATCH")]
        Patch =80,
        ///// <summary>
        ///// MOVE，请求服务器将指定的页面移至另一个网络地址。
        ///// </summary>
        //[Description("MOVE")]
        //Move =90,
        ///// <summary>
        ///// COPY，请求服务器将指定的页面拷贝至另一个网络地址。
        ///// </summary>
        //[Description("COPY")]
        //Copy =100,
        ///// <summary>
        ///// LINK，请求服务器建立链接关系。
        ///// </summary>
        //[Description("LINK")]
        //Link =110,
        ///// <summary>
        ///// UNLINK，断开链接关系。
        ///// </summary>
        //[Description("UNLINK")]
        //UnLink =120,
        ///// <summary>
        ///// WRAPPED，允许客户端发送经过封装的请求。
        ///// </summary>
        //[Description("WRAPPED")]
        //Wrapped =130,
        ///// <summary>
        ///// Extension-mothed，在不改动协议的前提下，可增加另外的方法。
        ///// </summary>
        //[Description("Extension-mothed")]
        //ExtensionMothed =140
    }
}
