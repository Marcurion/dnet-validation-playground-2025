using System.Reflection;
using Domain.Interfaces;

namespace Tests.Architecture.Tests;

public class InfrastructureTests
{
    [Fact]
    public void RepositoryClassShouldImplementIRepository()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "Infrastructure"))
            .That()
            .ResideInNamespace(name: "Infrastructure.Repositories")
            .And()
            .AreClasses()
            .Should()
            .ImplementInterface(interfaceType: typeof(IRepository<>))
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }

    [Fact]
    public void RepositoryClassesShouldBeSealed()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "Infrastructure"))
            .That()
            .ResideInNamespace(name: "Infrastructure.Repositories")
            .And()
            .ImplementInterface(interfaceType: typeof(IRepository<>))
            .And()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
    
    [Fact]
    public void RepositoryClassesShouldBeInternal()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "Infrastructure"))
            .That()
            .ResideInNamespace(name: "Infrastructure.Repositories")
            .And()
            .ImplementInterface(interfaceType: typeof(IRepository<>))
            .And()
            .AreClasses()
            .Should()
            .NotBePublic()
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
    
    [Fact]
    public void RepositoryClassesShouldNotImplementApplicationInterfaces()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "Infrastructure"))
            .That()
            .ResideInNamespace(name: "Infrastructure.Repositories")
            .And()
            .AreClasses()
            .ShouldNot()
            .HaveDependencyOn(dependency: "Application")
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
}