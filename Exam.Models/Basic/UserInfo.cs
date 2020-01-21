using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models.Basic
{
    public class UserInfo
    {
        public string UserName { get; set; }
        
        public string loginCode { get; set; }
        
        public string passWord { get; set; }
        
        public int rememberPwd { get; set; }
        
        public bool isForceLogin { get; set; }
        
    }
}
