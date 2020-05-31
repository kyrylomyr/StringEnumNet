using System;
using System.Collections.Generic;

namespace StringEnumNet
{
    public partial class StringEnum<T>
    {
        private static readonly HashSet<string> DefinedValues = new HashSet<string>();
        
        public static bool IsDefined(string value)
        {
            return DefinedValues.Contains(value);
        }

        public static T Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            if (IsDefined(value))
            {
                return new T { value = value };
            }
            
            throw new ArgumentOutOfRangeException(
                nameof(value), $"The value '{value}' is not defined in the {typeof(T)}");
        }

        public static bool TryParse(string value, out string stringEnum)
        {
            if (IsDefined(value))
            {
                stringEnum = new T { value = value };
                return true;
            }

            stringEnum = null;
            return false;
        }
        
        public static implicit operator string(StringEnum<T> stringEnum) => stringEnum.ToString();

        public static explicit operator StringEnum<T>(string value) => Parse(value);

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
            return DefineInternal(value);
        }
        
        // Used for testing purpose only.
        internal static T DefineInternal(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            if (DefinedValues.Contains(value))
            {
                throw new InvalidOperationException($"The value '{value}' is already defined in the {typeof(T)}");
            }

            DefinedValues.Add(value);

            return new T
                   {
                       value = value,
                       hashCode = (typeof(T).FullName + value).GetHashCode()
                   };
        }
    }
}