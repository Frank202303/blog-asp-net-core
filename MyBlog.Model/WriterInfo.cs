using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;// 引用

namespace MyBlog.Model
{
    public class WriterInfo: BaseId  //（共4个字段，id， Name，UserName，UserPwd）
    {
        //nvarchar 对中文友好
        [SugarColumn(ColumnDataType = "nvarchar(12)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(16)")]
        public string UserName { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(64)")]
        public string UserPwd { get; set; }
    }
}
