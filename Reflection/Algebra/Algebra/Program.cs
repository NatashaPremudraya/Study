using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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

        private static Expression Derive(this Expression e, string paramName)
        {
            switch (e.NodeType)
            {
                case ExpressionType.Constant:
                    return Expression.Constant(0.0);

                case ExpressionType.Parameter:
                    if (((ParameterExpression)e).Name == paramName)
                        return Expression.Constant(1.0);
                    else
                        return Expression.Constant(0.0);

                case ExpressionType.Negate:
                    Expression op = ((UnaryExpression)e).Operand;
                    return Expression.Negate(op.Derive(paramName));

                case ExpressionType.Add:
                    {
                        Expression dleft =
                           ((BinaryExpression)e).Left.Derive(paramName);
                        Expression dright =
                           ((BinaryExpression)e).Right.Derive(paramName);
                        return Expression.Add(dleft, dright);
                    }

                case ExpressionType.Multiply:
                    {
                        Expression left = ((BinaryExpression)e).Left;
                        Expression right = ((BinaryExpression)e).Right;
                        Expression dleft = left.Derive(paramName);
                        Expression dright = right.Derive(paramName);
                        return Expression.Add(
                                Expression.Multiply(left, dright),
                                Expression.Multiply(dleft, right));
                    }

                    case ExpressionType.Call:
                        return Expression.Power(Expression.Add(Expression.Constant(1),Expression.Negate(Expression.Power(e, Expression.Constant(2)))), Expression.Constant(0.5));
                // *** other node types here ***
                default:
                    throw new ExpressionExtensionsException(
                        "Not implemented expression type: " + e.NodeType.ToString());
            }
        }
    }

    public class ExpressionExtensionsException : Exception
    {
        public ExpressionExtensionsException(string msg) : base(msg, null) { }
        public ExpressionExtensionsException(string msg, Exception innerException) :
                base(msg, innerException)
        { }
    }

    public static class Algebra
    {
        public static Func<double, double> Derive(Expression<Func<double, double>> f)
        {
            return f.Derive().Compile();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            

            Expression<Func<double, double>> g1 = (x) => Math.Sin(x);
            Expression<Func<double, double>> g = (x) => 2*x;

            var t = typeof(Math).GetMethod("Sin");
            var b = Expression.Lambda<Func<double, double>>(Expression.Call(t, g));
            var temp = b.Compile();
            Console.WriteLine(temp(0));

            //Expression<Func<double, double>> expression = (x) => 2*x + 5 + x*x + Math.Sin(x);
            //var e = expression.Derive().Compile();
            //Console.WriteLine(e(5));
        }
    }

    //[TestFixture]
    //public class TestClass
    //{
    //    [Test]
    //    public void Test()
    //    {
    //        Expression<Func<double, double>> f = (x) => 2*x - x*x + Math.Sin(x * Math.PI);
    //        var derived = Algebra.CompiledDerive(f);
    //        Assert.AreEqual(0, derived(1));
    //    }
    //}
}
