using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using XL.Framework.Utility;
using System.Collections.Specialized;
using System.Web.Routing;
using Newtonsoft.Json;
using XL.Framework.Contract;

namespace XL.Framework.Web
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 操作人，传IP....到后端记录
        /// </summary>
        public virtual Operater Operater
        {
            get
            {
                var user = Session["user"];
                return user == null ? null : JsonConvert.DeserializeObject<Operater>(user.ToString());
            }
        }

        /// <summary>
        /// 分页大小
        /// </summary>
        //public virtual int PageSize
        //{
        //    get
        //    {
        //        return 15;
        //    }
        //}


        /// <summary>
        /// 在方法执行前更新操作人
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void UpdateOperater(ActionExecutingContext filterContext)
        {
            if (this.Operater == null)
                return;

            //WCFContext.Current.Operater = this.Operater;
        }

        public virtual void ClearOperater()
        {
            //TODO
        }

        /// <summary>
        /// AOP拦截，在Action执行后
        /// </summary>
        /// <param name="filterContext">filter context</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() && !filterContext.IsChildAction)
                this.ClearOperater();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.UpdateOperater(filterContext);
            base.OnActionExecuting(filterContext);

            //在方法执行前，附加上PageSize值
            //filterContext.ActionParameters.Values.Where(v => v is Request).ToList().ForEach(v => ((Request)v).PageSize = this.PageSize);
        }

        /// <summary>
        /// 发生异常写Log
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            var e = filterContext.Exception;
            LogException(e);
        }

        protected virtual void LogException(Exception exception, WebExceptionContext exceptionContext = null)
        {
            //do nothing!
        }
    }

    public class WebExceptionContext
    {
        public string IP { get; set; }
        public string CurrentUrl { get; set; }
        public string RefUrl { get; set; }
        public bool IsAjaxRequest { get; set; }
        public NameValueCollection FormData { get; set; }
        public NameValueCollection QueryData { get; set; }
        public RouteValueDictionary RouteData { get; set; }
    }
}
