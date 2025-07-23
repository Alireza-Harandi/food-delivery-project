namespace foodDelivery.Application.DTOs.Restaurant;

public class UpdateStockDto(int stock)
{
    public int Stock { get; set; } = stock;
}