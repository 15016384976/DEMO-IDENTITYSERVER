namespace IdentityServer4.Dapper.Entities
{
    public class ClientClaim
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Type { get; set; }
        public string Value { get; set; }

        #region Navigation Properties
        public Client Client { get; set; }
        #endregion
    }
}
