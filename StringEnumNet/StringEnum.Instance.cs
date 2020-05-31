using System;

namespace StringEnumNet
{
    public abstract partial class StringEnum<T> : IEquatable<T>
        where T : StringEnum<T>, new()
    {
        // Is shouldn't be possible to change value or hashCode after they are defined.
        private string value;
        private int hashCode;

        public bool Equals(T other)
        {
            return value == other?.value;
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other.GetType() == GetType() && Equals((T)other);
        }

        public override string ToString()
        {
            return value;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}