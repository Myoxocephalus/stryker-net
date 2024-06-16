using System.Reflection;
using Microsoft.Testing.Platform.Extensions.TestHost;
using Stryker.TestRunner.MSTest.Setup;

namespace Stryker.TestRunner.MSTest.Testing.LifecycleCallbacks;
internal class MutantControlLifecycleCallbacks : ITestApplicationLifecycleCallbacks
{
    private readonly string _assemblyPath;
    private readonly int _mutantId;
    private readonly string _mutantControlNamespace;

    private MutantControlLifecycleCallbacks(string assemblyPath, int mutantId, string mutantControlNamespace)
    {
        _assemblyPath = assemblyPath;
        _mutantId = mutantId;
        _mutantControlNamespace = mutantControlNamespace;
    }

    public static MutantControlLifecycleCallbacks Create(string assemblyPath, int mutantId, string mutantControlNamespace) =>
        new(assemblyPath, mutantId, mutantControlNamespace);

    public string Uid => nameof(MutantControlLifecycleCallbacks);

    public string Version => "1.0.0";

    public string DisplayName => $"Stryker.{Uid}";

    public string Description => "Setup and cleanup for mutation test run.";

    public Task BeforeRunAsync(CancellationToken cancellationToken)
    {
        var projects = DirectoryScanner.FindProjects(_assemblyPath);

        // Scan through assemblies containing the name of the .csproj files.
        var loadedProjects = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(assembly => projects.Contains(assembly.GetName().Name))
            .Where(assembly => !assembly.Location.EndsWith($"{AssemblyCopy.CopySuffix}.dll"))
            .Where(assembly => assembly.Location != _assemblyPath);

        foreach (var project in loadedProjects)
        {
            InitializeMutantController(project);
        }

        return Task.FromResult(true);
    }

    public Task AfterRunAsync(int exitCode, CancellationToken cancellation) => Task.FromResult(true);

    public Task<bool> IsEnabledAsync() => Task.FromResult(true);

    private void InitializeMutantController(Assembly assembly)
    {
        var mutantControlType = assembly.DefinedTypes?.FirstOrDefault(t => t.FullName == _mutantControlNamespace);

        if (mutantControlType is null)
        {
            return;
        }

        var activeMutantField = mutantControlType.GetField("ActiveMutant");
        activeMutantField?.SetValue(null, _mutantId);
    }
}
