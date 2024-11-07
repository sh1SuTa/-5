using Model;
using MyDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 用户信息的发送5
{
    /// <summary>
    /// ImageServer 的摘要说明
    /// </summary>
    public class ImageServer : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            ResultClass res = new ResultClass();
            try
            {
                String uName = My.decode(context.Request.QueryString["uName"]);
                String uPass = My.decode(context.Request.QueryString["uPass"]);
                res.state = "success";
                res.message = uName + "," + uPass;
            }
            catch (Exception exp) { res.state = "error"; res.message = exp.Message; }
            context.Response.ContentType = "text/plain";
            context.Response.Write(My.serialize<ResultClass>(res));
            context.Response.Flush();
        }
        public bool IsReusable
        {
            get { return false; }
        }
    }
}
