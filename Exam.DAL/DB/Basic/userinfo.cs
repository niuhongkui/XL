using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;
using XL.Framework.Contract;
using XL.Framework.DAL;

namespace Exam.DAL.DB.Basic
{
    [TableName("portal.userinfo")]
    [PrimaryKey("ID", AutoIncrement = false)]
    [ExplicitColumns]
    public class userinfo : DbContextBase.Record<userinfo>
    {
        [Column]
        public string ID { get; set; }
        [Column]
        public string UserName { get; set; }
        [Column]
        public string UserCode { get; set; }
        [Column]
        public string PassWord { get; set; }
        [Column]
        public int IsMember { get; set; }
        [Column]
        public int IsActive { get; set; }
        [Column]
        public DateTime CreateDate { get; set; }
        [Column]
        public string Phone { get; set; }
        [Column]
        public string ImgUrl { get; set; }
    }
}
