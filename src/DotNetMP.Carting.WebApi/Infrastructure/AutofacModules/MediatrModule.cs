using System.Reflection;
using Autofac;
using DotNetMP.Carting.WebApi.Application.Queries;
using MediatR;

namespace DotNetMP.Carting.WebApi.Infrastructure.AutofacModules;

public class MediatrModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

        builder.RegisterAssemblyTypes(typeof(GetCartQueryV1).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));

        builder.Register<ServiceFactory>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });
    }
}
