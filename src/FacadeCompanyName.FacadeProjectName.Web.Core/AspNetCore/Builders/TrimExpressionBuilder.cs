using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.AspNetCore.Builders
{
    public static class TrimExpressionBuilder
    {
        public static Action<object> Build(Type type)
        {
            var objParam = Expression.Parameter(typeof(object), "obj");

            var typedObj = Expression.Variable(type, "typed");

            var expressions = new List<Expression>
                {
                    Expression.Assign(typedObj, Expression.Convert(objParam, type))
                };

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;
                if (prop.PropertyType == typeof(IFormFile))
                    continue;
                if (prop.PropertyType == typeof(int))
                    continue;

                if (prop.PropertyType == typeof(string))
                {
                    expressions.Add(BuildStringTrim(typedObj, prop));
                }
                else if (IsEnumerable(prop.PropertyType))
                {
                    expressions.Add(BuildEnumerableTrim(typedObj, prop));
                }
                else if (!prop.PropertyType.IsValueType && prop.PropertyType != typeof(string))
                {
                    expressions.Add(BuildNestedTrim(typedObj, prop));
                }
            }

            var body = Expression.Block(new[] { typedObj }, expressions);

            return Expression.Lambda<Action<object>>(body, objParam).Compile();

        }

        private static Expression BuildStringTrim(ParameterExpression obj, PropertyInfo prop)
        {
            var propAccess = Expression.Property(obj, prop);

            var trimMethod = typeof(string).GetMethod(nameof(string.Trim), Type.EmptyTypes)!;

            var condition = Expression.NotEqual(propAccess, Expression.Constant(null));

            var assign = Expression.Assign(
                propAccess,
                Expression.Call(propAccess, trimMethod)
            );

            return Expression.IfThen(condition, assign);
        }

        private static Expression BuildNestedTrim(ParameterExpression obj, PropertyInfo prop)
        {
            var propAccess = Expression.Property(obj, prop);

            var trimMethod = typeof(TrimEngine).GetMethod(nameof(TrimEngine.Trim))!;

            var condition = Expression.NotEqual(propAccess, Expression.Constant(null));

            var call = Expression.Call(trimMethod, Expression.Convert(propAccess, typeof(object)));

            return Expression.IfThen(condition, call);
        }

        private static Expression BuildEnumerableTrim(ParameterExpression obj, PropertyInfo prop)
        {
            var propAccess = Expression.Property(obj, prop);

            var method = typeof(TrimExpressionBuilder)
                .GetMethod(nameof(ProcessEnumerable), BindingFlags.Static | BindingFlags.NonPublic)!;

            return Expression.Call(method, Expression.Convert(propAccess, typeof(IEnumerable)));
        }

        private static bool IsEnumerable(Type type)
        {
            if (type == typeof(string))
                return false;

            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        private static void ProcessEnumerable(IEnumerable enumerable)
        {
            if (enumerable == null) return;

            foreach (var item in enumerable)
            {
                if (item != null)
                    TrimEngine.Trim(item);
            }
        }
    }
}