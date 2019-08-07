using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XL.Framework.Contract;
using XL.Framework.Utility;

namespace XL.Framework.DAL
{
    public interface IDataRepositoryBase: IDependency
    {
        /// <summary>
        /// 返回更新条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Edit<T>(T entity) ;
        /// <summary>
        /// 返回更新后的ID（自增）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        string Add<T>(T entity) ;
        /// <summary>
        /// 返回更新条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Del<T>(T entity) ;

        /// <summary>
        /// 查询单个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql查询条件</param>
        /// <param name="keyValues">值</param>
        /// <returns></returns>
        T Find<T>(string sql,params object[] keyValues) ;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql查询条件</param>
        /// <param name="keyValues">值</param>
        /// <returns></returns>
        List<T> FindAll<T>(string sql, params object[] keyValues) ;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql查询条件</param>
        /// <param name="keyValues">值</param>
        /// <param name="count">条数</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageSize">一页条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns></returns>
        PagedList<T> FindAllByPage<T>( int pageSize, int pageIndex, out int count, string orderBy, string sql, params object[] keyValues) ;
    }
}
