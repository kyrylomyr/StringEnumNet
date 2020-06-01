using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StringEnumNet
{
    public partial class StringEnum<T>
    {
        private static readonly Dictionary<string, T> DefinedValues = new Dictionary<string, T>();

        static StringEnum()
        {
            // Force a constructor of the derived class to execute.
            // As a result, all static fields will be executed, and all string enum values will be defined.
            RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
        }
        
        /// <summary>
        /// Checks if the string value is defined as a member of the enumeration.
        /// </summary>
        /// <param name="stringValue">A string value.</param>
        /// <returns><c>True</c> if the string value is defined in the enumeration; otherwise, <c>False</c>.</returns>
        public static bool IsDefined(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            return DefinedValues.ContainsKey(stringValue);
        }

        /// <summary>
        /// Converts the string value to the enumeration member. If the string value is not defined,
        /// method throws an exception.
        /// </summary>
        /// <param name="stringValue">A string value.</param>
        /// <returns>An instance of the enumeration member with the specified string value.</returns>
        public static T Parse(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            if (TryParse(stringValue, out var stringEnum))
            {
                return stringEnum;
            }

            throw new ArgumentException(nameof(stringValue), $"The value '{stringValue}' is not defined in the {typeof(T)}");
        }

        /// <summary>
        /// Tries to converts the string value to the enumeration member.
        /// </summary>
        /// <param name="stringValue">A string value.</param>
        /// <param name="stringEnum">
        /// An output instance of the enumeration member with the specified string value. If the return value is
        /// <c>False</c>, the output instance is <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>True</c> if the string value is defined in the enumeration and conversion is done successfully;
        /// otherwise, <c>False</c>.
        /// </returns>
        public static bool TryParse(string stringValue, out T stringEnum)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            return DefinedValues.TryGetValue(stringValue, out stringEnum);
        }

        public static implicit operator string(StringEnum<T> stringEnum)
            => stringEnum?.ToString();

        public static explicit operator StringEnum<T>(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return null;
            }
            
            if (TryParse(stringValue, out var stringEnum))
            {
                return stringEnum;
            }
            
            throw new InvalidCastException($"Can't cast string to {typeof(T)} because the value '{stringValue}' is not defined");
        }

        public static bool operator ==(StringEnum<T> stringEnum1, StringEnum<T> stringEnum2)
            => stringEnum1?.stringValue == stringEnum2?.stringValue;

        public static bool operator !=(StringEnum<T> stringEnum1, StringEnum<T> stringEnum2)
            => stringEnum1?.stringValue != stringEnum2?.stringValue;

        public static bool operator ==(StringEnum<T> stringEnum, string stringValue)
            => stringEnum?.stringValue == stringValue;

        public static bool operator !=(StringEnum<T> stringEnum, string stringValue)
            => stringEnum?.stringValue != stringValue;

        public static bool operator ==(string stringValue, StringEnum<T> stringEnum)
            => stringValue == stringEnum?.stringValue;

        public static bool operator !=(string stringValue, StringEnum<T> stringEnum)
            => stringValue != stringEnum?.stringValue;

        /// <summary>
        /// Defines a new string value as a member of the enumeration. The value can not be null or empty,
        /// and the same value can not be defined multiple times.
        /// </summary>
        /// <param name="stringValue">A string value.</param>
        /// <returns>An instance of the defined enumeration member with the specified string value.</returns>
        protected static T Define(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            if (DefinedValues.ContainsKey(stringValue))
            {
                throw new InvalidOperationException($"The value '{stringValue}' is already defined in the {typeof(T)}");
            }

            var stringEnum = new T
                             {
                                 stringValue = stringValue,

                                 // Add the type name to the actual value to make the hash code for the same string value
                                 // unique across different enum types.
                                 hashCode = (typeof(T).FullName + stringValue).GetHashCode()
                             };

            DefinedValues.Add(stringValue, stringEnum);

            return stringEnum;
        }

        // Used for testing only.
        internal static T DefineInternal(string stringValue)
        {
            return Define(stringValue);
        }
    }
}