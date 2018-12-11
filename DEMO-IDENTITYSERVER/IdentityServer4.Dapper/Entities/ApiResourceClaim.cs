namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceClaim
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string Type { get; set; }

        #region Navigation Properties
        public ApiResource ApiResource { get; set; }
        #endregion
    }
}
