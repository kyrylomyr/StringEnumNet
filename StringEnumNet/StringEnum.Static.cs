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
        
        public static bool IsDefined(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return DefinedValues.ContainsKey(value);
        }

        public static T Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (TryParse(value, out var stringEnum))
            {
                return stringEnum;
            }

            throw new ArgumentException(nameof(value), $"The value '{value}' is not defined in the {typeof(T)}");
        }

        public static bool TryParse(string value, out T stringEnum)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return DefinedValues.TryGetValue(value, out stringEnum);
        }

        public static implicit operator string(StringEnum<T> stringEnum)
            => stringEnum?.ToString();

        public static explicit operator StringEnum<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            
            if (TryParse(value, out var stringEnum))
            {
                return stringEnum;
            }
            
            throw new InvalidCastException($"Can't cast string to {typeof(T)} because the value '{value}' is not defined");
        }

        public static bool operator ==(StringEnum<T> stringEnum1, StringEnum<T> stringEnum2)
            => stringEnum1?.value == stringEnum2?.value;

        public static bool operator !=(StringEnum<T> stringEnum1, StringEnum<T> stringEnum2)
            => stringEnum1?.value != stringEnum2?.value;

        public static bool operator ==(StringEnum<T> stringEnum1, string value2)
            => stringEnum1?.value == value2;

        public static bool operator !=(StringEnum<T> stringEnum1, string value2)
            => stringEnum1?.value != value2;

        public static bool operator ==(string value1, StringEnum<T> stringEnum2)
            => value1 == stringEnum2?.value;

        public static bool operator !=(string value1, StringEnum<T> stringEnum2)
            => value1 != stringEnum2?.value;

        protected static T Define(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (DefinedValues.ContainsKey(value))
            {
                throw new InvalidOperationException($"The value '{value}' is already defined in the {typeof(T)}");
            }

            var stringEnum = new T
                             {
                                 value = value,

                                 // Add the type name to the actual value to make the hash code for the same string value
                                 // unique across different enum types.
                                 hashCode = (typeof(T).FullName + value).GetHashCode()
                             };

            DefinedValues.Add(value, stringEnum);

            return stringEnum;
        }

        // Used for testing only.
        internal static T DefineInternal(string value)
        {
            return Define(value);
        }
    }
}