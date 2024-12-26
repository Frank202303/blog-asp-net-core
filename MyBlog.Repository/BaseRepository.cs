using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    //实现（IBaseRepository）： 继承时，类必须 放在 接口 前面
    public class BaseRepository<TEntity> :SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        //使用sqlSugar的IOC   依赖注入
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseRepository(ISqlSugarClient context=null):base(context)
        {
            base.Context = DbScoped.Sugar; //依赖 注入 2. 使用
            //创建 数据库
            base.Context.DbMaintenance.CreateDatabase();
            //创建 表
            base.Context.CodeFirst.InitTables(
                typeof(BlogNews),
                typeof(TypeInfo),
                typeof(WriterInfo)
                );
        }
        public async Task<bool> CreateAsync(TEntity entity)
        {
            // 增删改查：增
            return await base.InsertAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // 增删改查：删
            return await base.DeleteByIdAsync(id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            // 增删改查：改
            return await base.UpdateAsync(entity);
        }
        /// <summary>
        /// 导航 查询（virtual方法），子类想导航 查询时，重写这个方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(int id)
        {
            // 增删改查：查
            return await base.GetByIdAsync(id);
        }
        ////整一个自定义的
        public async Task<TEntity>  FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public virtual async Task<List<TEntity>> QueryAsync()
        {
            // 查询 所有的
            return await base.GetListAsync();
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            // 自定义 查询
            return await base.GetListAsync(func); 
        }

        public virtual async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            //   分页
            return await base.Context.Queryable<TEntity>()
                .ToPageListAsync(page,size,total);
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            // 自定义 查询 + 分页
            return await base.Context.Queryable<TEntity>()
                .Where(func)
                .ToPageListAsync(page, size, total);
        }
    }
}
 