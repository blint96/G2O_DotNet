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
namespace G2O.DotNet.ServerApi
{
    using System;

    /// <summary>
    ///     A struct for storing the server time(game time)
    /// </summary>
    public struct ServerTime : IEquatable<ServerTime>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerTime" /> struct.
        /// </summary>
        /// <param name="hour">The hours value of the time.</param>
        /// <param name="minute">The minutes value of the time.</param>
        /// <param name="day">The days value of the time.</param>
        public ServerTime(int hour, int minute, int day)
        {
            this.Hour = hour;
            this.Minute = minute;
            this.Day = day;
        }

        /// <summary>
        ///     Gets the days value of the <see cref="ServerTime" />.
        /// </summary>
        public int Day { get; }

        /// <summary>
        ///     Gets the hours value of the <see cref="ServerTime" />.
        /// </summary>
        public int Hour { get; }

        /// <summary>
        ///     Gets the minutes value of the <see cref="ServerTime" />.
        /// </summary>
        public int Minute { get; }

        /// <summary>
        ///     Compares two server times for equality.
        /// </summary>
        /// <param name="left">left operant.</param>
        /// <param name="right">right operant.</param>
        /// <returns>True if both instances have the same values.</returns>
        public static bool operator ==(ServerTime left, ServerTime right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Compares two server times for inequality.
        /// </summary>
        /// <param name="left">left operant.</param>
        /// <param name="right">right operant.</param>
        /// <returns>True if both instances do not have the same values.</returns>
        public static bool operator !=(ServerTime left, ServerTime right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Compares two server times for inequality.
        /// </summary>
        /// <param name="other">
        ///     The other instance of <see cref="ServerTime" /> that should be compared to this instance of
        ///     <see cref="ServerTime" />.
        /// </param>
        /// <returns>True if both instances have the same values.</returns>
        public bool Equals(ServerTime other)
        {
            return this.Hour == other.Hour && this.Minute == other.Minute && this.Day == other.Day;
        }

        /// <summary>
        ///     Compares a <see cref="object" /> to this instance of <see cref="ServerTime" />.
        /// </summary>
        /// <param name="obj">The object that should be compared to the <see cref="ServerTime" /> instance.</param>
        /// <returns>True if both objects are <see cref="ServerTime" /> instances and have equal values.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ServerTime)obj);
        }

        /// <summary>
        ///     Calculates the hashcode for the <see cref="ServerTime" /> instance.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        ///     Returns the string representation of the <see cref="ServerTime" /> instance.
        /// </summary>
        /// <returns>The string representation of the <see cref="ServerTime" /> instance</returns>
        public override string ToString()
        {
            return $"{this.Hour}:{this.Minute}(Day:{this.Day})";
        }
    }
}