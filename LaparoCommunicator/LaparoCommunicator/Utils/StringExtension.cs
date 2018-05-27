using System;
using System.Linq;

namespace LaparoCommunicator
    
    static class StringExtension
    {
        public static bool IsNullOrWhitespace(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return true;

            char[] chars = s.ToCharArray();
            char[] whitespaces = chars.Where(c => Char.IsWhiteSpace(c)).ToArray();

            return chars.Length == whitespaces.Length;
        }
    }
}
