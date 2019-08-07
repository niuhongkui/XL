using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data;
using XL.Framework.Contract;
using System.Threading.Tasks;
using PetaPoco;

namespace XL.Framework.DAL
{
    /// <summary>
    /// DAL基类，实现Repository通用泛型数据访问模式
    /// </summary>
    public class DbContextBase : Database, IDataRepositoryBase
    {
        private static string conn = "";
        public DbContextBase()
            : base("XLConnection")
        {
            conn = "XLConnection";
            CommonConstruct();
        }

        public DbContextBase(string connectionStringName)
            : base(connectionStringName)
        {
            conn = connectionStringName;
            CommonConstruct();
        }

        public virtual void CommonConstruct()
        {
        }

        public interface IFactory
        {
            DbContextBase GetInstance();
        }

        public static IFactory Factory { get; set; }
        public static DbContextBase GetInstance()
        {
            if (_instance != null)
                return _instance;

            if (Factory != null)
                return Factory.GetInstance();
            else
                return new DbContextBase(conn);
        }

        [ThreadStatic]
        static DbContextBase _instance;

        public override void OnBeginTransaction()
        {
            if (_instance == null)
                _instance = this;
        }

        public override void OnEndTransaction()
        {
            if (_instance == this)
                _instance = null;
        }

        public int Edit<T>(T entity) 
        {
            return GetInstance().Update(entity);
        }

        public string Add<T>(T entity) 
        {
            return GetInstance().Insert(entity).ToString();
        }

        public int Del<T>(T entity) 
        {
            return GetInstance().Delete(entity);
        }

        public T Find<T>(string sql, params object[] keyValues) 
        {
            return GetInstance().FirstOrDefault<T>(sql, keyValues);
        }

        public List<T> FindAll<T>(string sql, params object[] keyValues) 
        {
            var strSql = Sql.Builder;
            strSql.Append("SELECT * FROM " + typeof (T).Name);
            if(!string.IsNullOrEmpty(sql))
                strSql.Where(sql, keyValues);
            return GetInstance().Fetch<T>(strSql);
        }

        public PagedList<T> FindAllByPage<T>(int pageSize, int pageIndex,out int count, string orderBy, string sql,  params object[] keyValues) 
        {
            var strSql = Sql.Builder;
            strSql.Append("SELECT * FROM " + typeof(T).Name);
            if (!string.IsNullOrEmpty(sql))
                strSql.Where(sql, keyValues);
            if (string.IsNullOrEmpty(orderBy))
                orderBy = "ID Desc";
            strSql.OrderBy(orderBy);
            var sqlCount = "";
            var list = GetInstance().SkipTake<T>(pageIndex*pageSize, pageSize,strSql, out sqlCount);
            int.TryParse(sqlCount, out count);
            var page = new PagedList<T>(list, pageIndex, pageSize,  count);
            return page;
        }

        public class Record<T> 
        {
            public static DbContextBase repo { get { return DbContextBase.GetInstance(); } }
            public bool IsNew() { return repo.IsNew(this); }
            public object Insert() { return repo.Insert(this); }
            public void Save() { repo.Save(this); }
            public int Update() { return repo.Update(this); }
            public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
            public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
            public static int Update(Sql sql) { return repo.Update<T>(sql); }
            public int Delete() { return repo.Delete(this); }
            public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
            public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
            public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
            public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
            public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
            public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
            public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
            public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
            public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
            public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
            public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
            public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
            public static T Single(Sql sql) { return repo.Single<T>(sql); }
            public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
            public static T First(Sql sql) { return repo.First<T>(sql); }
            public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
            public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
            public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
            public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
            public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
            public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
            public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
            public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
            public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
            public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

        }

    }
}
