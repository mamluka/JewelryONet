using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace JONMVC.Website.Models.Utils
{
    public class MemberNameForInputElementsResolver
    {
        public string Name(Expression expression)
        {
            if (expression is UnaryExpression)
            {
                expression = ((UnaryExpression) expression).Operand;
            }
            if (expression is MethodCallExpression)
            {
                return Name((MethodCallExpression) expression);
            }
            if (expression is MemberExpression)
            {
                return Name((MemberExpression) expression);
            }
            if (expression is BinaryExpression && expression.NodeType == ExpressionType.ArrayIndex)
            {
                return Name((BinaryExpression) expression);
            }
            return null;
        }

        private string Name(BinaryExpression expression)
        {
            string result = null;
            if (expression.Left is MemberExpression)
            {
                result = Name((MemberExpression) expression.Left);
            }
            var index = Expression.Lambda(expression.Right).Compile().DynamicInvoke();
            return result + string.Format("[{0}]", index);
        }

        private string Name(MemberExpression expression)
        {
            var name = expression.Member.Name;
            var ancestorName = Name(expression.Expression);
            if (ancestorName != null)
            {
                name = ancestorName + "." + name;
            }
            return name;
        }

        private string Name(MethodCallExpression expression)
        {
            string name = null;
            if (expression.Object is MemberExpression)
            {
                name = Name((MemberExpression) expression.Object);
            }

            //TODO: Is there a more certain way to determine if this is an indexed property?
            if (expression.Method.Name == "get_Item" && expression.Arguments.Count == 1)
            {
                var index = Expression.Lambda(expression.Arguments[0]).Compile().DynamicInvoke();
                name += string.Format("[{0}]", index);
            }
            return name;
        }
    }

}