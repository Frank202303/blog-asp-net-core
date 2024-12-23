using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;// 引用

namespace MyBlog.Model
{
    public class BlogNews: BaseId   //文章表  继承 了BaseId （因为3个表都有Id，所以整了一个父类BaseId）
    {
        //nvarchar 对中文友好
        [SugarColumn(ColumnDataType = "nvarchar(30)")]
        public string Title { get; set; }
        [SugarColumn(ColumnDataType = "text")]
        public string Content { get; set; }
        public DateTime Time { get; set; }
        
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }
        public int TypeId { get; set; }
        public int WriterId { get; set; }

        /// <summary>
        /// 类型 ，不映射 到 数据库
        /// </summary>
        [SugarColumn(IsIgnore =true)]// 导航 查询
        public TypeInfo TypeInfo { get; set; }
        [SugarColumn(IsIgnore = true)]
        public WriterInfo WriterInfo { get; set; }

    }
}
