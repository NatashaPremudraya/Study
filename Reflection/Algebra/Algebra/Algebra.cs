using System;
using System.Linq.Expressions;

namespace Algebra
{
    public static class Algebra
    {
        public static Func<double, double> Derive(Expression<Func<double, double>> f)
        {
            var notCompiledDerive = f.Derive();
            return notCompiledDerive.Compile();
        }
    }
}