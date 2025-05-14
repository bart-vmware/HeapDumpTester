// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Options;
using Steeltoe.Management.Endpoint.Actuators.HeapDump;

namespace HeapDumpTester;

internal sealed class HeapDumpTestOptionsMonitor : IOptionsMonitor<HeapDumpEndpointOptions>
{
    public HeapDumpEndpointOptions CurrentValue { get; } = new();

    public HeapDumpEndpointOptions Get(string? name)
    {
        return CurrentValue;
    }

    public IDisposable? OnChange(Action<HeapDumpEndpointOptions, string?> listener)
    {
        return null;
    }
}