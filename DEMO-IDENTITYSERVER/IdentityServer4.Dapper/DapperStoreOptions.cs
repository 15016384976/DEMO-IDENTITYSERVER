namespace IdentityServer4.Dapper
{
    public class DapperStoreOptions
    {
        public string DbConnectionString { get; set; }
        public bool TokenCleanupEnabled { get; set; } = true;
        public int TokenCleanupInterval { get; set; } = 3600;
    }
}
