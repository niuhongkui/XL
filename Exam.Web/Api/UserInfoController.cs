using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Exam.BLL.Service.Basic;
using XL.Framework.Web;
using XL.Framework.Contract;
using Exam.Models.Basic;
using XL.Framework.Utility;

namespace Exam.Web.Api
{
    [AllowAnonymous]
    public class UserInfoController : BaseApiController
    {
        private IUserInfoService _userInfoService { get; set; }

        public UserInfoController(IUserInfoService userInfoService) {
            _userInfoService = userInfoService;
        }

        public ApiMessage<bool> Login(UserInfo user)
        {
            var i = Convert.ToInt32("3f");
            LogHelper.WriteLog("debug");
            return null;
        }
    }
}
