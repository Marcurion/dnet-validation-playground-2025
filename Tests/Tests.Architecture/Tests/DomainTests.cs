using Domain.Primitives;
using Tests.Architecture.Rules;

namespace Tests.Architecture.Tests;

public class DomainTests
{
    [Fact]
    public void ShouldNotHaveDependenciesOnOtherProjects()
    {
        var result = Types
            .InAssembly(assembly: typeof(AggregateRoot).Assembly)
            .ShouldNot()
            .HaveDependencyOnAll("dnet_exception_handling.Application", "dnet_exception_handling.Infrastructure")
            .GetResult();
        Assert.True(condition: result.IsSuccessful);
    }

    [Fact]
    public void ClassValueObjectsShouldBeSealed()
    {
        var result = Types
            .InAssembly(assembly: typeof(AggregateRoot).Assembly)
            .That()
            .ResideInNamespace(name: "dnet_exception_handling.Domain.ValueObjects")
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
    

    [Fact]
    public void EntityClassesShouldNotHavePublicConstructors()
    {
        var result = Types
            .InAssembly(assembly: typeof(AggregateRoot).Assembly)
            .That()
            .ResideInNamespace(name: "dnet_exception_handling.Domain.Entities")
            .ShouldNot()
            .MeetCustomRule(rule: new ContainsPublicConstructorParametersRule())
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
}