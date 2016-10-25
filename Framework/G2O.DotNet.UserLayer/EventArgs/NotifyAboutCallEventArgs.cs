// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyAboutCallEventArgs.cs" company="Colony Online Project">
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
namespace G2O.DotNet.ApiInterceptorLayer
{
    using System;

    /// <summary>
    ///     Event args for events that notify listeners about the call of a function.s
    /// </summary>
    /// <typeparam name="TParam1">Type of the first argument of the call</typeparam>
    public class NotifyAboutCallEventArgs<TParam1> : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyAboutCallEventArgs{TParam1}" /> class.
        /// </summary>
        /// <param name="parameter1">Value of the first argument.</param>
        public NotifyAboutCallEventArgs(TParam1 parameter1)
        {
            if (parameter1 == null)
            {
                throw new ArgumentNullException(nameof(parameter1));
            }

            this.Parameter1 = parameter1;
        }

        /// <summary>
        ///     Gets the value of the first parameter of the method call.
        /// </summary>
        public TParam1 Parameter1 { get; }
    }

    /// <summary>
    ///     Event args for events that notify listeners about the call of a function.s
    /// </summary>
    /// <typeparam name="TParam1">Type of the first argument of the call</typeparam>
    /// <typeparam name="TParam2">Type of the second argument of the call</typeparam>
    public class NotifyAboutCallEventArgs<TParam1, TParam2> : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyAboutCallEventArgs{TParam1,TParam2}" /> class.
        /// </summary>
        /// <param name="parameter1">Value of the first argument.</param>
        /// <param name="parameter2">Value of the second argument.</param>
        public NotifyAboutCallEventArgs(TParam1 parameter1, TParam2 parameter2)
        {
            if (parameter1 == null)
            {
                throw new ArgumentNullException(nameof(parameter1));
            }

            if (parameter2 == null)
            {
                throw new ArgumentNullException(nameof(parameter2));
            }

            this.Parameter1 = parameter1;
            this.Parameter2 = parameter2;
        }

        /// <summary>
        ///     Gets the value of the first parameter of the method call.
        /// </summary>
        public TParam1 Parameter1 { get; }

        /// <summary>
        ///     Gets the value of the second parameter of the method call.
        /// </summary>
        public TParam2 Parameter2 { get; }
    }

    /// <summary>
    ///     Event args for events that notify listeners about the call of a function.s
    /// </summary>
    /// <typeparam name="TParam1">Type of the first argument of the call</typeparam>
    /// <typeparam name="TParam2">Type of the second argument of the call</typeparam>
    /// <typeparam name="TParam3">Type of the third argument of the call</typeparam>
    public class NotifyAboutCallEventArgs<TParam1, TParam2, TParam3> : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyAboutCallEventArgs{TParam1,TParam2,TParam3}" /> class.
        /// </summary>
        /// <param name="parameter1">Value of the first argument.</param>
        /// <param name="parameter2">Value of the second argument.</param>
        /// <param name="parameter3">Value of the third argument.</param>
        public NotifyAboutCallEventArgs(TParam1 parameter1, TParam2 parameter2, TParam3 parameter3)
        {
            if (parameter1 == null)
            {
                throw new ArgumentNullException(nameof(parameter1));
            }

            if (parameter2 == null)
            {
                throw new ArgumentNullException(nameof(parameter2));
            }

            if (parameter3 == null)
            {
                throw new ArgumentNullException(nameof(parameter3));
            }

            this.Parameter1 = parameter1;
            this.Parameter2 = parameter2;
            this.Parameter3 = parameter3;
        }

        /// <summary>
        ///     Gets the value of the first parameter of the method call.
        /// </summary>
        public TParam1 Parameter1 { get; }

        /// <summary>
        ///     Gets the value of the second parameter of the method call.
        /// </summary>
        public TParam2 Parameter2 { get; }

        /// <summary>
        ///     Gets the value of the third parameter of the method call.
        /// </summary>
        public TParam3 Parameter3 { get; }
    }

    /// <summary>
    ///     Event args for events that notify listeners about the call of a function.s
    /// </summary>
    /// <typeparam name="TParam1">Type of the first argument of the call</typeparam>
    /// <typeparam name="TParam2">Type of the second argument of the call</typeparam>
    /// <typeparam name="TParam3">Type of the third argument of the call</typeparam>
    /// <typeparam name="TParam4">Type of the fourth argument of the call</typeparam>
    public class NotifyAboutCallEventArgs<TParam1, TParam2, TParam3, TParam4> : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyAboutCallEventArgs{TParam1,TParam2,TParam3,TParam4}" /> class.
        /// </summary>
        /// <param name="parameter1">Value of the first argument.</param>
        /// <param name="parameter2">Value of the second argument.</param>
        /// <param name="parameter3">Value of the third argument.</param>
        /// <param name="parameter4">Value of the fourth argument.</param>
        public NotifyAboutCallEventArgs(TParam1 parameter1, TParam2 parameter2, TParam3 parameter3, TParam4 parameter4)
        {
            if (parameter1 == null)
            {
                throw new ArgumentNullException(nameof(parameter1));
            }

            if (parameter2 == null)
            {
                throw new ArgumentNullException(nameof(parameter2));
            }

            if (parameter3 == null)
            {
                throw new ArgumentNullException(nameof(parameter3));
            }

            if (parameter4 == null)
            {
                throw new ArgumentNullException(nameof(parameter4));
            }

            this.Parameter1 = parameter1;
            this.Parameter2 = parameter2;
            this.Parameter3 = parameter3;
            this.Parameter4 = parameter4;
        }

        /// <summary>
        ///     Gets the value of the first parameter of the method call.
        /// </summary>
        public TParam1 Parameter1 { get; }

        /// <summary>
        ///     Gets the value of the second parameter of the method call.
        /// </summary>
        public TParam2 Parameter2 { get; }

        /// <summary>
        ///     Gets the value of the third parameter of the method call.
        /// </summary>
        public TParam3 Parameter3 { get; }

        /// <summary>
        ///     Gets the value of the fourth parameter of the method call.
        /// </summary>
        public TParam4 Parameter4 { get; }
    }

    /// <summary>
    ///     Event args for events that notify listeners about the call of a function.s
    /// </summary>
    /// <typeparam name="TParam1">Type of the first argument of the call</typeparam>
    /// <typeparam name="TParam2">Type of the second argument of the call</typeparam>
    /// <typeparam name="TParam3">Type of the third argument of the call</typeparam>
    /// <typeparam name="TParam4">Type of the fourth argument of the call</typeparam>
    /// <typeparam name="TParam5">Type of the fifth argument of the call</typeparam>
    public class NotifyAboutCallEventArgs<TParam1, TParam2, TParam3, TParam4, TParam5> : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NotifyAboutCallEventArgs{TParam1,TParam2,TParam3,TParam4,TParam5}" />
        ///     class.
        /// </summary>
        /// <param name="parameter1">Value of the first argument.</param>
        /// <param name="parameter2">Value of the second argument.</param>
        /// <param name="parameter3">Value of the third argument.</param>
        /// <param name="parameter4">Value of the fourth argument.</param>
        /// <param name="parameter5">Value of the fifth argument.</param>
        public NotifyAboutCallEventArgs(
            TParam1 parameter1, 
            TParam2 parameter2, 
            TParam3 parameter3, 
            TParam4 parameter4, 
            TParam5 parameter5)
        {
            if (parameter1 == null)
            {
                throw new ArgumentNullException(nameof(parameter1));
            }

            if (parameter2 == null)
            {
                throw new ArgumentNullException(nameof(parameter2));
            }

            if (parameter3 == null)
            {
                throw new ArgumentNullException(nameof(parameter3));
            }

            if (parameter4 == null)
            {
                throw new ArgumentNullException(nameof(parameter4));
            }

            if (parameter5 == null)
            {
                throw new ArgumentNullException(nameof(parameter5));
            }

            this.Parameter1 = parameter1;
            this.Parameter2 = parameter2;
            this.Parameter3 = parameter3;
            this.Parameter4 = parameter4;
            this.Parameter5 = parameter5;
        }

        /// <summary>
        ///     Gets the value of the first parameter of the method call.
        /// </summary>
        public TParam1 Parameter1 { get; }

        /// <summary>
        ///     Gets the value of the second parameter of the method call.
        /// </summary>
        public TParam2 Parameter2 { get; }

        /// <summary>
        ///     Gets the value of the third parameter of the method call.
        /// </summary>
        public TParam3 Parameter3 { get; }

        /// <summary>
        ///     Gets the value of the fourth parameter of the method call.
        /// </summary>
        public TParam4 Parameter4 { get; }

        /// <summary>
        ///     Gets the value of the fifth parameter of the method call.
        /// </summary>
        public TParam5 Parameter5 { get; }
    }
}