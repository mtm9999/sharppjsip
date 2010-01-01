using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    public class Response
    {
        private readonly ReturnCode _returnCode;

        public ReturnCode ReturnCode
        {
            get { return _returnCode; }
        }

        private readonly bool _authoritativeAnswer;

        public bool AuthoritativeAnswer
        {
            get { return _authoritativeAnswer; }
        }

        private readonly bool _recursionAvailable;

        public bool RecursionAvailable
        {
            get { return _recursionAvailable; }
        }

        private readonly bool _truncated;

        public bool Truncated
        {
            get { return _truncated; }
        }

        private readonly List<Query> _questions;

        public List<Query> Questions
        {
            get { return _questions; }
        }

        private readonly List<Answer> _answers;

        public List<Answer> Answers
        {
            get { return _answers; }
        }

        private readonly List<NameServer> _nameServers;

        public List<NameServer> NameServers
        {
            get { return _nameServers; }
        }

        private readonly List<AdditionalRecord> _additionalRecords;

        public List<AdditionalRecord> AdditionalRecords
        {
            get { return _additionalRecords; }
        }

        internal Response(byte[] message)
        {
            byte flags1 = message[2];
            byte flags2 = message[3];

            int returnCode = flags2 & 15;

            if (returnCode > 6) returnCode = 6;
            _returnCode = (ReturnCode)returnCode;

            _authoritativeAnswer = ((flags1 & 4) != 0);
            _recursionAvailable = ((flags2 & 128) != 0);
            _truncated = ((flags1 & 2) != 0);

            int _questionsCount = GetShort(message, 4);
            _questions = new List<Query>();
            int _answersCount = GetShort(message, 6);
            _answers = new List<Answer>();
            int _nameServersCount = GetShort(message, 8);
            _nameServers = new List<NameServer>();
            int _additionalRecordsCount = GetShort(message, 10);
            _additionalRecords = new List<AdditionalRecord>();

            SmartPointer pointer = new SmartPointer(message, 12);

            for (int i = 0; i < _questionsCount; i++)
            {
                try
                {
                    _questions.Add(new Query(pointer));
                }
                catch
                {
                    throw new Exception("Invalid Response");
                }
            }

            for (int i = 0; i < _answersCount; i++)
            {
                _answers.Add(new Answer(pointer));
            }

            for (int i = 0; i < _nameServersCount; i++)
            {
                _nameServers.Add(new NameServer(pointer));
            }

            for (int i = 0; i < _additionalRecordsCount; i++)
            {
                _additionalRecords.Add(new AdditionalRecord(pointer));
            }
        }

        private static short GetShort(byte[] message, int position)
        {
            return (short)(message[position] << 8 | message[position + 1]);
        }
    }
}
