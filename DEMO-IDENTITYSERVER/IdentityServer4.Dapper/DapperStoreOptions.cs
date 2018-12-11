using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper
{
    public class DapperStoreOptions
    {
        public string DbConnectionString { get; set; }

        public bool TokenCleanupEnabled { get; set; } = false;
        public int TokenCleanupInterval { get; set; } = 3600;
    }
}
