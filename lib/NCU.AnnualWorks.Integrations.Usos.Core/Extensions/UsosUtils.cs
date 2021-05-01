﻿using System.Collections.Generic;

namespace NCU.AnnualWorks.Integrations.Usos.Core.Extensions
{
    public static class UsosUtils
    {
        public static string ToScopes(this IList<string> scopes) => string.Join('|', scopes);
        public static string ToFields(this IList<string> fields) => string.Join('|', fields);
    }
}
