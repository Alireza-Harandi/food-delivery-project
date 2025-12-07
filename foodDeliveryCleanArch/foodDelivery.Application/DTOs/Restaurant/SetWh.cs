using System.ComponentModel.DataAnnotations;

namespace foodDelivery.Application.DTOs.Restaurant;

public record SetWh(DayOfWeek Day, string Start, string End);

public record SetWhRequest(
    [Required] Guid RestaurantId,
    [Required] List<SetWh> WhList);

public record SetWhResponse(Guid RestaurantId, List<SetWh> WorkingHours);