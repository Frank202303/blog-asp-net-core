using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;// 引用

namespace MyBlog.Model
{
   
    public class BaseId  // 用来 让 有 【主键且 自增的表】 来 继承 他
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)] //IsIdentity 代表 自增     IsPrimaryKey：主键
        public int Id { get; set; }
    }
}
