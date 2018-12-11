namespace IdentityServer4.Dapper.Entities
{
    public class ClientPostLogoutRedirectUri
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string PostLogoutRedirectUri { get; set; }

        #region Navigation Properties
        public Client Client { get; set; }
        #endregion
    }
}
