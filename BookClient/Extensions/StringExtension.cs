using System;
using System.Collections.Generic;
using System.Text;

namespace BookClient.Extensions
{
    // How to: Implement and Call a Custom Extension Method
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-and-call-a-custom-extension-method
    public static class StringExtension
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static bool IsNullOrEmpty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        public static bool BeginsWith(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) == 0;
        }
    }
}
