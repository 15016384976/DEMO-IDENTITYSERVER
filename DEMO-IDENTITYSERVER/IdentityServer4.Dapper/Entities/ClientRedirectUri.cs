namespace IdentityServer4.Dapper.Entities
{
    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string RedirectUri { get; set; }

        #region Navigation Properties
        public Client Client { get; set; } 
        #endregion
    }
}
