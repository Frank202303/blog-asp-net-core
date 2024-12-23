//基类IBaseRepository<TEntity> 
//    因为 每个表都会有增删改查，所以整一个 父类
//  < 表名 >泛型， 可以 接收3个表的表名
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using SqlSugar;// 引用

namespace MyBlog.IRepository
{
    //  给一个约束   :代表 继承，必须是class。 new()：代表 有构造 new()  ，代表： 而且
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        //写 增删改查 
        Task<bool> CreateAsync(TEntity  entity);//添加，返回值是 bool型
        Task<bool> DeleteAsync(int id);
        Task<bool> EditAsync(TEntity entity);// 修改 返回值是 bool型
        Task<TEntity> FindAsync(int id);// 查询，返回值是 一组数据
        Task<TEntity> FindAsync( Expression<Func<TEntity,bool>> func);//整一个自定义的
        /// <summary>
        /// 查询全部的数据
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync();// 查询，返回值是 一组数据  *n
        /// <summary>
        /// 自定义条件查询
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity,bool>> func);
        /// <summary>
        /// 分页 查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync( int page, int size, RefAsync<int> total);// 返回前端的总数
        /// <summary>
        /// 自定义 条件 分页 查询
        /// </summary>
        /// <param name="func"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func,int page, int size, RefAsync<int> total);//
    }
}
