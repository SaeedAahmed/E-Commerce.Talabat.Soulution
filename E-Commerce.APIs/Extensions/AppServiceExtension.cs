﻿using AutoMapper;
using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Helpers.Profiles;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.APIs.Extensions
{
    public static class AppServiceExtension
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(mappingProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                                                               .SelectMany(P => P.Value.Errors)
                                                                                               .Select(E => E.ErrorMessage)
                                                                                               .ToArray();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
