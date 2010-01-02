using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    [Serializable]
    public class MxRecord : Record, IComparable
    {
        private readonly string _domainName;

        public string DomainName
        {
            get { return _domainName; }
        }

        private readonly int _preference;

        public int Preference
        {
            get { return _preference; }
        }

        internal MxRecord(SmartPointer pointer)
        {
            _preference = pointer.ReadShort();
            _domainName = pointer.ReadDomain();
        }

        public override string ToString()
        {
            return string.Format("Mail Server = {0}, Preference = {1}", _domainName, _preference);
        }

        public int CompareTo(object obj)
        {
            MxRecord other = obj as MxRecord;

            if (other != null)
            {
                if (other.Preference < Preference) return 1;
                if (other.Preference > Preference) return -1;

                return -other.DomainName.CompareTo(_domainName);
            }
            else return 0;
        }

        public static bool operator ==(MxRecord lhs, MxRecord rhs)
        {
            if (lhs == null) throw new ArgumentNullException("lhs is null");

            return lhs.Equals(rhs);
        }

        public static bool operator !=(MxRecord lhs, MxRecord rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            MxRecord other = obj as MxRecord;

            if (obj == null) return false;

            if (other.Preference != Preference) return false;

            if (other.DomainName != DomainName) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Preference;
        }
    }
}
