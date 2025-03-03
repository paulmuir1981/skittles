﻿using System.Collections.ObjectModel;

namespace Skittles.Framework.Infrastructure.Cors;

public class CorsOptions
{
    public CorsOptions()
    {
        AllowedOrigins = [];
    }

    public Collection<string> AllowedOrigins { get; }
}
