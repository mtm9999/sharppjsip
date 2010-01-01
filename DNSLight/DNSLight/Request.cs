using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    public class Request
    {
        private List<Query> _questions;
        private bool _recursionDesired;

        public bool RecursionDesired
        {
            get { return _recursionDesired; }
            set { _recursionDesired = value; }
        }

        private Opcode _opcode;

        public Opcode Opcode
        {
            get { return _opcode; }
            set { _opcode = value; }
        }

        public Request()
        {
            _recursionDesired = true;
            Opcode = Opcode.StandardQuery;

            _questions = new List<Query>();
        }

        public void AddQuestion(Query question)
        {
            if (question == null) throw new ArgumentException();

            _questions.Add(question);
        }

        public byte[] GetMessage()
        {
            List<byte> data = new List<byte>();

            data.Add((byte)0);
            data.Add((byte)0);

            data.Add((byte)(((byte)_opcode << 3) | (_recursionDesired ? 0x01 : 0)));
            data.Add((byte)0);

            unchecked
            {
                data.Add((byte)(_questions.Count >> 8));
                data.Add((byte)_questions.Count);
            }

            data.Add((byte)0); data.Add((byte)0);
            data.Add((byte)0); data.Add((byte)0);
            data.Add((byte)0); data.Add((byte)0);

            foreach (Query question in _questions)
            {
                AddDomain(ref data, question.Domain);
                unchecked
                {
                    data.Add((byte)0);
                    data.Add((byte)question.DnsType);
                    data.Add((byte)0);
                    data.Add((byte)question.DnsClass);
                }
            }

            return data.ToArray();
        }

        private static void AddDomain(ref List<byte> data, string domain)
        {
            int position = 0;
            int length = 0;

            while (position < domain.Length)
            {
                length = domain.IndexOf('.', position) - position;

                if (length < 0) length = domain.Length - position;

                data.Add((byte)length);

                while (length-- > 0)
                {
                    data.Add((byte)domain[position++]);
                }

                position++;
            }

            data.Add((byte)0);
        }
    }
}
