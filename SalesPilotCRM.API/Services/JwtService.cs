namespace SalesPilotCRM.API.Services
{
    public class JwtService
    {

        public JwtService(GoogleSecretService secretService)
        {
            var jwtSecret = secretService.GetSecret("jwt-secret", "salespilotcrm");
            Console.WriteLine(jwtSecret);
        }
    }
}
