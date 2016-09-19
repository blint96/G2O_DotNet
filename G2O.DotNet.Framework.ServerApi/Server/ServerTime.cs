// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerTime.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  Julian Vogel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// -
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// -
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// -
// </copyright>
// <summary>
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.ServerApi.Server
{
    using System;

    public class ServerTime : IEquatable<ServerTime>
    {
        public ServerTime(int hour, int minute, int day)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Day = day;
        }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public static bool operator ==(ServerTime left, ServerTime right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ServerTime left, ServerTime right)
        {
            return !Equals(left, right);
        }

        public bool Equals(ServerTime other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Hour == other.Hour && this.Minute == other.Minute && this.Day == other.Day;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ServerTime)obj);
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
    }
}