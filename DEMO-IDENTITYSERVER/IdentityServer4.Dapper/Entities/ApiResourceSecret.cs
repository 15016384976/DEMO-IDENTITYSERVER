using System;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceSecret
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = SecretTypes.SharedSecret;

        #region Navigation Properties
        public ApiResource ApiResource { get; set; }
        #endregion
    }
}
