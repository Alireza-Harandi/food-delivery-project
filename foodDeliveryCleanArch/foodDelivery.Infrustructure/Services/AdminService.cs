using foodDelivery.Application.DTOs.Admin;
using foodDelivery.Application.Interface;
using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;

namespace foodDelivery.Infrustructure.Services;

public class AdminService(DbManager dbManager, IAuthService authService) : IAdminService
{
    private Token CheckAccess()
    {
        Token token = authService.CheckToken(Role.Admin);
        if (!dbManager.Users.Any(u => u.Id == token.UserId && u.Role == Role.Admin))
            throw new UnauthorizedAccessException("admin not found");
        return token;
    }

    public AdminSignupResponse Signup(AdminSignupRequest request)
    {
        CheckAccess();
        if (request.GetType().GetProperties().Any(p => p.GetValue(request) == null))
            throw new ArgumentException("All fields are required");
        if (dbManager.Users.Any(u => u.Username == request.Username))
            throw new ArgumentException("Username already taken");

        User user = new User(request.Username, request.Password, Role.Admin);
        dbManager.Users.Add(user);
        dbManager.SaveChanges();

        return new AdminSignupResponse(
            user.Username,
            user.Password
        );
    }

    public ReportsDto GetReports()
    {
        CheckAccess();
        List<ReportDetails> reports = dbManager.Reports
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
            )).ToList();

        return new ReportsDto(reports);
    }
}