﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using XL.Framework.Contract;
using XL.Framework.Utility;

namespace XL.Framework.Web
{
    /// <summary>
    /// 异常处理方法
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 重写基类的异常处理方法
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var err = new ApiMessage<string>()
            {
                Success = false,
                MsgCode = "500",
                Message = context.Exception.Message
            };
            var errMsg = JsonConvert.SerializeObject(err);

            //记录异常 
            LogHelper.WriteLog(JsonConvert.SerializeObject(new {
                Message= context.Exception.Message,
                StackTrace=context.Exception.StackTrace
            }), LogHelper.LogType.Error);

            //系统异常码
            var oResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(errMsg)
            };
            context.Response = oResponse;
            //业务异常处理
            base.OnException(context);
        }

    }
}
