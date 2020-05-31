namespace StringEnumNet
{
    public partial class StringEnum<T>
        where T : StringEnum<T>, new()
    {
        private string value;

        public override string ToString()
        {
            return value;
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

            if (obj.GetType() != GetType())
            {
                return false;
            }
            
            return value == ((StringEnum<T>)obj).value;
        }

        public override int GetHashCode()
        {
            return value != null ? value.GetHashCode() : 0;
        }
    }
}