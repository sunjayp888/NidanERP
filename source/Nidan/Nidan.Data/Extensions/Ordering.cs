using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Nidan.Entity.Dto;

namespace Nidan.Data.Extensions
{
    public static class Ordering
    {

        /// <summary>
        /// Order by property name as a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="ordering"></param>
        /// <returns>IQueryable<T> Ordered by a list of OrderBy objects</returns>
        /// http://stackoverflow.com/questions/37743378/multisorting-iqueryable-using-a-string-column-name-within-a-generic-extension-me
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, List<OrderBy> ordering)
        {
            var isEmpty = source == null || !source.Any() || ordering == null || !ordering.Any();
            if (isEmpty)
                return source;

            var queryExpr = source.Expression;
            var methodAsc = "OrderBy";
            var methodDesc = "OrderByDescending";
            foreach (var order in ordering)
            {
                var selectorParam = Expression.Parameter(typeof(T), "e");
                var expression = CreateExpression(typeof(T), order.Property);
                var method = order.Direction == ListSortDirection.Descending ? methodDesc : methodAsc;
                queryExpr = Expression.Call(typeof(Queryable), method, new Type[] { selectorParam.Type, expression.Body.Type }, queryExpr, Expression.Quote(expression));
                methodAsc = "ThenBy";
                methodDesc = "ThenByDescending";
            }
            return source.Provider.CreateQuery<T>(queryExpr);
        }


        public static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "e");
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }
            return Expression.Lambda(body, param);
        }
    }


    
}
