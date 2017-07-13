using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;



namespace Nidan.Business.Helper
{
    public static class CsvHelper
    {
        public static Stream ToCsvStream(this DataTable dt)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append('"' + dt.Columns[i].ColumnName + '"');
                sb.Append(i == dt.Columns.Count - 1 ? "\n" : ",");
            }

            foreach (DataRow row in dt.Rows)
            {
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append('"' + row[i].ToString() + '"');
                    sb.Append(i == dt.Columns.Count - 1 ? "\n" : ",");
                }
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(sb.ToString());
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string GetCSV<T>(this IList<T> list)
        {
            var sb = new StringBuilder();

            //Get the properties for type T for the headers
            var propInfos = typeof(T).GetProperties();
            var propInfosLength = propInfos.Length - 1;
            for (var i = 0; i <= propInfosLength; i++)
            {
                var property = propInfos[i];
                var name = property.IsDefined(typeof(DisplayAttribute), false)
                    ? property.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name
                    : property.Name;

                sb.Append(name);

                if (i < propInfos.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.AppendLine();

            //Loop through the collection, then the properties and add the values
            var listCount = list.Count - 1;
            for (var i = 0; i <= listCount; i++)
            {
                var item = list[i];
                for (var j = 0; j <= propInfosLength; j++)
                {
                    var property = item.GetType().GetProperty(propInfos[j].Name);
                    var o = property.GetValue(item,null);
                    var formatString = property.IsDefined(typeof(DisplayFormatAttribute), false)
                        ? property.GetCustomAttributes(typeof(DisplayFormatAttribute), false).Cast<DisplayFormatAttribute>().Single().DataFormatString
                        : string.Empty;
                    if (o != null)
                    {
                        var value = o is DateTime
                                    ? ((DateTime)o).ToString(formatString)
                                    : o.ToString();
                        //Check if the value contans a comma and place it in quotes if so
                        if (value.Contains(","))
                        {
                            value = string.Concat("\"", value, "\"");
                        }
                        //Replace any \r or \n special characters from a new line with a space
                        if (value.Contains("\r"))
                        {
                            value = value.Replace("\r", " ");
                        }
                        if (value.Contains("\n"))
                        {
                            value = value.Replace("\n", " ");
                        }
                        sb.Append(value);
                    }
                    if (o == null)
                    {
                        var value = "";
                        if (value.Contains(","))
                        {
                            value = string.Concat("\"", value, "\"");
                        }
                        sb.Append(value);
                    }
                    if (j < propInfos.Length - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static Stream ToCsvStream<T>(this List<T> list)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(typeof(T) == typeof(string)
                ? string.Join("\n", list)
                : string.Join("\n", list.GetCSV()));
            byte[] byteArray = Encoding.UTF8.GetBytes(typeof(T) == typeof(string)
                ? string.Join("\n", list)
                : string.Join("\n", list.GetCSV()));
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream streamTest = new MemoryStream(byteArray);

            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        //public static void NullToString<T>(this T myObject) where T : class
        //{
        //    PropertyInfo[] properties = typeof(T).GetProperties();
        //    foreach (var info in properties)
        //    {
        //        // if a string and null, set to String.Empty
        //        if (info.PropertyType == typeof(string) &&
        //           info.GetValue(myObject, null) == null)
        //        {
        //            info.SetValue(myObject, String.Empty, null);
        //        }
        //    }
        //}
    }
}