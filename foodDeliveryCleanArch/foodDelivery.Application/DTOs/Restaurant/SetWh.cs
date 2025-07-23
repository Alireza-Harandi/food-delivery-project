namespace foodDelivery.Application.DTOs.Restaurant;

public class SetWh
{
    public DayOfWeek Day { get; set; }
    public string Start { get; set; }
    public string End { get; set; }

    public SetWh(DayOfWeek day, string start, string end)
    {
        Day = day;
        Start = start;
        End = end;
    }
}

public class SetWhRequest
{
    public Guid RestaurantId { get; set; } 
    public List<SetWh> WhList { get; set; }

    public SetWhRequest(Guid restaurantId)
    {
        RestaurantId = restaurantId;
        WhList = new ();
    }
}

public class SetWhResponse
{
    public Guid RestaurantId { get; set; }
    public List<SetWh> WorkingHours { get; set; }

    public SetWhResponse(Guid restaurantId, List<SetWh> workingHours)
    {
        RestaurantId = restaurantId;
        WorkingHours = workingHours;
    }
}