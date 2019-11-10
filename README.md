[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![Build Status](https://dev.azure.com/mwosz/Invi/_apis/build/status/Invi.Extensions.Configuration.Validation?branchName=master)](https://dev.azure.com/mwosz/Invi/_build/latest?definitionId=2&branchName=master)

# Invi.Extensions.Configuration.Validation

Configuration extension for .net core 3 with the possibility of config validation 
on program start

### Get start

The library extends IServiceCollection with the following methods
1.  **ConfigureAndValidate** - for setup the configuration class
2.  **UseConfigurationValidation** - add a startup filter for validation

A [NuGet](https://www.nuget.org/packages/Invi.Extensions.Configuration.Validation/) package should be added to the project 
```
dotnet add package Invi.Extensions.Configuration.Validation
```

The class that stores configuration data must implement interface **IValidation**

```c#
class FooConfig : IValidation<FooConfig>
```

### Examples

The program will validate whether **TestVariable** has been set in environment variables

```c#
class ConfigTest : IValidation<ConfigTest>
{
    [Required]
    public string TestVariable { get; set; }
}
```

```c#
public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args)
                .Build().Run();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            Environment.ExitCode = 1;
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();

                services.AddOptions();
                services.ConfigureAndValidate<ConfigTest>(configuration);

                services.UseConfigurationValidation();
            });
}
```