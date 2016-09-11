using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Web.Help
{
    public class CommonHelper
    {
        public string MD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] encs = md5.ComputeHash(data);
            return BitConverter.ToString(encs).Replace("-", "");
        }
    }
}