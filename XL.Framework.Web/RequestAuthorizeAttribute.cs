﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using XL.Core.CacheHelper;
using XL.Framework.Contract;

namespace XL.Framework.Web
{
    /// <summary>
    /// 接口的身份验证
    /// </summary>
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        //重写基类的验证方式，加入我们自定义的Ticket验证
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {            
            if (((ReflectedHttpActionDescriptor)actionContext.ActionDescriptor).MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), true)|| actionContext.ActionDescriptor.ControllerDescriptor.ControllerType.IsDefined(typeof(AllowAnonymousAttribute), true))
                base.OnAuthorization(actionContext);
            else
            {
                var err = new ApiMessage<string>()
                {
                    Success = false,
                    MsgCode = "401",
                    Message = "请登录系统"
                };
                var errMsg = JsonConvert.SerializeObject(err);

                //系统异常码
                var oResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(errMsg)
                };

                //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
                var authorization = actionContext.Request.Headers.Authorization;
                if (authorization?.Parameter != null)
                {
                    //解密用户ticket,并校验用户名密码是否匹配
                    var encryptTicket = authorization.Parameter;
                    if (!ValidateTicket(encryptTicket))
                    {
                        actionContext.Response = oResponse;
                    }
                }
                else
                {
                    actionContext.Response = oResponse;
                }
                base.IsAuthorized(actionContext);
            }
        }

        //校验用户名密码（正式环境中应该是数据库校验）
        private bool ValidateTicket(string encryptTicket)
        {
            var user = (Operater)CacheHelper.GetCache(encryptTicket);
            return user != null;
        }
    }


    /// <summary>
    /// 允许WebApi跨域访问
    /// </summary>
    public class EnableCorsAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="origins">允许跨域访问的域名(协议/端口不一致都算不同域名,默认*)</param>
        /// <param name="headers">允许跨域的请求标头(默认*)</param>
        /// <param name="methods">允许跨域的请求方法(默认*)</param>
        public EnableCorsAttribute(string origins, string headers, string methods)
        {
            this.Origins = origins;
            this.Headers = headers;
            this.Methods = methods;
        }

        public string Origins { get; set; }
        public string Methods { get; set; }
        public string Headers { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //if (actionExecutedContext.Response != null)
            //{
            //    actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", this.Origins);
            //    actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Method", this.Methods);
            //    actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Headers", this.Headers);
            //}

            base.OnActionExecuted(actionExecutedContext);
        }




    }
}
