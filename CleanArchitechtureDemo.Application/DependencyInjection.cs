using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean_Architecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Đăng ký tất cả các FluentValidation validator từ Assembly hiện tại
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Đăng ký MediatR behaviors nếu cần
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Common.Bahaviours.ValidationBehaviour<,>));

        return services;
    }
}
