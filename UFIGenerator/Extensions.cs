using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UFIGenerator
{
    public static class Extensions
    {
        public static string ToDigitString(this BitArray array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (bool bit in array.Cast<bool>())
                builder.Append(bit ? "1" : "0");

            return Reverse(builder.ToString());
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static BitArray Append(this BitArray current, BitArray after)
        {
            bool[] bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }


        public static BitArray ToBinary(this int numeral, int bitCount)
        {
            BitArray binary = new BitArray(new[] { numeral }) { Length = bitCount };
            return binary;
        }

        public static BitArray ToBinary(this long numeral, int bitCount)
        {
            BitArray binary = new BitArray(BitConverter.GetBytes(numeral)) { Length = bitCount };
            return binary;
        }
    }
}
