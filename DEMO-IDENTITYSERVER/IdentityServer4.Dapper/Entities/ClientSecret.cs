using System;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer4.Dapper.Entities
{
    public class ClientSecret
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = SecretTypes.SharedSecret;

        #region Navigation Properties
        public Client Client { get; set; }
        #endregion
    }
}
