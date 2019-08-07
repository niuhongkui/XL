using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XL.Framework.DAL;

namespace Exam.DAL.DataRepository
{
    public class DataRepositoryBase : DbContextBase
    {
        public DataRepositoryBase(): base("ExamConnection")
        {
        }
    }
}
