using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    class ServerTime : IEquatable<ServerTime>
    {
        public  int Hour { get; set; }
        public  int Minute { get; set; }
        public int Day { get; set; }

        public ServerTime(int hour, int minute, int day)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Day = day;
        }

        public bool Equals(ServerTime other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Hour == other.Hour && this.Minute == other.Minute && this.Day == other.Day;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ServerTime)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Hour;
                hashCode = (hashCode * 397) ^ this.Minute;
                hashCode = (hashCode * 397) ^ this.Day;
                return hashCode;
            }
        }

        public static bool operator ==(ServerTime left, ServerTime right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ServerTime left, ServerTime right)
        {
            return !Equals(left, right);
        }
    }
}
