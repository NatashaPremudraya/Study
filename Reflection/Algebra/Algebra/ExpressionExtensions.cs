using System;
using System.Linq;
using System.Linq.Expressions;

namespace Algebra
{
    public static class ExpressionExtensions
    {
        public static Expression<T> Derive<T>(this Expression<T> e)
        {

            if (e == null)
                throw new Exception("Expression must be non-null");

            if (e.Parameters.Count != 1)
                throw new Exception("Incorrect number of parameters");

            if (e.NodeType != ExpressionType.Lambda)
                throw new Exception("Functionality not supported");

            return Expression.Lambda<T>(e.Body.Derive(e.Parameters[0].Name),
                e.Parameters);
        }

        private static Expression Derive(this Expression expression, string paramName)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Constant:
                    return Expression.Constant(0.0);

                case ExpressionType.Parameter:
                    return Expression.Constant(((ParameterExpression)expression).Name == paramName ? 1.0 : 0.0);

                case ExpressionType.Negate:
                    var op = ((UnaryExpression)expression).Operand;
                    return Expression.Negate(op.Derive(paramName));

                case ExpressionType.Add:
                    {
                        var dleft =
                            ((BinaryExpression)expression).Left.Derive(paramName);
                        var dright =
                            ((BinaryExpression)expression).Right.Derive(paramName);
                        return Expression.Add(dleft, dright);
                    }

                case ExpressionType.Multiply:
                    {
                        var left = ((BinaryExpression)expression).Left;
                        var right = ((BinaryExpression)expression).Right;
                        var dleft = left.Derive(paramName);
                        var dright = right.Derive(paramName);
                        return Expression.Add(
                            Expression.Multiply(left, dright),
                            Expression.Multiply(dleft, right));
                    }

                case ExpressionType.Call:
                    {
                        var methodName = ((MethodCallExpression)expression).Method.Name;
                        if (methodName != "Sin")
                            throw new ExpressionExtensionsException("Not implemented method: " + methodName);
                        var argument = ((MethodCallExpression)expression).Arguments;
                        var left = Expression.Call(typeof(Math).GetMethod("Cos"), argument);
                        var right = argument.FirstOrDefault().Derive(paramName);
                        return Expression.Multiply(left, right);
                    }

                default:
                    throw new ExpressionExtensionsException(
                        "Not implemented expression type: " + expression.NodeType);
            }
        }
    }
}