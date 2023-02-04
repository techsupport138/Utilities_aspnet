using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities
{
    public static class Extension
    {
        /// <summary>
        /// کاراکتر هایی مثل 
        /// * | + | @ | # | SPACE
        /// را پاک میکند
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeleteAdditionsInsteadNumber(this string str)
        {
            str = str.Replace("@", "");
            str = str.Replace("#", "");
            str = str.Replace("+", "");
            str = str.Replace("*", "");
            str = str.Replace(" ", "");
            return str;
        }

        /// <summary>
        /// به تعداد کاراکتر مشخص از آخر متن جدا میکند
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tail_length"></param>
        /// <returns></returns>
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}
