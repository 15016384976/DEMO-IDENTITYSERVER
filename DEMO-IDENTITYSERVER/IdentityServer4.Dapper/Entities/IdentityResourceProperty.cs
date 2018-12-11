namespace IdentityServer4.Dapper.Entities
{
    public class IdentityResourceProperty
    {
        public int Id { get; set; }
        public int IdentityResourceId { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

        #region Navigation Properties
        public IdentityResource IdentityResource { get; set; }
        #endregion
    }
}
