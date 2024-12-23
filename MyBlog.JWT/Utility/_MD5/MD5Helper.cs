using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;//11
using System.Text;              //22222222

using System.Threading.Tasks;

namespace MyBlog.JWT.Utility._MD5
{
    public  static class MD5Helper
    {
        public static string MD5Encrypt32(string password)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();//  实例化 一个 md5对象
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            for ( int  i=0; i<s.Length; i++ ) 
            {
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }
}
