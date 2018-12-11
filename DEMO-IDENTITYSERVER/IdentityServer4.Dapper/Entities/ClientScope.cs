namespace IdentityServer4.Dapper.Entities
{
    public class ClientScope
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Scope { get; set; }

        #region Navigation Properties
        public Client Client { get; set; } 
        #endregion
    }
}
