using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public byte ReadByte()
        {
            return _message[_position++];
        }

        public short ReadShort()
        {
            return (short)(ReadByte() << 8 | ReadByte());
        }

        public int ReadInt()
        {
            return (ushort)ReadShort() << 16 | (ushort)ReadShort();
        }

        public char ReadChar()
        {
            return (char)ReadByte();
        }

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
