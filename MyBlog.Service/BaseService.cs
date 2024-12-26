using MyBlog.IRepository;
using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //  222. service layer：  class implement interface;
        //从 子类的构造函数 中传入， 通过接口_iBaseRepository 来访问方法
        protected IBaseRepository<TEntity> _iBaseRepository;
        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await _iBaseRepository.CreateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _iBaseRepository.DeleteAsync(id);
        }

        public async Task<bool> EditAsync(TEntity entity)
        {
            return await _iBaseRepository.EditAsync(entity);
        }

        public   async Task<TEntity> FindAsync(int id)
        {
            return await _iBaseRepository.FindAsync(id);
        }
        ////整一个自定义的,实现
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await  _iBaseRepository.FindAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync()
        {
            return await _iBaseRepository.QueryAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.QueryAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(func,page, size, total);
        }
    }
}
