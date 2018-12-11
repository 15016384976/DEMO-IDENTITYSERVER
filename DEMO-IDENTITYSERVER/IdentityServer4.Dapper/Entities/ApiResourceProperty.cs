namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceProperty
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }

        #region Navigation Properties
        public ApiResource ApiResource { get; set; }
        #endregion
    }
}
