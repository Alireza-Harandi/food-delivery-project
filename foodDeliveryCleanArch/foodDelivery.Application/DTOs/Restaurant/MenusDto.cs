namespace foodDelivery.Application.DTOs.Restaurant;

public class MenusDto(List<MenuDetailsDto> menus)
{
    public List<MenuDetailsDto> Menus { get; set; } = menus;
}