namespace SalesPilotCRM.Application.Interfaces
{
    public class SystemCurrentUserService : ICurrentUserService
    {
        //Fallback implementation for ICurrentUserService  during  design time or testing
        public string? UserId => "System";
        public string? Email => "system@salespilotcrm.local";
        public bool IsAuthenticated => false;
    }
}
