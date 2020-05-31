using System;
using System.Collections.Generic;

namespace StringEnumNet
{
    public partial class StringEnum<T>
    {
        private static readonly HashSet<string> DefinedValues = new HashSet<string>();
        
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

            DefinedValues.Add(value);
            return new T { value = value };
        }
        
        public static bool IsDefined(string value)
        {
            // TODO; Verify if value was already defined.
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
                nameof(value), $"String enum value '{value}' is not defined in the {typeof(T)}");
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
        
        public static implicit operator string(StringEnum<T> stringEnum)
        {
            return stringEnum.ToString();
        }
        
        public static explicit operator StringEnum<T>(string value)
        {
            return Parse(value);
        }
    }
}