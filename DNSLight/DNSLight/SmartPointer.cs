using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DNSLight
{
    public class SmartPointer
    {
        private byte[] _message;
        private int _position;

        public SmartPointer(byte[] message, int position)
        {
            _message = message;
            _position = position;
        }

        public SmartPointer Copy()
        {
            return new SmartPointer(_message, _position);
        }

        public void SetPosition(int position)
        {
            _position = position;
        }

        public static SmartPointer operator +(SmartPointer pointer, int offset)
        {
            return new SmartPointer(pointer._message, pointer._position + offset);
        }

        public byte Peek()
        {
            return _message[_position];
        }

        /// <summary>
        /// 8 bit field
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return _message[_position++];
        }

        /// <summary>
        /// a 32 bit field
        /// </summary>
        /// <returns></returns>
        public IPAddress ReadIPAddress()
        {
            byte b1 = ReadByte();
            byte b2 = ReadByte();
            byte b3 = ReadByte();
            byte b4 = ReadByte();

            return IPAddress.Parse(string.Format("{0}.{1}.{2}.{3}", b1, b2, b3, b4));
        }

        /// <summary>
        /// 16 bit field
        /// </summary>
        /// <returns></returns>
        public short ReadShort()
        {
            return (short)(ReadByte() << 8 | ReadByte());
        }

        /// <summary>
        /// 32 bit field
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            return (ushort)ReadShort() << 16 | (ushort)ReadShort();
        }

        /// <summary>
        /// 8 bit field as char
        /// </summary>
        /// <returns></returns>
        public char ReadChar()
        {
            return (char)ReadByte();
        }

        /// <summary>
        /// reads an entire string of format xx.yy.zz
        /// </summary>
        /// <returns></returns>
        public string ReadDomain()
        {
            StringBuilder domain = new StringBuilder();
            int len = 0;

            while ((len = ReadByte()) != 0)
            {
                if ((len & 0xc0) == 0xc0)
                {
                    SmartPointer newPointer = Copy();
                    newPointer.SetPosition((len & 0x3f) << 8 | ReadByte());

                    domain.Append(newPointer.ReadDomain());
                    return domain.ToString();
                }

                while (len > 0)
                {
                    domain.Append(ReadChar());
                    len--;
                }

                if (Peek() != 0) domain.Append('.');
            }

            return domain.ToString();
        }
    }
}
