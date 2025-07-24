using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodDelivery.Domain;

public class WorkingHour
{
    [Key] public Guid Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }

    public Guid RestaurantId { get; set; }
    [ForeignKey("RestaurantId")] public Restaurant? Restaurant { get; set; }

    public WorkingHour(DayOfWeek day, TimeOnly start, TimeOnly end, Guid restaurantId)
    {
        Id = Guid.NewGuid();
        Day = day;
        Start = start;
        End = end;
        RestaurantId = restaurantId;
    }

    public WorkingHour()
    {
    }
}