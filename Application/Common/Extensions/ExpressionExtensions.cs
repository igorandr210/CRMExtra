using System.Linq;
using System.Linq.Expressions;

namespace Application.Common.Extensions
{
    public static class ExpressionExtensions
    {
        public static MemberExpression Property(this ParameterExpression parameter, string propertyName)
        {
            var propertyPath = propertyName.Split('.');
            
            MemberExpression body = Expression.Property(parameter, propertyPath[0]);
            foreach (var member in propertyPath.Skip(1))
            {
                body = Expression.Property(body, member);
            }

            return body;
        }
    }
}