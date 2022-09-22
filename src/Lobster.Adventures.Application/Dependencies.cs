using System.Reflection;

using Lobster.Adventures.Application.Adventures.Dtos;
using Lobster.Adventures.Application.Adventures.Queries;
using Lobster.Adventures.Application.SeedWork;
using Lobster.Adventures.Domain.SeedWork;

using MediatR;
using MediatR.Pipeline;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lobster.Adventures.Application
{
    public static class Dependencies
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // MediatR Exception Handlers
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
            services.AddScoped(typeof(IRequestExceptionHandler<GetAllAdventuresQuery, ListResponseDto<IReadOnlyList<AdventureDto>>, Exception>), typeof(GetAllAdventuresQueryExceptionHandler));
            services.AddScoped(typeof(IRequestExceptionHandler<GetAdventureQuery, EntityResponseDto<AdventureDto>, Exception>), typeof(GetAdventureQueryExceptionHandler));

            services.AddScoped<IBusinessRuleValidator, BusinessRuleValidator>();

            return services;
        }
    }
}