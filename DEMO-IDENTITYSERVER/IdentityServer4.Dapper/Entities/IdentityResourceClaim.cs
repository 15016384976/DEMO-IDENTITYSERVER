namespace IdentityServer4.Dapper.Entities
{
    public class IdentityResourceClaim
    {
        public int Id { get; set; }
        public int IdentityResourceId { get; set; }

        public string Type { get; set; }

        #region Navigation Properties
        public IdentityResource IdentityResource { get; set; }
        #endregion
    }
}
