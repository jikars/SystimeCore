using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Operator
{
    public static  class  Operator
    {
        public const String GREATER_THAN = ">";
        public const String SMALLER_THAN = "<";
        public const String GREATER_EQUAL = ">=";
        public const String SMALLER_EQUAL = "<=";
        public const String EQUAL = "==";
        public const String DIFFERENT = "!=";



        public static bool Compare(string op, Object left, Object right)
        {
            if (left.GetType() == right.GetType())
            {
                if (left is int)
                    return Compare(op, (int)left, (int)right);
                else if (left is long)
                    return Compare(op, (long)left, (long)right);
                else if (left is String)
                    return Compare(op, (string)left, (string)right);
                else if (left is float)
                    return Compare(op, (float)left, (float)right);
                else if (left is bool)
                    return Compare(op, (bool)left, (bool)right);
            }
            return false;

        }

        private static bool Compare<T>(string op, T left, T right) where T : IComparable<T>
        {
            switch (op)
            {
                case SMALLER_THAN: return left.CompareTo(right) < 0;
                case GREATER_THAN: return left.CompareTo(right) > 0;
                case SMALLER_EQUAL: return left.CompareTo(right) <= 0;
                case GREATER_EQUAL: return left.CompareTo(right) >= 0;
                case EQUAL: return left.Equals(right);
                case DIFFERENT: return !left.Equals(right);
                default: throw new ArgumentException("Invalid comparison operator: {0}", op);
            }
        }
    }
}
