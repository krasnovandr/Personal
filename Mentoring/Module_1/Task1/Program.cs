using System;
using System.Linq.Expressions;

namespace Task1
{
    class Program
    {
        static void Main()
        {
            Expression<Func<int, int>> incrementExpression = (a) => a + 1;
            Expression<Func<int, int>> decrementExpression = (a) => a - 1;

            var incr = (new ExpressionTreeTransformator().VisitAndConvert(incrementExpression, ""));
            var decr = (new ExpressionTreeTransformator().VisitAndConvert(decrementExpression, ""));
            var incrementResult = incr.Compile().Invoke(2);
            var decrementResult = decr.Compile().Invoke(2);
        }
    }
}
