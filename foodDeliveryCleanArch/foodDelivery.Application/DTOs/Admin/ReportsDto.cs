namespace foodDelivery.Application.DTOs.Admin;

public class ReportsDto(List<ReportDetails> reports)
{
    List<ReportDetails> Reposts { get; set; } = reports;
}

public class ReportDetails(Guid reportId, Guid customerId, Guid restaurantId, string customerName, string restaurantName, DateTime date, string description)
{
    public Guid ReportId { get; set; } = reportId;
    public Guid CustomerId { get; set; } = customerId;
    public Guid RestaurantId { get; set; } = restaurantId;
    public string CustomerName { get; set; } = customerName;
    public string RestaurantName { get; set; } = restaurantName;
    public DateTime Date { get; set; } = date;
    public string Description { get; set; } = description;
}