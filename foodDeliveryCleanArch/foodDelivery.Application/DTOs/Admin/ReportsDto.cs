namespace foodDelivery.Application.DTOs.Admin;

public record ReportsDto(List<ReportDetails> Reports);

public record ReportDetails(
    Guid ReportId,
    Guid CustomerId,
    Guid RestaurantId,
    string CustomerName,
    string RestaurantName,
    DateTime Date,
    string Description);