﻿using System.Collections.Generic;

namespace IdentityServer4.Dapper.Entities
{
    public class ApiResourceScope
    {
        public int Id { get; set; }
        public int ApiResourceId { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;

        #region Navigation Properties
        public ApiResource ApiResource { get; set; }
        public List<ApiResourceScopeClaim> UserClaims { get; set; }
        #endregion
    }
}