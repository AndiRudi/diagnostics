﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Diagnostics.Monitoring.EventPipe
{
    internal class EventProcessInfoPipeline : EventSourcePipeline<EventProcessInfoPipelineSettings>
    {
        private readonly Func<string, CancellationToken, Task> _onCommandLine;

        public EventProcessInfoPipeline(DiagnosticsClient client, EventProcessInfoPipelineSettings settings, Func<string, CancellationToken, Task> onCommandLine)
            : base(client, settings)
        {
            _onCommandLine = onCommandLine ?? throw new ArgumentNullException(nameof(onCommandLine));
        }

        internal override DiagnosticsEventPipeProcessor CreateProcessor() =>
            new DiagnosticsEventPipeProcessor(
                PipeMode.ProcessInfo,
                processInfoCallback: _onCommandLine);
    }
}