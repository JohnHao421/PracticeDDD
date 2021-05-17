using System;
using System.Diagnostics.CodeAnalysis;

namespace FindSelf.Domain.SeedWork
{
    public abstract class GuidTypedIdValueBase : IComparable<GuidTypedIdValueBase>, IEquatable<GuidTypedIdValueBase>
    {
        public Guid Value { get; }

        protected GuidTypedIdValueBase(Guid value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GuidTypedIdValueBase other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(GuidTypedIdValueBase other)
        {
            return this.Value == other.Value;
        }

        public int CompareTo([AllowNull] GuidTypedIdValueBase other)
        {
            return other.Value.CompareTo(Value);
        }

        public static bool operator ==(GuidTypedIdValueBase obj1, GuidTypedIdValueBase obj2)
        {
            if (object.Equals(obj1, null))
            {
                if (object.Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }
        public static bool operator !=(GuidTypedIdValueBase x, GuidTypedIdValueBase y) 
        {
            return !(x == y);
        }
    }
}