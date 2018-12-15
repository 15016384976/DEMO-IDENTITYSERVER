using Dapper;
using IdentityModel;
using IdentityServer4.Stores;
using IdentityServer4.Stores.Serialization;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityServer4.Dapper.Stores
{
    public class DeviceFlowStore : IDeviceFlowStore
    {
        private readonly DapperStoreOptions _dapperStoreOptions;
        private readonly IPersistentGrantSerializer _persistentGrantSerializer; // PersistentGrantSerializer

        public DeviceFlowStore(DapperStoreOptions dapperStoreOptions, IPersistentGrantSerializer persistentGrantSerializer)
        {
            _dapperStoreOptions = dapperStoreOptions;
            _persistentGrantSerializer = persistentGrantSerializer;
        }

        public async Task<Models.DeviceCode> FindByDeviceCodeAsync(string deviceCode)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT
	                DeviceCode,
                    UserCode,
                    SubjectId,
                    ClientId,
                    CreationTime,
                    Expiration,
                    Data
                FROM DeviceFlowCodes
                WHERE DeviceCode = @deviceCode;
                ";
                var deviceFlowCode = await connection.QueryFirstOrDefaultAsync<Entities.DeviceFlowCodes>(sql, new { deviceCode });
                var deviceFlowCodeData = deviceFlowCode?.Data;
                if (deviceFlowCodeData == null) return null;
                return _persistentGrantSerializer.Deserialize<Models.DeviceCode>(deviceFlowCodeData);
            }
        }

        public async Task<Models.DeviceCode> FindByUserCodeAsync(string userCode)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT
	                DeviceCode,
                    UserCode,
                    SubjectId,
                    ClientId,
                    CreationTime,
                    Expiration,
                    Data
                FROM DeviceFlowCodes
                WHERE UserCode = @userCode;
                ";
                var deviceFlowCode = await connection.QueryFirstOrDefaultAsync<Entities.DeviceFlowCodes>(sql, new { userCode });
                var deviceFlowCodeData = deviceFlowCode?.Data;
                if (deviceFlowCodeData == null) return null;
                return _persistentGrantSerializer.Deserialize<Models.DeviceCode>(deviceFlowCodeData);
            }
        }

        public async Task RemoveByDeviceCodeAsync(string deviceCode)
        {
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                DELETE
                FROM DeviceFlowCodes
                WHERE DeviceCode = @deviceCode;
                ";
                await connection.ExecuteAsync(sql, new { deviceCode });
            }
        }

        public async Task StoreDeviceAuthorizationAsync(string deviceCode, string userCode, Models.DeviceCode data)
        {
            if (deviceCode == null || userCode == null || data == null) return;
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var entity = new Entities.DeviceFlowCodes
                {
                    DeviceCode = deviceCode,
                    UserCode = userCode,
                    ClientId = data.ClientId,
                    SubjectId = data.Subject?.FindFirst(JwtClaimTypes.Subject).Value,
                    CreationTime = data.CreationTime,
                    Expiration = data.CreationTime.AddSeconds(data.Lifetime),
                    Data = _persistentGrantSerializer.Serialize(data)
                };
                var sql = $@"
                INSERT
                INTO DeviceFlowCodes(DeviceCode, UserCode, SubjectId, ClientId, CreationTime, Expiration, Data)
                VALUES(@DeviceCode, @UserCode, @SubjectId, @ClientId, @CreationTime, @Expiration, @Data);
                ";
                await connection.ExecuteAsync(sql, new { entity.DeviceCode, entity.UserCode, entity.SubjectId, entity.ClientId, entity.CreationTime, entity.Expiration, entity.Data });
            }
        }

        public async Task UpdateByUserCodeAsync(string userCode, Models.DeviceCode data)
        {
            if (userCode == null || data == null) return;
            using (var connection = new SqlConnection(_dapperStoreOptions.DbConnectionString))
            {
                var sql = $@"
                SELECT
	                DeviceCode,
                    UserCode,
                    SubjectId,
                    ClientId,
                    CreationTime,
                    Expiration,
                    Data
                FROM DeviceFlowCodes
                WHERE UserCode = @userCode;
                ";
                var deviceFlowCode = await connection.QueryFirstOrDefaultAsync<Entities.DeviceFlowCodes>(sql, new { userCode });
                if (deviceFlowCode == null) return;
                var entity = new Entities.DeviceFlowCodes
                {
                    DeviceCode = deviceFlowCode.DeviceCode,
                    UserCode = userCode,
                    ClientId = data.ClientId,
                    SubjectId = data.Subject?.FindFirst(JwtClaimTypes.Subject).Value,
                    CreationTime = data.CreationTime,
                    Expiration = data.CreationTime.AddSeconds(data.Lifetime),
                    Data = _persistentGrantSerializer.Serialize(data)
                };
                sql = $@"
                UPDATE DeviceFlowCodes
                SET DeviceCode = @DeviceCode, UserCode = @UserCode, SubjectId = @SubjectId, ClientId = @ClientId, CreationTime = @CreationTime, Expiration = @Expiration, Data = @Data
                WHERE UserCode = @UserCode
                ";
                await connection.ExecuteAsync(sql, new { entity.DeviceCode, entity.UserCode, entity.SubjectId, entity.ClientId, entity.CreationTime, entity.Expiration, entity.Data });
            }
        }
    }
}
