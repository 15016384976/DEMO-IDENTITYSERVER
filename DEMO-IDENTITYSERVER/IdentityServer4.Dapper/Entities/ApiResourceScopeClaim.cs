namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceScopeClaim
    {
        public int Id { get; set; }
        public int ApiResourceScopeId { get; set; }

        public string Type { get; set; }

        #region
        public ApiResourceScope ApiResourceScope { get; set; }
        #endregion
    }
}
