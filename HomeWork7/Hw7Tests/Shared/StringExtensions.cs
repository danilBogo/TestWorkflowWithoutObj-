using System;

namespace Hw7Tests;

public static class StringExtensions
{
    public static string RemoveNewLine(this string src)
    {
        if (src is null) throw new ArgumentNullException(nameof(src));
        return src.Replace("\r", "").Replace("\n", "");
    }
}