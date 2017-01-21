using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nidan.Business.Models;

namespace Nidan.Business.Extensions
{
    public static class NodeExtensions
    {
        public static IEnumerable<T> Values<T>(this IEnumerable<Node<T>> nodes)
        {
            return nodes.Select(n => n.Value);
        }
    }

    public static class OtherExtensions
    {        
        public static string FormatInvariant(this string text, params object[] parameters)
        {
            return string.Format(CultureInfo.InvariantCulture, text, parameters);
        }
    }
}
