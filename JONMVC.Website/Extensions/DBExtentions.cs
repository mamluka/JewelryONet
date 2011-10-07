using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace JONMVC.Website.Extensions
{
     public static class DbExtentions
        {
            public static IQueryable<T> ExtWhereIn<T, TValue>(this ObjectQuery<T> query,
                        Expression<Func<T, TValue>> valueSelector,
                        IEnumerable<TValue> values)
            {
                return query.Where(BuildContainsExpression<T, TValue>(valueSelector, values));
            }
            public static IQueryable<T> ExtWhereIn<T, TValue>(this IQueryable<T> query,
                Expression<Func<T, TValue>> valueSelector,
                IEnumerable<TValue> values)
            {
                return query.Where(BuildContainsExpression<T, TValue>(valueSelector, values));
            }
            private static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
                    Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
            {
                if (null == valueSelector) { throw new ArgumentNullException("valueSelector"); }
                if (null == values) { throw new ArgumentNullException("values"); }
                ParameterExpression p = valueSelector.Parameters.Single();
                // p => valueSelector(p) == values[0] || valueSelector(p) == ...
                //if we have no filters then return all so return true
                if (!values.Any())
                {
                    return e => true;
                }
                var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
                var body = equals.Aggregate<Expression>(Expression.Or);
                return Expression.Lambda<Func<TElement, bool>>(body, p);
            }

            public static IQueryable<T> ExtWhereIgnoreZero<T, TValue>(this ObjectQuery<T> query,
                           Expression<Func<T, TValue>> valueSelector,
                           decimal from, decimal to)
            {
                return query.Where(BuildWhereIgnoreZeroExpression<T, TValue>(valueSelector, from, to));
            }
            public static IQueryable<T> ExtWhereFromToRangeAndIgnoreZero<T, TValue>(this IQueryable<T> query,
                Expression<Func<T, TValue>> valueSelector,
                decimal from,decimal to)
            {
                return query.Where(BuildWhereIgnoreZeroExpression<T, TValue>(valueSelector, from,to));
            }
            private static Expression<Func<TElement, bool>> BuildWhereIgnoreZeroExpression<TElement, TValue>(
                    Expression<Func<TElement, TValue>> valueSelector, decimal from,decimal to)
            {
                if (null == valueSelector) { throw new ArgumentNullException("valueSelector"); }
               
                ParameterExpression p = valueSelector.Parameters.Single();
                // p => valueSelector(p) == values[0] || valueSelector(p) == ...
                //if we have no filters then return all so return true
                if (from == 0 && to == 0)
                {
                    return e => true;
                }
                //var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
                var expressions = new List<Expression>();
                if (from > 0)
                {
                    expressions.Add(Expression.GreaterThanOrEqual(valueSelector.Body, Expression.Constant(from)));
                }
                if (to > 0)
                {
                    expressions.Add(Expression.LessThanOrEqual(valueSelector.Body, Expression.Constant(to)));
                }

                var body = expressions.Aggregate(Expression.And);
                
                
                return Expression.Lambda<Func<TElement, bool>>(body, p);
            }
        }
}