using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using XL.Core.CacheHelper;
using XL.Framework.Contract;

namespace XL.Framework.Web
{
    [RequestAuthorize]
    public class BaseApiController : ApiController
    {
        protected Operater UserInfo
        {
            get
            {

                var auth = Request.Headers.Authorization?.Parameter;
                var user = (Operater)CacheHelper.GetCache(auth);
                return user;
            }
        }
    }
}
