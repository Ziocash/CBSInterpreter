using CBSInterpreter.Models.Enums;

namespace CBSInterpreter.Models
{
    public class CBSEntry
    {
        public DateTime DateTime { get; set; }

        public CBSEntryType Type { get; set; }

        public CBSEntryContext Context { get; set; }

        public string Value { get; set; } = string.Empty;

        private bool Equals(CBSEntry entry)
        {
            return (Type == entry.Type) && (Context == entry.Context) && (Value == entry.Value) && (DateTime == entry.DateTime);
        }

        public static bool operator ==(CBSEntry entry1, CBSEntry entry2)
        {
            return entry1.Equals(entry2);
        }

        public static bool operator !=(CBSEntry entry1, CBSEntry entry2)
        {
            return !entry1.Equals(entry2);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return obj is CBSEntry && this == (CBSEntry)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Context, Value);
        }
    }
}
