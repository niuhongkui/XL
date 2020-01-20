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
    [WebApiExceptionFilter]
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Cache或者Cookie的Key前缀
        /// </summary>
        public virtual string KeyPrefix
        {
            get
            {
                return "Context_";
            }
        }

        protected Operater UserInfo
        {
            get
            {
                var auth = Request.Headers.Authorization?.Parameter;
                var user = CacheHelper.GetCache(auth);
                return user != null ? (Operater)user : null;
            }
        }

    }
}
