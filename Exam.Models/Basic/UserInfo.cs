using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Models.Basic
{
    public class UserInfo
    {
        
        public string ID { get; set; }
        
        public string UserName { get; set; }
        
        public string UserCode { get; set; }
        
        public string PassWord { get; set; }
        
        public int IsMember { get; set; }
        
        public int IsActive { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public string Phone { get; set; }
        
        public string ImgUrl { get; set; }
    }
}
