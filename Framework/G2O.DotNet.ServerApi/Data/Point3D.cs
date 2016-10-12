// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Point3D.cs" company="Colony Online Project">
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
    using System.Globalization;

    /// <summary>
    ///     A struct that describes a point in the three dimensional game world.
    /// </summary>
    public struct Point3D : IEquatable<Point3D>
    {
        /// <summary>
        ///     Gets the position on the x axis.
        /// </summary>
        public float X { get; }

        /// <summary>
        ///     Gets the position on the y axis.
        /// </summary>
        public float Y { get; }

        /// <summary>
        ///     Gets the position on the z axis.
        /// </summary>
        public float Z { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Point3D" /> struct.
        /// </summary>
        /// <param name="x">The position on the x axis.</param>
        /// <param name="y">The position on the y axis.</param>
        /// <param name="z">The position on the z axis.</param>
        public Point3D(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        ///     Compares two points for equality.
        ///     <remarks>
        ///         This function may fail because of floating point number imprecision.
        ///         Use the <see cref="AreNearlyEqual" /> method instead.
        ///     </remarks>
        /// </summary>
        /// <param name="other">
        ///     The other instance of <see cref="Point3D" /> that should be compared to this  instance of
        ///     <see cref="Point3D" />.
        /// </param>
        /// <returns>True if the instances have equal values.</returns>
        public bool Equals(Point3D other)
        {
            return this.X.Equals(other.X) && this.Y.Equals(other.Y) && this.Z.Equals(other.Z);
        }

        /// <summary>
        ///     Compares a object to this instance of <see cref="Point3D" />.
        ///     <remarks>Compares the type of the given object first before comparing the values.</remarks>
        /// </summary>
        /// <param name="obj">The object that should be compared to this instance of <see cref="Point3D" />.</param>
        /// <returns>True if the instances are of the same type and have equal values.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            {
                return obj is Point3D && this.Equals((Point3D)obj);
            }
        }

        /// <summary>
        ///     Gets the hash code of this <see cref="Point3D" />.
        ///     <remarks>The hashcode is calculated of the coordinate values.</remarks>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.X.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Y.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Compares two instances of <see cref="Point3D" /> for equality.
        /// </summary>
        /// <param name="left">Left operant.</param>
        /// <param name="right">right operant.</param>
        /// <returns>True if the instances equal in value.</returns>
        public static bool operator ==(Point3D left, Point3D right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Compares two instances of <see cref="Point3D" /> for inequality.
        /// </summary>
        /// <param name="left">Left operant.</param>
        /// <param name="right">right operant.</param>
        /// <returns>True if the instances inequal in value.</returns>
        public static bool operator !=(Point3D left, Point3D right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///     Returns a string representation of the <see cref="Point3D" /> instance.
        ///     <remarks>The string representation is ment to be used for debugging.</remarks>
        /// </summary>
        /// <returns>A string representation of the <see cref="Point3D" /> instance.</returns>
        public override string ToString()
        {
            return
                $"{this.X.ToString(CultureInfo.InvariantCulture)}|{this.Y.ToString(CultureInfo.InvariantCulture)}|{this.Z.ToString(CultureInfo.InvariantCulture)}";
        }

        /// <summary>
        ///     Compares two points for equality.
        ///     <remarks>Compares the values allowing a given inacuracy</remarks>
        /// </summary>
        /// <param name="other">The other instance of <see cref="Point3D" />.</param>
        /// <param name="epsilon">Maximum inacuracy in the value comparison.</param>
        /// <returns>True the values of the <see cref="Point3D" /> instances a equal.</returns>
        public bool AreNearlyEqual(Point3D other, float epsilon)
        {
            return Math.Abs(this.X - other.X) <= epsilon && Math.Abs(this.Y - other.Y) <= epsilon
                   && Math.Abs(this.Z - other.Z) <= epsilon;
        }

        /// <summary>
        ///     Calculates the distance this instance of <see cref="Point3D" /> and another instance of <see cref="Point3D" />
        ///     This method should not be used to check if one <see cref="Point3D" /> is in range of another,
        ///     used the <see cref="IsInRangeOf" /> method instead.
        /// </summary>
        /// <param name="other">The other instance of <see cref="Point3D" />.</param>
        /// <returns>The distance between two points.</returns>
        public float GetDistanceTo(Point3D other)
        {
            float deltaX = this.X - other.X;
            float deltaY = this.Y - other.Y;
            float deltaZ = this.Z - other.Z;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        /// <summary>
        ///     Checks if this instance of <see cref="Point3D" /> is within a given range of another <see cref="Point3D" />.
        /// </summary>
        /// <param name="other">The other <see cref="Point3D" /> instance.</param>
        /// <param name="range">The range.</param>
        /// <returns>True if the distance between the two points is equal of below the given range.</returns>
        public bool IsInRangeOf(Point3D other, float range)
        {
            float deltaX = this.X - other.X;
            float deltaY = this.Y - other.Y;
            float deltaZ = this.Z - other.Z;

            // Does not need to executed a expensive square root operation.
            return (deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ) <= (range * range);
        }
    }
}