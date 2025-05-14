// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using HeapDumpTester;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Management.Endpoint.Actuators.HeapDump;

// To view gcdump files cross-platform:
// - dotnet tool restore
// - dotnet dotnet-heapview <PATH>.gcdump

string outputDirectory = Path.Combine(Environment.CurrentDirectory, args.Length == 1 ? args[0] : "../../../artifacts");
Directory.CreateDirectory(outputDirectory);

var optionsMonitor = new HeapDumpTestOptionsMonitor();

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Services.AddOptions();
builder.Services.AddSingleton<IOptionsMonitor<HeapDumpEndpointOptions>>(optionsMonitor);
builder.Services.AddSingleton<IHeapDumper, HeapDumper>();
builder.Services.AddSingleton(TimeProvider.System);

IHost host = builder.Build();

var heapDumper = host.Services.GetRequiredService<IHeapDumper>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

foreach (HeapDumpType type in Enum.GetValues<HeapDumpType>())
{
    optionsMonitor.CurrentValue.HeapDumpType = type;

    try
    {
        string tempPath = heapDumper.DumpHeapToFile(CancellationToken.None);

        string outputPath = Path.GetFullPath(Path.Combine(outputDirectory, Path.GetFileName(tempPath)));
        File.Copy(tempPath, outputPath);

        logger.LogInformation("Dump file written to {Path}", outputPath);
    }
    catch (Exception exception)
    {
        logger.LogError(exception, "Heap dump failed.");
    }
}

host.Run();
