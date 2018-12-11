using System;

namespace IdentityServer4.Dapper.Entities
{
    public class DeviceCode
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public string UserCode { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime Expiration { get; set; }
        public string Data { get; set; }
    }
}
