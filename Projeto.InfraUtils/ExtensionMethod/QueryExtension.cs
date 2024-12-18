using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Projeto.Utils.ExtensionMethod
{
    public static class QueryExtension
    {

        public static void SetNull<T>(this T entity, Expression<Func<T, object>> action) where T : class
        {
            var expression = GetMemberInfo(action);
            string propertyName = expression.Member.Name;
            var property = entity.GetType().GetProperty(propertyName);
            property.SetValue(entity, null);

        }
        public static bool HasProperty(this object obj, string propertyName)
        {
            bool hasProperty = false;
            try
            {
                var p = obj.GetType().GetProperty(propertyName);
                if (p != null)
                {
                    hasProperty = true;

                }
            }
            catch { }
            return hasProperty;
        }

        public static void CopyTo<T>(this T source, T target) where T : class
        {

            if (source != null)
            {
                foreach (var property in source.GetType().GetProperties())
                {
                    if (property.PropertyType.IsGenericType && property.PropertyType.IsValueType == false) continue;
                    if (property.CanWrite == true)
                    {
                        object value = property.GetValue(source);
                        if (target.HasProperty(property.Name))
                            target.GetType().GetProperty(property.Name).SetValue(target, value);
                    }

                }
            }
        }
        public static string ToLowerAndNoWhiteSpace(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = value.Replace(" ", "").ToLower();
            return value;
        }
        private static MemberExpression GetMemberInfo(Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> query, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), typeof(T).Name.ToLower());
            List<string> fieldsInList = SortField.Split('.').ToList();
            var prop = GetExpression(ref fieldsInList, param);
            var exp = Expression.Lambda(prop, param);
            string method = string.Empty;

            if (query.Expression.Type == typeof(IOrderedQueryable<T>))
            {
                method = Ascending ? "ThenBy" : "ThenByDescending";
            }
            else
            {
                method = Ascending ? "OrderBy" : "OrderByDescending";
            }

            if (prop.Type.IsGenericType && typeof(IEnumerable<>).MakeGenericType(prop.Type.GetGenericArguments()).IsAssignableFrom(prop.Type))
            {
                var toQueryable = typeof(Queryable).GetMethods()
                        .Where(m => m.Name == "AsQueryable")
                        .Single(m => m.IsGenericMethod)
                        .MakeGenericMethod(prop.Type.GenericTypeArguments.First());


                var callExpressionAsQuery = Expression.Call(null, toQueryable, prop);

                var methodSub = Ascending ? "Min" : "Max";

                ParameterExpression peSub = Expression.Parameter(prop.Type.GenericTypeArguments.First(), prop.Type.GenericTypeArguments.First().Name.ToLower());

                var propSub = GetExpression(ref fieldsInList, peSub);
                var expLambdaSub = Expression.Lambda(propSub, peSub);
                var expSubOrder = Expression.Call(typeof(Queryable), methodSub, new Type[] { prop.Type.GenericTypeArguments.First(), propSub.Type }, callExpressionAsQuery, expLambdaSub);
                exp = Expression.Lambda(expSubOrder, param);
            }

            Type[] types = new Type[] { query.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);

            query = query.Provider.CreateQuery<T>(mce);

            return query;

        }

        public static IEnumerable<TResult> SelectOrder<T, TResult>(this IEnumerable<T> source, Func<T, TResult> operation)
        {
            foreach (var item in source)
            {
                yield return operation(item);
            }
        }


        public static IEnumerable<TResult> SelectOrder<T, TResult>(this IEnumerable<T> source, Func<T, int, TResult> operation)
        {
            int i = 0;
            foreach (var item in source)
            {
                yield return operation(item, i++);
            }
        }

        public static IQueryable<T> OrderByFields<T>(this IQueryable<T> query, IList<Tuple<string, string, string>> Order)
        {
            if (Order != null && Order.Count() > 0)
            {
                var param = Expression.Parameter(typeof(T), typeof(T).Name.ToLower());

                List<string> fieldsInList = Order.First().Item1.Split('.').ToList();
                var prop = GetExpression(ref fieldsInList, param);

                var exp = Expression.Lambda(prop, param);
                string method = string.Empty;

                if (query.Expression.Type == typeof(IOrderedQueryable<T>))
                {
                    method = Order.First().Item2.ToLower() == "asc" ? "ThenBy" : "ThenByDescending";
                }
                else
                {
                    method = Order.First().Item2.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
                }

                if (prop.Type.IsGenericType && typeof(IEnumerable<>).MakeGenericType(prop.Type.GetGenericArguments()).IsAssignableFrom(prop.Type))
                {
                    var toQueryable = typeof(Queryable).GetMethods()
                            .Where(m => m.Name == "AsQueryable")
                            .Single(m => m.IsGenericMethod)
                            .MakeGenericMethod(prop.Type.GenericTypeArguments.First());


                    var callExpressionAsQuery = Expression.Call(null, toQueryable, prop);

                    var methodSub = string.Empty;

                    if (string.IsNullOrEmpty(Order.First().Item3))
                    {
                        methodSub = Order.First().Item2.ToLower() == "asc" ? "Min" : "Max";
                    }
                    else
                    {
                        methodSub = Order.First().Item3;
                    }

                    ParameterExpression peSub = Expression.Parameter(prop.Type.GenericTypeArguments.First(), prop.Type.GenericTypeArguments.First().Name.ToLower());

                    var propSub = GetExpression(ref fieldsInList, peSub);
                    var expLambdaSub = Expression.Lambda(propSub, peSub);

                    var expSubOrder = Expression.Call(typeof(Queryable), methodSub, new Type[] { prop.Type.GenericTypeArguments.First(), propSub.Type }, callExpressionAsQuery, expLambdaSub);

                    exp = Expression.Lambda(expSubOrder, param);
                }

                Type[] types = new Type[] { query.ElementType, exp.Body.Type };
                var mce = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);

                query = query.Provider.CreateQuery<T>(mce);

                if (Order.Count == 1)
                {
                    return query;
                }
                else
                {
                    Order.RemoveAt(0);
                    return query.OrderByFields(Order);
                }
            }

            return query;

        }

        private static MemberExpression GetExpression(ref List<string> SortField, Expression expression)
        {
            string propbase = String.Empty;
            propbase = SortField.First();

            if (SortField.Count > 1)
            {
                SortField.RemoveAt(0);

                MemberExpression memberVerify = Expression.Property(expression, propbase);

                if (memberVerify.Type.IsGenericType && typeof(IEnumerable<>).MakeGenericType(memberVerify.Type.GetGenericArguments()).IsAssignableFrom(memberVerify.Type))
                {
                    return memberVerify;
                }
                else
                {
                    return GetExpression(ref SortField, memberVerify);
                }
            }
            else
            {
                return Expression.Property(expression, propbase);
            }
        }
    }
}
