using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PJLib;

namespace PJLibUtil
{
    public class PjBase64
    {
        #region builtIn
        public static string encode(byte[] input)
        {
            return System.Convert.ToBase64String(input, Base64FormattingOptions.None);
        }

        public static byte[] decode(string input)
        {
            return System.Convert.FromBase64String(input);
        }
        #endregion

        #region PJSIP

        private static int inv = -1;
        private static char padding = '=';

        private static char[] base64_char = {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
        'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
        'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
        'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 
        'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', 
        '8', '9', '+', '/'};

        private static int base256_char(char c)
        {
            if (c >= 'A' && c <= 'Z')
                return (c - 'A');
            else if (c >= 'a' && c <= 'z')
                return c - 'a' + 26;
            else if (c >= '0' && c <= '9')
                return c - '0' + 52;
            else if (c == '+')
                return 62;
            else if (c == '/')
                return 63;
            else
            {
                PjLog log = new PjLog();
                log.log("base64.cs:base256_char", PjLogLevel.Error, "= should have been filtered");
                return inv;
            }
        }

        public static void pjBase256to64(byte c1, byte c2, byte c3, int padding, ref StringBuilder output)
        {
            output.Append(base64_char[c1>>2]);
            output.Append(base64_char[((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4)]);
            switch (padding)
            {
                case 0:
                    output.Append(base64_char[((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6)]);
                    output.Append(base64_char[c3 & 0x3F]);
                    break;
                case 1:
                    output.Append(base64_char[((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6)]);
                    output.Append(padding);
                    break;
                case 2:
                default:
                    output.Append(padding);
                    output.Append(padding);
                    break;
            }
        }

        private static int LenBase256To64(int len)
        {
            return (len * 4 / 3 + 3);
        }

        private static int LenBase64To256(int len)
        {
            return (len * 3 / 4);
        }

        public static void pjBase64Encode(byte[] input, out string output)
        {
            int pi = 0;
            byte c1, c2, c3;
            int i = 0;
            StringBuilder po = new StringBuilder();

            int outLen = LenBase256To64(input.Length);

            while (i < input.Length)
            {
                c1 = input[pi++];
                ++i;
                if (i == input.Length)
                {
                    pjBase256to64(c1, 0, 0, 2, ref po);
                    break;
                }
                else
                {
                    c2 = input[pi++];
                    ++i;

                    if (i == input.Length)
                    {
                        pjBase256to64(c1, c2, 0, 1, ref po);
                        break;
                    }
                    else
                    {
                        c3 = input[pi++];
                        ++i;
                        pjBase256to64(c1, c2, c3, 0, ref po);
                    }
                }
            }

            output = po.ToString();
        }

        void pjBase64Decode(string input, out byte[] output)
        {
            int len = input.Length;
            int i, j;
            int c1, c2, c3, c4;
            StringBuilder builder = new StringBuilder();
            foreach (char c in input)
                if (c != '=') builder.Append(c);
            char[] buf = builder.ToString().ToCharArray();
            len = buf.Length;

            output = new byte[LenBase64To256(len)];

            for (i = 0, j = 0; i + 3 < len; i += 4)
            {
                c1 = base256_char(buf[i]);
                c2 = base256_char(buf[i + 1]);
                c3 = base256_char(buf[i + 2]);
                c4 = base256_char(buf[i + 3]);

                output[j++] = (byte)((c1 << 2) | ((c2 & 0x30) >> 4));
                output[j++] = (byte)(((c2 & 0x0F) << 4) | ((c3 & 0x3C) >> 2));
                output[j++] = (byte)(((c3 & 0x03) << 6) | (c4 & 0x3F));
            }

            if (i < len)
            {
                c1 = base256_char(buf[i]);

                if (i + 1 < len)
                    c2 = base256_char(buf[i + 1]);
                else
                    c2 = (inv);

                if (i + 2 < len)
                    c3 = base256_char(buf[i + 2]);
                else
                    c3 = (inv);

                c4 = (inv);

                if (c2 != inv)
                {
                    output[j++] = (byte)((c1 << 2) | ((c2 & 0x30) >> 4));
                    if (c3 != inv)
                    {
                        output[j++] = (byte)(((c2 & 0x0F) << 4) | ((c3 & 0x3C) >> 2));
                        if (c4 != inv)
                        {
                            output[j++] = (byte)(((c3 & 0x03) << 6) | (c4 & 0x3F));
                        }
                    }
                }
            }

            //yay!!! hopefully it worked!!!

        }

        #endregion
    }
}
