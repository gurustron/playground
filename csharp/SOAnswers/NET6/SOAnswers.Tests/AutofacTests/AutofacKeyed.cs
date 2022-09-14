using Autofac;
using Autofac.Features.Indexed;

namespace SOAnswers.Tests.AutofacTests;

public class AutofacKeyed
{
    [Test]
    public void  Keyed()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ImplOne>()
            .Keyed<IDependency>(MyTypeEnum.TypeOne)
            .SingleInstance();

        builder.RegisterType<ImplTwo>()
            .Keyed<IDependency>(MyTypeEnum.TypeTwo)
            .SingleInstance();

        builder.Register((c, p) =>
        {
            var type = p.TypedAs<MyTypeEnum>();
            var resolve = c.Resolve<IIndex<MyTypeEnum, IDependency>>();
            return resolve[type];
        });
        var container = builder.Build();
        Func<MyTypeEnum, IDependency> factory = container.Resolve<Func<MyTypeEnum, IDependency>>();
        var dependency = factory(MyTypeEnum.TypeOne);
    }    
}

public class ImplTwo:IDependency
{
}

public enum MyTypeEnum
{
    TypeOne,
    TypeTwo
}

public interface IDependency
{
}

public class ImplOne:IDependency
{
}