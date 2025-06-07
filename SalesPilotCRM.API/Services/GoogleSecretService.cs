using Google.Cloud.SecretManager.V1;

namespace SalesPilotCRM.API.Services
{
    public class GoogleSecretService
    {
        private readonly SecretManagerServiceClient _client;

        public GoogleSecretService(SecretManagerServiceClient client)
        {

            _client = client;
        }
        public string GetSecret(string name, string projectId)
        {

            var secretName = new SecretVersionName(projectId, name, "latest");
            var result = _client.AccessSecretVersion(secretName);
            var value = result.Payload.Data.ToStringUtf8();

            return value;


        }
    }
}
