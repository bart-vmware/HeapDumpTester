﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Steeltoe.Management.Endpoint.Actuators.HeapDump;

public sealed class HeapDumpEndpointOptions
{
    /// <summary>
    /// Gets or sets the type of dump to create. Default value: Full (on macOS: GCDump).
    /// </summary>
    public HeapDumpType? HeapDumpType { get; set; }
}