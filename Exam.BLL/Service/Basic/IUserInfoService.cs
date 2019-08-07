using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DAL.DB.Basic;
using Exam.Models.Basic;
using XL.Framework.BLL;

namespace Exam.BLL.Service.Basic
{
   public  interface IUserInfoService : IServiceBase
   {
       List<UserInfo> List();
   }
}
