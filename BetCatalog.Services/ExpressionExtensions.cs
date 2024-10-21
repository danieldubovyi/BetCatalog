using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BetCatalog.Services
{
    internal static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>>? ToAndExpression<T>(this IEnumerable<Expression<Func<T, bool>>> expressions)
        {
            if (!expressions.Any())
            {
                return null;
            }

            var parameter = Expression.Parameter(typeof(T));

            Expression body = expressions.First().ReplaceParameter(parameter);

            foreach (var arg in expressions.Skip(1))
            {
                body = Expression.AndAlso(body, arg.ReplaceParameter(parameter));
            }

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression ReplaceParameter(this LambdaExpression expression, Expression newParameter, int parameterIndex = 0)
        {
            var visitor = new ReplaceExpressionVisitor(expression.Parameters[parameterIndex], newParameter);

            return visitor.Visit(expression.Body);
        }

        private class ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            : ExpressionVisitor
        {
            private readonly Expression oldValue = oldValue;
            private readonly Expression newValue = newValue;

            [return: NotNullIfNotNull(nameof(node))]
            public override Expression? Visit(Expression? node)
            {
                if (node == oldValue)
                {
                    return newValue;
                }

                return base.Visit(node);
            }
        }
    }
}
