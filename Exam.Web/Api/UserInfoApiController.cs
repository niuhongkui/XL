using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Exam.BLL.Service.Basic;
using XL.Framework.Web;
using XL.Framework.Contract;

namespace Exam.Web.Api
{
    //[AllowAnonymous]
    //[RequestAuthorize]
    [WebApiExceptionFilter]
    public class UserInfoController : ApiController
    {
        private IUserInfoService _userInfoService { get; set; }

        public UserInfoController(IUserInfoService userInfoService) {
            _userInfoService = userInfoService;
        }

        public ApiMessage<bool> Login()
        {
           var list= _userInfoService.List();
            return null;
        }
    }
}
