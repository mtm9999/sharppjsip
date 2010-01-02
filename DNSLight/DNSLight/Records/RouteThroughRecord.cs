using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNSLight
{
    public class RouteThroughRecord : Record, IComparable
    {
        private readonly int _preference;

        public int Preference
        {
            get { return _preference; }
        }

        private readonly string _intermediateHost;

        public string IntermediateHost
        {
            get { return _intermediateHost; }
        }

        internal RouteThroughRecord(SmartPointer pointer)
        {
            _preference = pointer.ReadShort();
            _intermediateHost = pointer.ReadDomain();
        }

        public override string ToString()
        {
            return string.Format("Intermediate Host = {0}, Preference = {1}", _intermediateHost, _preference);
        }

        public int CompareTo(object o)
        {
            RouteThroughRecord rt = o as RouteThroughRecord;

            if (rt == null)
                return 0;

            if (rt.Preference < Preference) return 1;
            if (rt.Preference > Preference) return -1;

            return -rt._intermediateHost.CompareTo(_intermediateHost);
        }

        public static bool operator ==(RouteThroughRecord lhs, RouteThroughRecord rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(RouteThroughRecord lhs, RouteThroughRecord rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            RouteThroughRecord rt = obj as RouteThroughRecord;

            if (rt == null) return false;

            if (rt.Preference != this.Preference) return false;
            if (rt.IntermediateHost != this.IntermediateHost) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Preference;
        }
    }
}
