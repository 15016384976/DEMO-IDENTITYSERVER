namespace IdentityServer4.Dapper.Entities
{
    public class ClientCorsOrigin
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Origin { get; set; }

        #region Navigation Properties
        public Client Client { get; set; }
        #endregion
    }
}
