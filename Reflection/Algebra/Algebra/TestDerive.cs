using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Algebra
{
    [TestFixture]
    public class TestDerive
    {
        [Test]
        public void TestConstatant()
        {
            Expression<Func<double, double>> f = (x) => 2;
            var derived = Algebra.Derive(f);
            Assert.AreEqual(0, derived(2));
            Assert.AreEqual(0, derived(10));
        }

        [Test]
        public void TestBasicOperations()
        {
            Expression<Func<double, double>> f = (x) => 2*x + (- x*x);
            var derived = Algebra.Derive(f);
            Assert.AreEqual(0, derived(1));
            Assert.AreEqual(2, derived(0));
        }

        [Test]
        public void TestSin()
        {
            Expression<Func<double, double>> f = (x) => Math.Sin(2*x);
            var derived = Algebra.Derive(f);
            Assert.AreEqual(2, derived(Math.PI));
        }

        [Test]
        public void TestCos()
        {
            Expression<Func<double, double>> f = (x) => Math.Cos(2 * x);
            Assert.Throws<ExpressionExtensionsException>(() => Algebra.Derive(f));
        }
    }
}