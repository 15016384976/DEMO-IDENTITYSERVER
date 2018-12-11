namespace IdentityServer4.Dapper.Entities
{
    public class ClientIdPRestriction
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Provider { get; set; }

        #region Navigation Properties
        public Client Client { get; set; }
        #endregion
    }
}
