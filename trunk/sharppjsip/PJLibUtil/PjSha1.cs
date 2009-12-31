using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJLibUtil
{
    public class PjSha1
    {
        public static byte[] Sha1(byte[] data)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider x = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            return x.ComputeHash(data);
        }
    }
}
