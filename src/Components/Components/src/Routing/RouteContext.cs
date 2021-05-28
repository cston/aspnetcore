// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.AspNetCore.Internal.LinkerFlags;

namespace Microsoft.AspNetCore.Components.Routing
{
    internal class RouteContext
    {
        private static readonly char[] Separator = new[] { '/' };

        public RouteContext(string path, string locationAbsolute)
        {
            // This is a simplification. We are assuming there are no paths like /a//b/. A proper routing
            // implementation would be more sophisticated.
            Segments = path.Trim('/').Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            // Individual segments are URL-decoded in order to support arbitrary characters, assuming UTF-8 encoding.
            for (int i = 0; i < Segments.Length; i++)
            {
                Segments[i] = Uri.UnescapeDataString(Segments[i]);
            }

            LocationAbsolute = locationAbsolute;
        }

        public string[] Segments { get; }

        public string LocationAbsolute { get; }

        [DynamicallyAccessedMembers(Component)]
        public Type? Handler { get; set; }

        public IReadOnlyDictionary<string, object>? Parameters { get; set; }
    }
}
