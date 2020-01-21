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

        public BaseController() {
            ViewBag.Title = "开发练习";
        }
    }

}
