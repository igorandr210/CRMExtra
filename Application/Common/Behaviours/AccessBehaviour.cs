using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using MediatR;

namespace Application.Common.Behaviours
{
    public interface IAccessValidator<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<bool> ValidateAccess(TRequest request);
    }
    
    public class AccessBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IAccessValidator<TRequest, TResponse>> _accessValidators;
        
        public AccessBehaviour(IEnumerable<IAccessValidator<TRequest, TResponse>> accessValidators)
        {
            _accessValidators = accessValidators;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_accessValidators.Any())
            {
                var validationResults = await Task.WhenAll(_accessValidators.Select(x => x.ValidateAccess(request)));
                if (validationResults.Any(x=>!x))
                    throw new NotAllowedException("Your permissions are weak for current operation.");
            }
            return await next();
        }
    }
}