using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.DAL.DataRepository.Basic;
using Exam.DAL.DB.Basic;
using Exam.DAL.IDataRepository.Basic;
using Exam.Models.Basic;
using XL.Framework.BLL;
using XL.Framework.DAL;
using XL.Framework.Utility;

namespace Exam.BLL.Service.Basic
{
    public class UserInfoService:ServiceBase,IServiceBase,IUserInfoService
    {
        private readonly IUserInfoDataRepository _userInfoDataRepository;

        public UserInfoService(IUserInfoDataRepository userInfoDataRepository)
        {
            _userInfoDataRepository= userInfoDataRepository;
        }

        public List<UserInfo> List()
        {
            var list= _userInfoDataRepository.FindAll<userinfo>("");
            return ObjToObj.ToObj<List<UserInfo>>(list);
        }
    }
}
