namespace IdentityServer4.Dapper.Entities
{
    public class ClientGrantType
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string GrantType { get; set; }

        #region Navigation Properties
        public Client Client { get; set; }
        #endregion
    }
}
