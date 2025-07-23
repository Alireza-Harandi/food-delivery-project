namespace foodDelivery.Application.DTOs.Customer;

public class AutocompleteResponseDto(List<AutocompleteItemDto> items)
{
    public List<AutocompleteItemDto> Item { get; set; } = items;
}

public class AutocompleteItemDto(Guid id, string name)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
}




