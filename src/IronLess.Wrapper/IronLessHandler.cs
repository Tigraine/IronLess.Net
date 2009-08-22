namespace IronLess.Wrapper
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;

    public class IronLessHandler : IHttpHandler
    {
        private IronLessExecuter ironLess = new IronLessExecuter();
        public void ProcessRequest(HttpContext context)
        {
            string lessFile = context.Server.MapPath(context.Request.Url.LocalPath);
            var regex = new Regex(@"(\.less$)");
            string cssFile = regex.Replace(lessFile, ".css");
            ironLess.CompileLess(lessFile, cssFile);
            context.Response.TransmitFile(cssFile);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}