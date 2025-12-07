using foodDelivery.Application.DTOs.Admin;

namespace foodDelivery.Application.Interface;

public interface IAdminService
{
    public Task<AdminSignupResponse> SignupAsync(AdminSignupRequest request);
    public Task<ReportsDto> GetReportsAsync();
}