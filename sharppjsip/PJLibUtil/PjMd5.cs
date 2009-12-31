using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJLibUtil
{
    public class PjMd5
    {
        public static byte[] Md5(byte[] data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();

            return x.ComputeHash(data);
        }
    }
}
