using System.Reflection;

namespace Tests.Architecture.Tests;
public class ApplicationTests
{
    [Fact]
    public void ProjectShouldNotHaveDependenciesOnInfrastructureProject()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "Application"))
            .That()
            .ResideInNamespace(name: "Application")
            .ShouldNot()
            .HaveDependencyOn(dependency: "Infrastructure")
            .GetResult();
        Assert.True(condition: result.IsSuccessful);
    }

}
