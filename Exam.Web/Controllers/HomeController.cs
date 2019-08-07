using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.BLL.Service.Basic;

namespace Exam.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserInfoService _userInfoService;

        public HomeController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var list = _userInfoService.List();
            return View();
        }
    }
}