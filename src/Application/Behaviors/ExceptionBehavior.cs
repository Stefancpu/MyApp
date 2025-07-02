using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Behaviors
{
    public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Domain.Exceptions.DomainException dex)
            {
                // ovde možete logovati ili mapirati domain izuzetke
                throw;
            }
            catch (Exception ex)
            {
                // generička obrada svih ostalih izuzetaka
                throw new ApplicationException(
                    "Dogodila se greška pri obradi zahteva.", ex);
            }
        }
    }
}