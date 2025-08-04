using foodDelivery.Application.DTOs.Admin;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrastructure.Services;

public class AdminService(DbManager dbManager, IAuthService authService) : IAdminService
{
    private async Task CheckAccessAsync()
    {
        Token token = await authService.CheckTokenAsync(Role.Admin);
        bool exists = await dbManager.Users.AnyAsync(u => u.Id == token.UserId && u.Role == Role.Admin);
        if (!exists)
            throw new UnauthorizedAccessException("admin not found");
    }

    public async Task<AdminSignupResponse> SignupAsync(AdminSignupRequest request)
    {
        await CheckAccessAsync();

        bool usernameExists = await dbManager.Users.AnyAsync(u => u.Username == request.Username);
        if (usernameExists)
            throw new ArgumentException("Username already taken");

        User user = new(request.Username, request.Password, Role.Admin);
        dbManager.Users.Add(user);
        await dbManager.SaveChangesAsync();

        return new AdminSignupResponse(user.Username, user.Password);
    }

    public async Task<ReportsDto> GetReportsAsync()
    {
        await CheckAccessAsync();

        List<ReportDetails> reports = await dbManager.Reports
            .Include(r => r.Customer)
            .Include(r => r.Restaurant)
            .Select(r => new ReportDetails(
                r.Id,
                r.CustomerId,
                r.RestaurantId,
                r.Customer!.Name,
                r.Restaurant!.Name,
                r.Date,
                r.Description
            )).ToListAsync();

        return new ReportsDto(reports);
    }
}